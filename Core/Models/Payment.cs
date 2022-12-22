using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Payment : EntityBase
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Url { get; set; }
    }
}
