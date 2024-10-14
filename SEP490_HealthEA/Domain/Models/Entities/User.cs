using System;
using System.Collections.Generic;
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
        public required string LName { get; set; }
        public string? FName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Gender { get; set; }
        public virtual ICollection<HealthProfile> PatientProfiles { get; set; } = new List<HealthProfile>();
    }
}
