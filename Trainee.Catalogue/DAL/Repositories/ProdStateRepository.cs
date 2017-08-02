using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Context;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.DAL.Repositories
{
    class ProdStateRepository : IProdStateRepository
    {
        private readonly CatalogueDbContext _context;

        public ProdStateRepository(CatalogueDbContext context)
        {
            _context = context;
        }

        public ProductState AddProductState(ProductState productState)
        {
            _context.ProductStates.Add(productState);
            _context.SaveChanges();
            return productState;
        }

        public void DeleteProductState(int id)
        {
            var state = _context.ProductStates.FirstOrDefault(p => p.Id == id);
            _context.ProductStates.Remove(state);
            _context.SaveChanges();
        }

        public IQueryable<ProductState> GetAllProductStates()
        {
            return _context.ProductStates.AsQueryable();
        }

        public ProductState GetProductState(int id)
        {
            return _context.ProductStates.FirstOrDefault(p => p.Id == id);
        }

        public ProductState UpdateProductState(ProductState productState)
        {
            var oldState = _context.ProductStates.FirstOrDefault(p => p.Id == productState.Id);
            _context.Entry(oldState).CurrentValues.SetValues(productState);
            _context.SaveChanges();
            return productState;
        }
    }
}
