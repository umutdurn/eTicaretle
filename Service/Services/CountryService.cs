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
    public class CountryService : Services<Country>, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork, IRepository<Country> repository) : base(unitOfWork, repository)
        {
        }

        public List<Country> GetAllIncludeCountry()
        {
            return _unitOfWork.Country.GetAllIncludeCountry();
        }
    }
}
