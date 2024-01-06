using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Comments : EntityBase
    {
        public Comments()
        {
            SubComments = new List<Comments>();
        }

        public string Name { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int Like { get; set; }
        public Comments? MainComment { get; set; }
        public ICollection<Comments> SubComments { get; set; }
        public bool Situation { get; set; }
    }
}
