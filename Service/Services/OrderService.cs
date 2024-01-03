using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : Services<Order>, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IRepository<Order> repository) : base(unitOfWork, repository)
        {
        }

        public List<Order> GetAllInclude()
        {
            return _unitOfWork.Order.GetAllInclude();
        }

        public Order GetAllIncludeId(int id)
        {
            return _unitOfWork.Order.GetAllIncludeId(id);
        }

        public Order GetAllIncludeOrderId(string orderId)
        {
            return _unitOfWork.Order.GetAllIncludeOrderId(orderId);
        }
    }
}
