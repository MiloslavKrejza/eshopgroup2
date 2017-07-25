using Trainee.Core.DAL.Abstraction;
using Alza.Core.Module.Http;
using Trainee.Core.DAL.Entities;
using System;

namespace Trainee.Core.Business
{
    class CountryService
    {
        private readonly ICountryRepository _countryRepos;

        public CountryService(ICountryRepository countryRepos)
        {
            _countryRepos = countryRepos;
        }

        public AlzaAdminDTO AddCountry(Country country)
        {
            try
            {
                var result = _countryRepos.AddCountry(country);
                return AlzaAdminDTO.Data(result);
            }
            catch(Exception e)
            {
                return AlzaAdminDTO.Error(new Guid(), e.Message);
            }
        }

    }
}
