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
    public class HomeColumnService : Services<HomeColumn>, IHomeColumnService
    {
        public HomeColumnService(IUnitOfWork unitOfWork, IRepository<HomeColumn> repository) : base(unitOfWork, repository)
        {
        }

        public ICollection<HomeColumn> GetAllHomecolumn()
        {
            return _unitOfWork.HomeColumn.GetAllHomecolumn();
        }
    }
}
