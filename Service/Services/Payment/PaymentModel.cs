using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Service.Services.Payment
{
    public class PaymentModel
    {
        public decimal Total { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirateDateMonth { get; set; }
        public string ExpirateDateYear { get; set; }
        public string CardCVV2 { get; set; }
        public string MerchantOrderId { get; set; }
        public string OkUrl { get; set; }
        public string FailUrl { get; set; }
        public int Installment { get; set; }
        public string IpAdress { get; set; }
    }
}
