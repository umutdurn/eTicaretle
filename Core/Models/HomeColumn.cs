using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HomeColumn : EntityBase
    {
        public string ColumnName { get; set; }
        public int LeftColumn { get; set; }
        public int MidColumn { get; set; }
        public int RightColumn { get; set; }
        public int Order { get; set; }
    }
}
