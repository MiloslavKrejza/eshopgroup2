using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Context;
using Trainee.Catalogue.DAL.Entities;
using System.Diagnostics;

namespace Trainee.Catalogue.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogueDbContext _context;

        public ProductRepository(CatalogueDbContext context)
        {
            _context = context;
        }

        public ProductBase AddProduct(ProductBase product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IQueryable<ProductBase> GetAllProducts()
        {
            var result = _context.Products
                .Include(p => p.Book)
                   //.ThenInclude(b => b.AuthorsBooks)
                     //   .ThenInclude(ab => ab.Author)
                   //.ThenInclude(b => b.Country)
                .Include(p => p.Category)
                .Include(p => p.Format)
                .Include(p => p.Language)
                .Include(p => p.Publisher)
                .Include(p => p.State)
                .AsQueryable().ToList();

            int i = 0;
             foreach(var item in result)
             {
                item.Book = _context.Books.Where(b => b.BookId == item.BookId).FirstOrDefault();
                 item.Book.AuthorsBooks = _context.AuthorsBooks.Where(ab => ab.BookId == item.BookId).ToList();

                 foreach (AuthorBook ab in item.Book.AuthorsBooks)
                 {
                     ab.Author = _context.Authors.Where(a => a.AuthorId == ab.AuthorId).FirstOrDefault();
                 }
                i++;
                Debug.WriteLine($"{i}: {item.Book.Name} – {item.Name}");
                 
             }

            return result.AsQueryable();
        }

        public ProductBase GetProduct(int id)
        {
            var result = _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.Book)
                    //.ThenInclude(b => b.AuthorsBooks)
                       // .ThenInclude(ab => ab.Author)
                        //.ThenInclude(a => a.Country)
                .Include(p => p.Category)
                .Include(p => p.Format)
                .Include(p => p.Language)
                .Include(p => p.Publisher)
                .Include(p => p.State)
                .FirstOrDefault();

            
            result.Book.AuthorsBooks = _context.AuthorsBooks.Where(ab => ab.BookId == result.BookId).ToList();
            
            foreach(AuthorBook ab in result.Book.AuthorsBooks)
            {
                ab.Author = _context.Authors.Where(a => a.AuthorId == ab.AuthorId).FirstOrDefault();
            }

            return result;
        }

        public ProductBase UpdateProduct(ProductBase product)
        {
            var oldProduct = GetProduct(product.Id);
            _context.Entry(oldProduct).CurrentValues.SetValues(product);
            _context.SaveChanges();
            return product;
        }
    }
}
