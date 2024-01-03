using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IReturnOrderService : IService<ReturnOrder>
    {
        List<ReturnOrder> GetAllInclude();
        ReturnOrder GetAllIncludeById(int id);
    }
}
