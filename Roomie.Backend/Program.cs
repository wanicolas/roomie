using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Roomie.Backend.Data;
using Roomie.Backend.Models;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Remplacer les deux configurations CORS par une seule
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins(
                "http://localhost:3000",
                "http://localhost:5184"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Ajouter les services MVC
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Roomie API",
        Version = "v1",
        Description = "API pour la gestion des salles et réservations"
    });

    // Définition de la sécurité JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entrez 'Bearer' [espace] et votre token. Exemple: 'Bearer 12345abcdef'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Ajouter la connexion SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=roomie.db"));

// Ajouter Identity pour la gestion des utilisateurs
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Désactiver la confirmation de compte
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Désactiver les redirections automatiques d'Identity (Login & AccessDenied)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

// Configuration JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();
            
            logger.LogInformation("Token validé pour l'utilisateur: {user}", 
                context.Principal?.Identity?.Name);
            
            foreach (var claim in context.Principal?.Claims ?? Enumerable.Empty<Claim>())
            {
                logger.LogInformation("Claim dans le token - Type: {type}, Value: {value}", 
                    claim.Type, claim.Value);
            }
        },
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();
            
            logger.LogError("Échec de l'authentification: {error}", 
                context.Exception.Message);
            
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Initialiser la BDD avec un admin si besoin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbInitializer.Initialize(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERREUR] Échec de l'initialisation de la BDD : {ex.Message}");
    }
}

// Activer Swagger uniquement en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialiser la base de données et ajouter des données de test
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    RoomSeeder.Seed(context);

    try
    {
        await CreateRoles(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERREUR] Impossible de créer les rôles : {ex.Message}");
    }
}

// Middleware
app.UseHttpsRedirection();

// Ajouter UseCors() juste avant UseAuthentication
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Afficher la configuration au démarrage
Console.WriteLine($"JWT Issuer: {app.Configuration["Jwt:Issuer"]}");
Console.WriteLine($"JWT Audience: {app.Configuration["Jwt:Audience"]}");
Console.WriteLine($"JWT Key length: {app.Configuration["Jwt:Key"]?.Length ?? 0}");

app.Run();

// Méthode pour créer les rôles si ils n'existent pas
async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "User", "Admin" };

    foreach (var roleName in roleNames)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
            Console.WriteLine($"[INFO] Rôle créé : {roleName}");
        }
    }
}
