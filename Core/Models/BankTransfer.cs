using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BankTransfer : EntityBase
    {
        public Bank Bank { get; set; }
        public string Buyer { get; set; } // Alıcı
        public string? Branch { get; set; } // Şube
        public string AccountNumber { get; set; } // Hesap Numarası
        public string? IBAN { get; set; } // IBAN
        public string? Swift { get; set; } // Swift

    }
}
