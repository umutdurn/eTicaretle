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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public Product UpdateProductGetAllInclude(int id)
        {
            return _appDbContext.Product.Include(x => x.Colors).Include(x => x.Gallery).FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Product> GetAllProductsList()
        {
            return _appDbContext.Product.Where(x => x.Situation == true).ToList();
        }

        public Product GetProductBySeoUrl(string seoUrl)
        {
            return _appDbContext.Product.Include(x => x.Colors).Include(x => x.Gallery).FirstOrDefault(x => x.SeoUrl == seoUrl);
        }
    }
}
