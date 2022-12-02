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
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public ICollection<Cart> GetAllCartInclude(string cookieId)
        {
            return _appDbContext.Cart.Include(x => x.Product).Where(x => x.CookieId == cookieId).ToList();
        }
    }
}
