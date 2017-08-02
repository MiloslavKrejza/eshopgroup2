using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Context;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.DAL.Repositories
{
    class LanguageRepository : ILanguageRepository
    {
        private readonly CatalogueDbContext _context;

        public LanguageRepository(CatalogueDbContext context)
        {
            _context = context;
        }

        public Language AddLanguage(Language lang)
        {
            _context.Languages.Add(lang);
            _context.SaveChanges();
            return lang;
        }

        public void DeleteLanguage(int id)
        {
            var lang = _context.Languages.FirstOrDefault(l => l.Id == id);
            _context.Languages.Remove(lang);
            _context.SaveChanges();
        }

        public IQueryable<Language> GetAllLangs()
        {
            return _context.Languages.AsQueryable();
        }

        public Language GetLanguage(int id)
        {
            return _context.Languages.FirstOrDefault(c => c.Id == id);
        }

        public Language UpdateLanguage(Language lang)
        {
            var oldLang = GetLanguage(lang.Id);
            _context.Entry(oldLang).CurrentValues.SetValues(lang);
            _context.SaveChanges();
            return lang;
        }
    }
}
