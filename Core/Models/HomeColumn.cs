using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HomeColumn : EntityBase
    {
        public HomeColumn()
        {
            ColumnDetail = new Collection<ColumnDetail>();
        }

        public string ColumnName { get; set; }
        public int LeftColumn { get; set; }
        public int MidColumn { get; set; }
        public int RightColumn { get; set; }
        public int Order { get; set; }
        public Screen? Screen { get; set; }
        public ICollection<ColumnDetail> ColumnDetail { get; set; }
    }
}
