using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Core.DAL.Abstraction;
using Trainee.Core.DAL.Context;
using User.DAL.Entities;

namespace Trainee.Core.DAL.Repositories
{
    class CountryRepository : ICountryRepository
    {
        private readonly CountryDbContext _context;
        public CountryRepository(CountryDbContext context)
        {
            _context = context;
        }
        public Country AddCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return country;

        }

        public void DeleteCountry(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Country> GetCountries()
        {
            throw new NotImplementedException();
        }

        public Country GetCountry(int id)
        {
            throw new NotImplementedException();
        }
    }
}
