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
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CountryRepository(AppDbContext context) : base(context)
        {
        }

        public List<Country> GetAllIncludeCountry()
        {
            return _appDbContext.Country.Include(x => x.Cargos).ToList();
        }
    }
}
