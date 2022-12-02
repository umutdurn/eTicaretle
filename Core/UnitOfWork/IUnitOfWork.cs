using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IHomeColumnRepository HomeColumn { get; }
        IProductRepository Product { get; }
        ICartRepository Cart { get; }

        Task CommitAsync();

        void Commit();
    }
}
