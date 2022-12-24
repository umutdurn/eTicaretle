using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Address : EntityBase
    {
        public Member? Member { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GSM { get; set; }
        public string Email { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public string Adress { get; set; }
    }
}
