using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ReturnOrder : EntityBase
    {
        public Order Order { get; set; }
        public Product? SendToBack { get; set; }
        public Product? WantToBuy { get; set; }
        public string? NameSurname { get; set; }
        public string? IBAN { get; set; }
        public bool Type { get; set; }
        public bool Situation { get; set; }
    }
}
