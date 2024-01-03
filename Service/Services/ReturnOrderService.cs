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
    public class ReturnOrderService : Services<ReturnOrder>, IReturnOrderService
    {
        public ReturnOrderService(IUnitOfWork unitOfWork, IRepository<ReturnOrder> repository) : base(unitOfWork, repository)
        {
        }

        public List<ReturnOrder> GetAllInclude()
        {
            return _unitOfWork.ReturnOrder.GetAllInclude();
        }

        public ReturnOrder GetAllIncludeById(int id)
        {
            return _unitOfWork.ReturnOrder.GetAllIncludeById(id);
        }
    }
}
