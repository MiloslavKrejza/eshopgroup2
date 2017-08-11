using Trainee.Core.Abstraction;
using Alza.Core.Module.Http;
using Trainee.Core.DAL.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Trainee.Core.Business
{
    /// <summary>
    /// This service provides access to countries
    /// </summary>
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
        public AlzaAdminDTO<Country> GetCountry(int id)
        {
            try
            {
                var result = _countryRepos.GetCountry(id);
                return AlzaAdminDTO<Country>.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Country>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO<Country> GetCountry(string countryCode)
        {
            try
            {
                var result = _countryRepos.GetCountryByCode(countryCode);
                return AlzaAdminDTO<Country> .Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Country> .Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /****************************************/
        /*         GET ALL COUNTRIES            */
        /****************************************/

        /// <summary>
        /// Provides a list of all countries
        /// </summary>
        /// <returns>DTO containing a list of all available countries</returns>
        public AlzaAdminDTO<ICollection<Country>> GetAllCountries()
        {
            try
            {
                var result = _countryRepos.GetCountries().ToList();
                
                return AlzaAdminDTO<ICollection<Country>>.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<ICollection<Country>> .Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

    }
}
