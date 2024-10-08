using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class PatientProfile
    {
        [Key]
        public Guid PantientId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Residence { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public virtual User User { get; set; }
    }
}
