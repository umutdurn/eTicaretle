using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IColumnDetailService : IService<ColumnDetail>
    {
        ColumnDetail GetById(int id);
    }
}
