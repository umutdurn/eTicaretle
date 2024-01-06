using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Screen : EntityBase
    {
        public string Name { get; set; }
        public int MinResolution { get; set; }
        public int MaxResolution { get; set; }
    }
}
