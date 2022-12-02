using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Order : EntityBase
    {
        public Order()
        {
            Cart = new Collection<Cart>();
        }

        public ICollection<Cart> Cart { get; set; }
    }
}
