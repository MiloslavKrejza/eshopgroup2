using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            string queryString = $"SELECT * FROM [dbo].[ProductFilter] (@category, @languages,  @formats, @publishers , @authors  , @minPrice, @maxPrice) ORDER BY {orderParameter} {orderType};";
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
                                resultProducts.Add(currentProduct);
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
                            decimal? avgRating = reader["AverageRating"] != DBNull.Value ? (decimal?)reader["AverageRating"] : null;
                            DateTime dateAdded = (DateTime)reader["DateAdded"];

                            //creating principal entities
                            Publisher pub = new Publisher() { Id = publisherId, Name = publisherName };
                            Category cat = new Category() { Name = categoryName, Id = catagoryId };
                            Language lang = new Language() { Id = languageId, Name = languageName };
                            Format form = new Format() { Id = formatId, Name = formatName };
                            ProductState state = new ProductState() { Id = stateId, Name = stateName };
                            Book book = new Book() { Name = bookName, Annotation = bookAnnotation, BookId = bookId, AuthorsBooks = new List<AuthorBook>() };

                            currentProduct = new ProductBO()
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
                                StateId = stateId,
                                AverageRating = avgRating
                            };
                        };

                        //add a new authorbook, author
                        int? countryId = reader["AuthorCountryId"] != DBNull.Value ? (int?)reader["AuthorCountryId"] : null;
                        Country country = countryId != null ? new Country() { Name = (string)reader["AuthorCountryName"], Id = (int)countryId, CountryCode = (string)reader["AuthorCountryCode"] } : null;
                        int authorId = (int)reader["AuthorId"];
                        string authorName = (string)reader["AuthorName"];
                        string authorSurname = (string)reader["AuthorSurname"];
                        Author author = new Author() { Country = country, CountryId = countryId, Name = authorName, Surname = authorSurname, AuthorId = authorId };
                        AuthorBook ab = new AuthorBook { Author = author, AuthorId = authorId, Book = currentProduct.Book, BookId = bookId };
                        currentProduct.Book.AuthorsBooks.Add(ab);

                    }
                    resultProducts.Add(currentProduct);


                }
                var result = new QueryResultWrapper() { Products = resultProducts };
                return result;
            }

        }
    }
}
