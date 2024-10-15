using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class HealthProfile
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public required string ProfileCode { get; set; }
        [Required]
        public required string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? Residence { get; set; }
        public string? Note { get; set; }
        /// <summary>
        /// 0 - private
        /// 1 - doctor can view
        /// 2 - user can view
        /// 3 - public
        /// </summary>
        public int SharedStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public virtual ICollection<DocumentProfile> MedicalRecords { get; set; } = new List<DocumentProfile>();
        public virtual User? User { get; set; }
    }
}
