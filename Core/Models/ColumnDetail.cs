using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ColumnDetail:EntityBase
    {
        public HomeColumn HomeColumn { get; set; }
        public string Url { get; set; }
        public string Background { get; set; }
        public string FrontText { get; set; }
        public string Column { get; set; }
        public string FrontTitle { get; set; }
    }
}
