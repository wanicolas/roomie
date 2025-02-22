using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Roomie.Backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public List<Reservation> Reservations { get; set; } = new();
    }
}
