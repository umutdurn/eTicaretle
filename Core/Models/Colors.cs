using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Colors : EntityBase
    {
        public string Title { get; set; }
        public ICollection<Product> Product { get; set; } = new HashSet<Product>();
    }
}
