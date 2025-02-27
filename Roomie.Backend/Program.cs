using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Roomie.Backend.Data;
using Roomie.Backend.Models;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://localhost:3000", "http://localhost:5184")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Ajout des services MVC et Swagger
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

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        // Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        // BearerFormat = "JWT",
        // In = ParameterLocation.Header,
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
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuration de la base de données SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=roomie.db"));

// Configuration d'Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Désactiver les redirections automatiques d'Identity
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
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async context =>
            {
                context.NoResult();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(context.Exception.Message);
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
              //  context.Response.StatusCode = 401;
              // context.Response.ContentType = "text/plain";
                return Task.CompletedTask;
            },
            // OnMessageReceived = context =>
            // {
            //     string authorization = context.Request.Headers["Authorization"];
			// 	Console.WriteLine("auth: " + authorization);
            //     if (string.IsNullOrEmpty(authorization))
            //     {
            //         context.NoResult();
            //         return Task.CompletedTask;
            //     }

            //     if (authorization.StartsWith(JwtBearerDefaults.AuthenticationScheme + " ", StringComparison.OrdinalIgnoreCase))
            //         context.Token = authorization[(JwtBearerDefaults.AuthenticationScheme + " ").Length..].Trim();

            //     if (string.IsNullOrEmpty(context.Token))
            //     {
            //         context.NoResult();
            //         return Task.CompletedTask;
            //     }
			// 	Console.WriteLine("auth: " + context.Token);

            //     return Task.CompletedTask;
            // },
            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddAuthorization();

var app = builder.Build();

// Initialiser la base de données et ajouter un admin
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

// Initialiser la base de données et ajouter des rôles si besoin
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
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Vérification des configs
Console.WriteLine($"JWT Issuer: {jwtIssuer}");
Console.WriteLine($"JWT Audience: {jwtAudience}");
Console.WriteLine($"JWT Key length: {jwtKey.Length} (ne pas afficher en prod)");

app.Run();

// Méthode pour créer les rôles
async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "User", "Admin" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
            Console.WriteLine($"[INFO] Rôle créé : {roleName}");
        }
    }
}