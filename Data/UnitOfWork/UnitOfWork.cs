using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private HomeColumnRepository homeColumnRepository;
        private ProductRepository productRepository;

        private CartRepository cartRepository;

        public IHomeColumnRepository HomeColumn => homeColumnRepository = homeColumnRepository ?? new HomeColumnRepository(_appDbContext);
        public IProductRepository Product => productRepository = productRepository ?? new ProductRepository(_appDbContext);

        public ICartRepository Cart => cartRepository = cartRepository ?? new CartRepository(_appDbContext);

        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _appDbContext.DisposeAsync();
        }
    }
}
