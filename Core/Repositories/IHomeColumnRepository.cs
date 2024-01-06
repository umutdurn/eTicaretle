using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IHomeColumnRepository : IRepository<HomeColumn>
    {
        ICollection<HomeColumn> GetAllHomecolumn();
        HomeColumn GetIdHomecolumn(int id);
    }
}
