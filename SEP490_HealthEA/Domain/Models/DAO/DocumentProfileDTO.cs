using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO
{
    public class DocumentProfileDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PantientId { get; set; }
        public int Type { get; set; }
        public string? ContentMedical { get; set; }
        public List<string>? Image { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public int Status { get; set; }
    }
}
