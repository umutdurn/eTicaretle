using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Order : EntityBase
    {
        public Order()
        {
            Cart = new Collection<Cart>();
        }

        public ICollection<Cart> Cart { get; set; }
        public bool Situation { get; set; }
        public Payment Payment { get; set; }
        public string OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public string? OrderNote { get; set; }
        public DateTime OrderDate { get; set; }
        public Cargo Cargo { get; set; }
        public string? ThreeDError { get; set; }
        public OrderSituation OrderSituation { get; set; }
        public bool GiftBox { get; set; }
        public string? GiftTextOne { get; set; }
        public string? GiftTextTwo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GSM { get; set; }
        public string Email { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public string Adress { get; set; }
        public string? PaymentAtDoorType { get; set; }
        public string? PaymentErrorCode { get; set; }
        public string? PaymentErrorMessage { get; set; }
        public string? PaymentResult { get; set; }
        public Member Member { get; set; }
    }
}
