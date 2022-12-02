using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class City : EntityBase
    {
        public City()
        {
            Districts = new Collection<District>();
        }

        public string Title { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
