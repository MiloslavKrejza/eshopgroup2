using Alza.Core.Module.Abstraction;
using Alza.Core.Module.Http;
using Alza.Module.Catalog.Dal.Context;
using Alza.Module.Catalog.Dal.Entities;
using Alza.Module.Catalog.Dal.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Repository.Implementation.Ef
{
    public class ProductRepository : IProductRepository
    {

        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public Product Add(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public Product Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            //_context.Products.Remove(entity);
            //_context.SaveChanges();
        }


        public Product Get(int Id)
        {
            var temp = _context.Products.Where(p => p.Id == Id).FirstOrDefault();

            return temp;
        }

        public Product GetFull(int Id)
        {

            var temp = _context.Products.Include(x => x.ProductCategories).Where(p => p.Id == Id).FirstOrDefault();

            return  temp;

        }

        
        public IQueryable<Product> Query()
        {
            var temp = _context.Products.AsQueryable();
            return temp;
        }

        public IQueryable<Product> QueryFull()
        {
            var temp = _context.Products.Include(x => x.ProductCategories).AsQueryable();
            return temp;
        }

        public IQueryable<Product> Query(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }

        public Product GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Product GetBySEOName(string seoName)
        {
            throw new NotImplementedException();
        }

        public Product GetByCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
