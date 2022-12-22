using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Installment : EntityBase
    {
        public int Number { get; set; }
        public float Interest { get; set; }
    }
}
