using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Person
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public int roomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
