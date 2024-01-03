using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IOrderService : IService<Order>
    {
        List<Order> GetAllInclude();
        Order GetAllIncludeOrderId(string orderId);
        Order GetAllIncludeId(int id);
    }
}
