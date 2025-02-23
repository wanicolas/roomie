using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Roomie.Backend.Models;

namespace Roomie.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Ajouter un utilisateur en tant qu'admin
        [HttpPost("add-admin")]
        public async Task<IActionResult> AddAdmin([FromBody] AddAdminRequest request)
        {
            // Trouver l'utilisateur par son email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { message = "Utilisateur non trouvé." });
            }

            // Vérifier si le rôle Admin existe, sinon le créer
            var roleExist = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                var createRoleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!createRoleResult.Succeeded)
                {
                    return BadRequest(new { message = "Erreur lors de la création du rôle Admin." });
                }
            }

            // Ajouter l'utilisateur au rôle Admin
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return Ok(new { message = "Utilisateur ajouté comme administrateur avec succès." });
            }

            return BadRequest(new { message = "Échec de l'ajout de l'utilisateur au rôle Admin." });
        }
    }

    public class AddAdminRequest
    {
        public required string Email { get; set; }
    }
}
