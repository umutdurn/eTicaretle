using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Core.Models
{
    public class Coupons : EntityBase
    {
        public string Code { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public bool ForOnce { get; set; }
        public bool Situation { get; set; }
    }
}
