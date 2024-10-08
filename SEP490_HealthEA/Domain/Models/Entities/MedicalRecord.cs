using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class MedicalRecord
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PantientId { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public List<string> Image { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public int Status { get; set; }
        //public virtual User User { get; set; }
        public virtual PatientProfile PatientProfile { get; set; }
    }
}
