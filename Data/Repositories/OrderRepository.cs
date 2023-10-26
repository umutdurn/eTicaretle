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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public List<Order> GetAllInclude()
        {
            return _appDbContext.Order
                .Include(x => x.Member)
                .Include(x => x.Payment)
                .Include(x => x.Cargo)
                .Include(x => x.Payment)
                .Include(x => x.OrderSituation)
                .Include(x => x.City)
                .Include(x => x.District)
                .ToList();
        }
    }
}
