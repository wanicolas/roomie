using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Roomie.Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Roomie.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    return BadRequest(new { message = "Un utilisateur avec cet email existe déjà." });

                var user = new ApplicationUser
                {
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { message = "Échec de l'inscription.", errors });
                }

                // Vérifier si le rôle "User" existe, sinon le créer
                var roleExists = await _roleManager.RoleExistsAsync("User");
                if (!roleExists)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("User"));
                    if (!roleResult.Succeeded)
                    {
                        return StatusCode(500, new { message = "Erreur lors de la création du rôle 'User'." });
                    }
                }

                // Assigner le rôle "User" au nouvel utilisateur
                await _userManager.AddToRoleAsync(user, "User");

                return Ok(new { message = "Utilisateur enregistré avec succès !" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur inattendue : " + ex.Message);
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { message = "Email ou mot de passe incorrect." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Email ou mot de passe incorrect." });

            // Génération du token JWT
            var token = await GenerateJwtToken(user);
            
            return Ok(new { Token = token });
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            // Ajouter les rôles de l'utilisateur
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JwtSettings:TokenExpiryHours"]!)),
                signingCredentials: creds,
                claims: authClaims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
