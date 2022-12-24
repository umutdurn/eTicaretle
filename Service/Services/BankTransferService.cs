using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BankTransferService : Services<BankTransfer>, IBankTransferService
    {
        public BankTransferService(IUnitOfWork unitOfWork, IRepository<BankTransfer> repository) : base(unitOfWork, repository)
        {
        }

        public List<BankTransfer> GetAllBankTransfer()
        {
            return _unitOfWork.BankTransfer.GetAllBankTransfer();
        }
    }
}
