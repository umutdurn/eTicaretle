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
    public class CartService : Services<Cart>, ICartService
    {
        public CartService(IUnitOfWork unitOfWork, IRepository<Cart> repository) : base(unitOfWork, repository)
        {
        }

        public ICollection<Cart> GetAllCartInclude(string cookieId)
        {
            return _unitOfWork.Cart.GetAllCartInclude(cookieId);
        }
    }
}
