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
    public class BankTransferRepository : Repository<BankTransfer>, IBankTransferRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public BankTransferRepository(AppDbContext context) : base(context)
        {
        }

        public List<BankTransfer> GetAllBankTransfer()
        {
            return _appDbContext.BankTransfer.Include(x => x.Bank).AsNoTracking().AsQueryable().ToList();
        }
    }
}
