using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO
{
    public class DocumentProfileInputDAO
    {
        public Guid HealthProfileId { get; set; }
        public int Type { get; set; }
        public string? ContentMedical { get; set; }
        public List<string>? Image { get; set; }
    }
}
