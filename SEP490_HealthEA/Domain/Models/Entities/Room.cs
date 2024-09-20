using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Room
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
    }
}
