using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string LastName { get; set; }
        public string? FirstName { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public virtual ICollection<HealthProfile> PatientProfiles { get; set; } = new List<HealthProfile>();
        [DefaultValue("true")]
        public string Role { get; set; } = "User";
        public string Status { get; set; } = "INACTIVE";
    }
}

