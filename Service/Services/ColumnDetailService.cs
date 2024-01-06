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
    public class ColumnDetailService : Services<ColumnDetail>, IColumnDetailService
    {
        public ColumnDetailService(IUnitOfWork unitOfWork, IRepository<ColumnDetail> repository) : base(unitOfWork, repository)
        {
        }

        public ColumnDetail GetById(int id)
        {
            return _unitOfWork.ColumnDetail.GetById(id);
        }
    }
}
