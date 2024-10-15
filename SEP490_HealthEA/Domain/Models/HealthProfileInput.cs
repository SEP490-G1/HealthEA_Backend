using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class HealthProfileInput
    {

        public string? FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? Residence { get; set; }
        public string? Note { get; set; }
    }
}
