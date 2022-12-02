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
    }
}
