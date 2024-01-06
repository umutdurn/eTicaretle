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
    public class ColumnDetailRepository : Repository<ColumnDetail>, IColumnDetailRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public ColumnDetailRepository(AppDbContext context) : base(context)
        {
        }

        public ColumnDetail GetById(int id)
        {
            return _appDbContext.columnDetail.Include(x => x.HomeColumn).FirstOrDefault(x => x.Id == id);
        }
    }
}
