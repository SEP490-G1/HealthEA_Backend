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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Gender { get; set; }
        public virtual ICollection<PatientProfile> PatientProfiles { get; set; } = new List<PatientProfile>();
    }
}
