using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Core.Abstraction;
using Trainee.Core.DAL.Context;
using Trainee.Core.DAL.Entities;

namespace Trainee.Core.DAL.Repositories
{
    public class CountryRepository : ICountryRepository
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
            var country = _context.Countries.FirstOrDefault(c => c.Id == id);
            _context.Countries.Remove(country);
            _context.SaveChanges();

        }

        public IQueryable<Country> GetCountries()
        {
            return _context.Countries.AsQueryable();
        }

        public Country GetCountry(int id)
        {
            var country = _context.Countries.Where(c => c.Id == id).FirstOrDefault();
            return country;
        }

        public Country GetCountryByCode(string code)
        {
            var country = _context.Countries.Where(c => c.CountryCode == code).FirstOrDefault();
            return country;
        }
    }
}
