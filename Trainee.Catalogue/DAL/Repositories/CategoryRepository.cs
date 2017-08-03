using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Entities;
using Trainee.Catalogue.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Trainee.Catalogue.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogueDbContext _context;

        public CategoryRepository(CatalogueDbContext context)
        {
            _context = context;
        }

        public Category AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public IQueryable<Category> GetAllCategories()
        {
            return _context.Categories.AsQueryable();
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories
                .Where(c => c.Id == id)
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .FirstOrDefault();
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var oldCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            _context.Entry(oldCategory).CurrentValues.SetValues(category);
            _context.SaveChanges();
            return category;
        }
    }
}
