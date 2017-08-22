using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Entities;
using Trainee.Core.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class FilteringRepository : IFilteringRepository
    {
        string _connectionString;
        public FilteringRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public QueryResultWrapper FilterProducts(QueryParametersWrapper parameters)
        {
            Dictionary<int, Author> authors = new Dictionary<int, Author>();
            Dictionary<int, Book> books = new Dictionary<int, Book>();
            Dictionary<int, Language> languages = new Dictionary<int, Language>();
            Dictionary<int, Format> formats = new Dictionary<int, Format>();
            Dictionary<int, ProductState> productStates = new Dictionary<int, ProductState>();
            Dictionary<int, Category> categories = new Dictionary<int, Category>();
            Dictionary<int, Publisher> publishers = new Dictionary<int, Publisher>();
            Dictionary<int, Country> countries = new Dictionary<int, Country>() { { 0, null } };
            decimal maxPrice = 0;
            decimal minPrice = decimal.MaxValue;
            string queryStringAll = $"SELECT * FROM dbo.ProductFilter({parameters.CategoryId},NULL,NULL,NULL,NULL,NULL,NULL) ORDER BY ProductId ASC";
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryStringAll, conn);
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    int previousProductId = 0;
                    Book book = null;
                    while (reader.Read())
                    {
                        int id = (int)reader["ProductId"];
                        if (book != null && previousProductId != id)
                        {
                            books.Add(book.BookId, book);
                            book = null;
                        }
                        previousProductId = id;
                        int categoryId = (int)reader["CategoryId"];
                        if (!categories.ContainsKey(categoryId))
                        {
                            categories.Add(categoryId, new Category()
                            {
                                Id = categoryId,
                                Name = (string)reader["CategoryName"]
                            });
                        }
                        int countryId = reader["AuthorCountryId"] != DBNull.Value ? (int)reader["AuthorCountryId"] : 0;
                        if (!countries.ContainsKey(countryId))
                        {
                            countries.Add(countryId, new Country() { Name = (string)reader["AuthorCountryName"], Id = (int)countryId, CountryCode = (string)reader["AuthorCountryCode"] });
                        }
                        int formatId = (int)reader["FormatId"];
                        if (!formats.ContainsKey(formatId))
                        {
                            formats.Add(formatId, new Format()
                            {
                                Id = formatId,
                                Name = (string)reader["FormatName"]
                            });
                        }
                        int stateId = (int)reader["ProductStateId"];
                        if (!productStates.ContainsKey(stateId))
                        {
                            productStates.Add(stateId, new ProductState() { Id = stateId, Name = (string)reader["ProductStateName"] });
                        }
                        int authorId = (int)reader["AuthorId"];
                        if (!authors.ContainsKey(authorId))
                        {
                            authors.Add(authorId, new Author()
                            {
                                AuthorId = authorId,
                                CountryId = countryId,
                                Country = countries[countryId],
                                Name = (string)reader["AuthorName"],
                                Surname = (string)reader["AuthorSurname"]
                            });
                        };
                        int bookId = (int)reader["BookId"];
                        if (book == null && !books.ContainsKey(bookId))
                        {
                            book = new Book() { BookId = bookId, Name = (string)reader["BookName"], Annotation = (string)reader["BookAnnotation"], AuthorsBooks = new List<AuthorBook>() };
                        }
                        if (!books.ContainsKey(bookId))
                        {
                            book.AuthorsBooks.Add(new AuthorBook() { Book = book, BookId = bookId, AuthorId = authorId, Author = authors[authorId] });
                        }
                        int languageId = (int)reader["LanguageId"];
                        if (!languages.ContainsKey(languageId))
                        {
                            languages.Add(languageId, new Language() { Id = languageId, Name = (string)reader["LanguageName"]});
                        }
                        int publisherId = (int)reader["PublisherId"];
                        if (!publishers.ContainsKey(publisherId))
                        {
                            publishers.Add(publisherId, new Publisher() { Id = publisherId, Name = (string)reader["PublisherName"] });
                        }
                        decimal price = (decimal)reader["Price"];
                        minPrice = minPrice < price ? minPrice : price;
                        maxPrice = maxPrice > price ? maxPrice : price;

                    }
                }

            }
            List<ProductBO> resultProducts = new List<ProductBO>();
            string orderParameter;
            switch (parameters.SortingParameter)
            {
                case Business.Enums.SortingParameter.Date:
                    orderParameter = "DateAdded";
                    break;
                case Business.Enums.SortingParameter.Price:
                    orderParameter = "Price";
                    break;
                case Business.Enums.SortingParameter.Rating:
                    orderParameter = "AvgRating";
                    break;
                case Business.Enums.SortingParameter.Name:
                    orderParameter = "ProductName";
                    break;
                default:
                    orderParameter = "AvgRating";
                    break;
            }
            string orderType;
            switch (parameters.SortingType)
            {
                case Business.Enums.SortType.Asc:
                    orderType = "ASC";
                    break;
                case Business.Enums.SortType.Desc:
                    orderType = "DESC";
                    break;
                default:
                    orderType = "DESC";
                    break;
            }
            string queryString = $"SELECT * FROM [dbo].[ProductFilterLite] (@category, @languages,  @formats, @publishers , @authors  , @minPrice, @maxPrice) ORDER BY {orderParameter} {orderType};";
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@category", parameters.CategoryId);
                command.Parameters.AddWithValue("@languages", ((parameters.Languages != null) ? (object)string.Join(",", parameters.Languages) : DBNull.Value));
                command.Parameters.AddWithValue("@formats", ((parameters.Formats != null) ? (object)string.Join(",", parameters.Formats) : DBNull.Value));
                command.Parameters.AddWithValue("@publishers", ((parameters.Publishers != null) ? (object)string.Join(",", parameters.Publishers) : DBNull.Value));
                command.Parameters.AddWithValue("@authors", ((parameters.Authors != null) ? (object)string.Join(",", parameters.Authors) : DBNull.Value));
                command.Parameters.AddWithValue("@minPrice", ((parameters.MinPrice != null) ? (object)parameters.MinPrice : DBNull.Value));
                command.Parameters.AddWithValue("@maxPrice", ((parameters.MaxPrice != null) ? (object)parameters.MaxPrice : DBNull.Value));
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    ProductBO currentProduct = null;
                    //read all products
                    while (reader.Read())
                    {
                        //Has to be reviewed if column names change by chance
                        int id = (int)reader["ProductId"];
                        int bookId = (int)reader["BookId"];
                        if (currentProduct != null)
                        {
                            resultProducts.Add(currentProduct);
                        }
                        //Getting columns from DB
                        string productName = (string)reader["ProductName"];
                        string text = reader["Text"] != DBNull.Value ? (string)reader["Text"] : null;
                        int categoryId = (int)reader["CategoryId"];
                        int publisherId = (int)reader["PublisherId"];
                        int languageId = (int)reader["LanguageId"];
                        int stateId = (int)reader["ProductStateId"];
                        int formatId = (int)reader["FormatId"];
                        string ean = reader["EAN"] != DBNull.Value ? (string)reader["EAN"] : null;
                        string isbn = reader["ISBN"] != DBNull.Value ? (string)reader["ISBN"] : null;
                        string picAddress = reader["PicAddress"] != DBNull.Value ? (string)reader["PicAddress"] : null;
                        decimal price = (decimal)reader["Price"];
                        int? year = reader["Year"] != DBNull.Value ? (int?)reader["Year"] : null;
                        int? pageCount = reader["PageCount"] != DBNull.Value ? (int?)reader["PageCount"] : null;
                        decimal? avgRating = reader["AverageRating"] != DBNull.Value ? (decimal?)reader["AverageRating"] : null;
                        DateTime dateAdded = (DateTime)reader["DateAdded"];
                        currentProduct = new ProductBO()
                        {
                            Id = id,
                            Name = productName,
                            BookId = bookId,
                            Book = books[bookId],
                            Category = categories[categoryId],
                            CategoryId = categoryId,
                            DateAdded = dateAdded,
                            EAN = ean,
                            ISBN = isbn,
                            Format = formats[formatId],
                            FormatId = formatId,
                            Language = languages[languageId],
                            LanguageId = languageId,
                            PageCount = pageCount,
                            PicAddress = picAddress,
                            Price = price,
                            Publisher = publishers[publisherId],
                            PublisherId = publisherId,
                            Text = text,
                            Year = year,
                            State = productStates[stateId],
                            StateId = stateId,
                            AverageRating = avgRating
                        };
                    }
                    resultProducts.Add(currentProduct);
                    conn.Close();
                }
                
            }
            countries.Remove(0);
            var result = new QueryResultWrapper();
            result.Authors = new List<Author>(authors.Values);
            result.Publishers = new List<Publisher>(publishers.Values);
            result.Languages = new List<Language>(languages.Values);
            result.Formats = new List<Format>(formats.Values);
            result.ResultCount = resultProducts.Count;
            result.MaxPrice = maxPrice;
            result.MinPrice = minPrice;
            result.Products = resultProducts.AsQueryable().Skip((parameters.PageNum - 1) * parameters.PageSize).Take(parameters.PageSize).ToList();
            return result;

        }
    }
}
