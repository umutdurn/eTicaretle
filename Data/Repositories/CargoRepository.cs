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
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CargoRepository(AppDbContext context) : base(context)
        {
        }

        public ICollection<Cargo> GetAllCargos()
        {
            return _appDbContext.Cargo.ToList();
        }

        public Cargo GetSingleCargo(int id)
        {
            return _appDbContext.Cargo.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Cargo> GetAllCargosForCountry(int countryId)
        {
            var country = _appDbContext.Country.FirstOrDefault(x => x.Id == countryId);

            return _appDbContext.Cargo.Where(x => x.Countries.Contains(country)).ToList();
        }
    }
}
