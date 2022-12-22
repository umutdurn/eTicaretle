using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Calculate
{
    public class CalculateInstallmentModel
    {
        public string NumberOfInstallment { get; set; }
        public decimal InstallmentPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
