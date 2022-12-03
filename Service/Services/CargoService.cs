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
    public class CargoService : Services<Cargo>, ICargoService
    {
        public CargoService(IUnitOfWork unitOfWork, IRepository<Cargo> repository) : base(unitOfWork, repository)
        {
        }

        public ICollection<Cargo> GetAllCargos()
        {
            return _unitOfWork.Cargo.GetAllCargos();
        }

        public ICollection<Cargo> GetAllCargosForCountry(int countryId)
        {
            return _unitOfWork.Cargo.GetAllCargosForCountry(countryId);
        }

        public Cargo GetSingleCargo(int id)
        {
            return _unitOfWork.Cargo.GetSingleCargo(id);
        }
    }
}
