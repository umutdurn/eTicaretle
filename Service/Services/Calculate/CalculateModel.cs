using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Calculate
{
    public class Calculate
    {
        public int CartId { get; set; }
        public Product Product { get; set; }
        public int Piece{ get; set; }
        public decimal Subtotal { get; set; }
    }

    public class CalculateModel {

        public List<Calculate> Calculate { get; set; }
        public decimal Cargo { get; set; }
        public decimal Coupon { get; set; }
        public decimal CartTotal { get; set; }
        public decimal GeneralTotal { get; set; }

    }
}
