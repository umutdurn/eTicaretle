using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Cart : EntityBase
    {
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Piece { get; set; }
        public decimal TotalPrice { get; set; }
        public string CookieId { get; set; }
        public Order? Order { get; set; }

    }
}
