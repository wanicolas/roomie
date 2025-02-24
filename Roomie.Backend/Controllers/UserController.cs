using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Roomie.Backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Roomie.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserController> _logger;

        public UserController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ILogger<UserController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // Ajouter un utilisateur en tant qu'admin
        [HttpPost("add-admin")]
        public async Task<IActionResult> AddAdmin([FromBody] AddAdminRequest request)
        {
            try 
            {
                // Vérifier si l'email est fourni
                if (string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new { message = "L'email est requis." });
                }

                // Trouver l'utilisateur
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return NotFound(new { message = "Utilisateur non trouvé." });
                }

                // Vérifier si le rôle Admin existe
                var roleExists = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Vérifier si l'utilisateur est déjà admin
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return BadRequest(new { message = "L'utilisateur est déjà admin." });
                }

                // Ajouter le rôle
                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    return Ok(new { message = "Utilisateur promu admin avec succès." });
                }

                return BadRequest(new { message = "Échec de l'ajout du rôle admin." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur.", error = ex.Message });
            }
        }

        [HttpGet("check-role")]
        public async Task<IActionResult> CheckRole([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "Utilisateur non trouvé" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            
            return Ok(new { 
                email = user.Email,
                roles = roles 
            });
        }

        [HttpPost("force-admin")]
        public async Task<IActionResult> ForceAdmin([FromBody] AddAdminRequest request)
        {
            try 
            {
                // Vérifier si l'utilisateur existe
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return NotFound(new { message = "Utilisateur non trouvé" });
                }

                // Vérifier si le rôle Admin existe
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest(new { message = "Impossible de créer le rôle Admin", errors = roleResult.Errors });
                    }
                }

                // Vérifier si l'utilisateur a déjà le rôle Admin
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return Ok(new { message = "L'utilisateur a déjà le rôle Admin" });
                }

                // Ajouter le rôle Admin
                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    return Ok(new { message = "Rôle Admin ajouté avec succès" });
                }

                // Si l'ajout a échoué, retourner les erreurs détaillées
                return BadRequest(new { 
                    message = "Échec de l'ajout du rôle Admin",
                    errors = result.Errors.Select(e => new { e.Code, e.Description })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Erreur lors de l'ajout du rôle Admin",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        [HttpGet("info")]
        [Authorize]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfo()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Utilisateur non authentifié");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("Utilisateur non trouvé");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userInfo = new UserInfoDTO
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    Roles = roles.ToList()
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des informations utilisateur");
                return StatusCode(500, "Erreur interne du serveur");
            }
        }

        [HttpPut("info")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoDTO userInfo)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Utilisateur non authentifié");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("Utilisateur non trouvé");
                }

                user.FullName = userInfo.FullName;
                
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour des informations utilisateur");
                return StatusCode(500, "Erreur interne du serveur");
            }
        }
    }

    public class AddAdminRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
