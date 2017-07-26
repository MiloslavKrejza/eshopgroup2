using Trainee.Core.Abstraction;
using Alza.Core.Module.Http;
using Trainee.Core.DAL.Entities;
using System;
using System.Linq;

namespace Trainee.Core.Business
{
    public class CountryService
    {
        private readonly ICountryRepository _countryRepos;

        public CountryService(ICountryRepository countryRepos)
        {
            _countryRepos = countryRepos;
        }


        /****************************************/
        /*         GET COUNTRY BY ID            */
        /****************************************/
        
        /// <summary>
        /// Provides a country with a specified id
        /// </summary>
        /// <param name="id">Country id</param>
        /// <returns>DTO containing the country with matching id</returns>
        public AlzaAdminDTO GetCountry(int id)
        {
            try
            {
                var result = _countryRepos.GetCountry(id);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /****************************************/
        /*         GET ALL COUNTRIES            */
        /****************************************/

        /// <summary>
        /// Provides a list of all countries
        /// </summary>
        /// <returns>DTO containing a list of all available countries</returns>
        public AlzaAdminDTO GetAllCountries()
        {
            try
            {
                var result = _countryRepos.GetCountries().ToList();
                
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

    }
}
