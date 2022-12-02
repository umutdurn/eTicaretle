using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Member :EntityBase
    {
        public Member()
        {
            Addresse = new Collection<Address>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Address> Addresse { get; set; }
    }
}
