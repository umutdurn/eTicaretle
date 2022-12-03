using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICargoRepository: IRepository<Cargo>
    {
        ICollection<Cargo> GetAllCargos();
        ICollection<Cargo> GetAllCargosForCountry(int countryId);
        Cargo GetSingleCargo(int id);
    }
}
