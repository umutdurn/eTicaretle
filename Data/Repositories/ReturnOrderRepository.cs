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
    public class ReturnOrderRepository : Repository<ReturnOrder>, IReturnOrderRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public ReturnOrderRepository(AppDbContext context) : base(context)
        {
        }

        public List<ReturnOrder> GetAllInclude()
        {
            return _appDbContext.ReturnOrder
                .Include(x => x.Order)
                .Include(x => x.SendToBack)
                .Include(x => x.WantToBuy).ToList();


        }

        public ReturnOrder GetAllIncludeById(int id)
        {
            return _appDbContext.ReturnOrder
               .Include(x => x.Order)
               .Include(x => x.SendToBack)
               .Include(x => x.WantToBuy)
               .FirstOrDefault(x => x.Id == id);
        }
    }
}
