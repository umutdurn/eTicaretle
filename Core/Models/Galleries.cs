using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Galleries : EntityBase
    {
        public string Image { get; set; }
        public bool Mobile { get; set; }
        public Product Product { get; set; }
    }
}
