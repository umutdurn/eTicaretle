using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IReturnOrderRepository : IRepository<ReturnOrder>
    {
        List<ReturnOrder> GetAllInclude();
        ReturnOrder GetAllIncludeById(int id);
    }
}
