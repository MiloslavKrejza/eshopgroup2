using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Context;
using Trainee.Catalogue.DAL.Entities;
using System.Data.SqlClient;
using System.Diagnostics;
using Trainee.Core.DAL.Entities;

namespace Trainee.Catalogue.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogueDbContext _context;
        private readonly string _connectionString;


        public ProductRepository(CatalogueDbContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
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

            List<ProductBase> result = new List<ProductBase>();
            //ADO.NET is used to work with database views
            string queryString = "SELECT * FROM CompleteProducts ORDER BY ProductId ASC";
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, conn);
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    ProductBase currentProduct = null;
                    int previousProductId = 0;

                    //read all products
                    while (reader.Read())
                    {
                        //Has to be reviewed if column names change by chance
                        int id = (int)reader["ProductId"];
                        int bookId = (int)reader["BookId"];

                        //if they differ, stop adding AuthorsBooks and make a new product
                        if (id != previousProductId)
                        {
                            previousProductId = id;
                            if (currentProduct != null)
                            {
                                result.Add(currentProduct);
                            }
                            //Getting columns from DB
                            string productName = (string)reader["ProductName"];
                            string text = reader["Text"] != DBNull.Value ? (string)reader["Text"] : null;

                            string bookName = (string)reader["BookName"];
                            string bookAnnotation = reader["BookAnnotation"] != DBNull.Value ? (string)reader["BookAnnotation"] : null;
                            int catagoryId = (int)reader["CategoryId"];
                            string categoryName = (string)reader["CategoryName"];
                            int publisherId = (int)reader["PublisherId"];
                            string publisherName = (string)reader["PublisherName"];
                            int languageId = (int)reader["LanguageId"];
                            string languageName = (string)reader["LanguageName"];
                            int stateId = (int)reader["ProductStateId"];
                            string stateName = (string)reader["ProductStateName"];
                            int formatId = (int)reader["FormatId"];
                            string formatName = (string)reader["FormatName"];
                            string ean = reader["EAN"] != DBNull.Value ? (string)reader["EAN"] : null;
                            string isbn = reader["ISBN"] != DBNull.Value ? (string)reader["ISBN"] : null;
                            string picAddress = reader["PicAddress"] != DBNull.Value ? (string)reader["PicAddress"] : null;
                            decimal price = (decimal)reader["Price"];
                            int? year = reader["Year"] != DBNull.Value ? (int?)reader["Year"] : null;
                            int? pageCount = reader["PageCount"] != DBNull.Value ? (int?)reader["PageCount"] : null;
                            DateTime dateAdded = (DateTime)reader["DateAdded"];

                            //creating principal entities
                            Publisher pub = new Publisher() { Id = publisherId, Name = publisherName };
                            Category cat = new Category() { Name = categoryName, Id = catagoryId };
                            Language lang = new Language() { Id = languageId, Name = languageName };
                            Format form = new Format() { Id = formatId, Name = formatName };
                            ProductState state = new ProductState() { Id = stateId, Name = stateName };
                            Book book = new Book() { Name = bookName, Annotation = bookAnnotation, BookId = bookId, AuthorsBooks = new List<AuthorBook>() };

                            currentProduct = new ProductBase()
                            {
                                Id = id,
                                Name = productName,
                                BookId = bookId,
                                Book = book,
                                Category = cat,
                                CategoryId = catagoryId,
                                DateAdded = dateAdded,
                                EAN = ean,
                                ISBN = isbn,
                                Format = form,
                                FormatId = formatId,
                                Language = lang,
                                LanguageId = languageId,
                                PageCount = pageCount,
                                PicAddress = picAddress,
                                Price = price,
                                Publisher = pub,
                                PublisherId = publisherId,
                                Text = text,
                                Year = year,
                                State = state,
                                StateId = stateId
                            };
                        }
                        //else add a new authorbook, author
                        int? countryId = reader["AuthorCountryId"] != DBNull.Value ? (int?)reader["AuthorCountryId"] : null;
                        Country country = countryId != null ? new Country() { Name = (string)reader["AuthorCountryName"], Id = (int)countryId, CountryCode = (string)reader["AuthorCountryCode"] } : null;
                        int authorId = (int)reader["AuthorId"];
                        string authorName = (string)reader["AuthorName"];
                        string authorSurname = (string)reader["AuthorSurname"];
                        Author author = new Author() { Country = country, CountryId = countryId, Name = authorName, Surname = authorSurname, AuthorId = authorId };
                        AuthorBook ab = new AuthorBook { Author = author, AuthorId = authorId, Book = currentProduct.Book, BookId = bookId };
                        currentProduct.Book.AuthorsBooks.Add(ab);

                    }
                    result.Add(currentProduct);


                }
            }

            return result.AsQueryable();
        }

        public ProductBase GetProduct(int id)
        {
            var result = _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.Book)
                .Include(p => p.Category)
                .Include(p => p.Format)
                .Include(p => p.Language)
                .Include(p => p.Publisher)
                .Include(p => p.State)
                .FirstOrDefault();

            //Unknown id would throw an NullReferenceException
            if (ReferenceEquals(result, null))
                return result;
            
            //manually adding, as EF core does not support many to many (and therefore ThenInclude is buggy)
            result.Book.AuthorsBooks = _context.AuthorsBooks.Where(ab => ab.BookId == result.BookId).ToList();

            foreach (AuthorBook ab in result.Book.AuthorsBooks)
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
