using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class HomeColumnRepository : Repository<HomeColumn>, IHomeColumnRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public HomeColumnRepository(AppDbContext context) : base(context)
        {

        }

        public ICollection<HomeColumn> GetAllHomecolumn()
        {
            return _appDbContext.HomeColumns.Include(x => x.Screen).Include(x => x.ColumnDetail).ToList();
        }
    }
}
