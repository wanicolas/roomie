using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roomie.Backend.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; } // Clé étrangère vers ApplicationUser

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [Required]
        public int RoomId { get; set; } // Clé étrangère vers Room

        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
