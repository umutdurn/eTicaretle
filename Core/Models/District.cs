using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class District : EntityBase
    {
        public City City { get; set; }
        public string Title { get; set; }
    }
}
