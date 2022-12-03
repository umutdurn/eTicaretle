using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Cargo : EntityBase
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public ICollection<Country> Countries { get; set; } = new HashSet<Country>();
    }
}
