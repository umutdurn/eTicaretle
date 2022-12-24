using Core.Models;
using Core.Repositories;
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
    }
}
