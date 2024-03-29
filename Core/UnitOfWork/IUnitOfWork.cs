﻿using Core.Repositories;
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
        ICargoRepository Cargo { get; }
        ICountryRepository Country { get; }
        IBankTransferRepository BankTransfer { get; }
        IOrderRepository Order { get; }
        IMemberRepository Member { get; }
        IReturnOrderRepository ReturnOrder { get; }
        ICommentsRepository Comments { get; }
        IColumnDetailRepository ColumnDetail { get; }
        Task CommitAsync();

        void Commit();
    }
}
