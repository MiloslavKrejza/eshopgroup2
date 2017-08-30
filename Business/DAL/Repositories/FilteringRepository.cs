using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.Business.Enums;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Entities;
using Trainee.Core.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class FilteringRepository : IFilteringRepository
    {
        readonly string _connectionString;
        public FilteringRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Using provided QueryParametersWrapper filters products and gets all related infromation
        /// </summary>
        /// <param name="parameters">Wrapper that contains all parameters for filtering and ordering</param>
        /// <returns>Filtered products and related information</returns>
        public QueryResultWrapper FilterProducts(QueryParametersWrapper parameters)
        {
            Dictionary<int, Author> authors = new Dictionary<int, Author>();
            Dictionary<int, Book> books = new Dictionary<int, Book>();
            Dictionary<int, Language> languages = new Dictionary<int, Language>();
            Dictionary<int, Format> formats = new Dictionary<int, Format>();
            Dictionary<int, ProductState> productStates = new Dictionary<int, ProductState>();
            Dictionary<int, Category> categories = new Dictionary<int, Category>();
            Dictionary<int, Publisher> publishers = new Dictionary<int, Publisher>();
            // {O,null} is a country that is assigned to authors without set country, it is removed before conversion to List
            Dictionary<int, Country> countries = new Dictionary<int, Country> { { 0, null } };
            decimal maxPrice = 0;
            decimal minPrice = decimal.MaxValue;
            //Preparing query that will select all products from selected category and its child categories ignoring all filters
            string queryStringAll = $"SELECT * FROM dbo.ProductFilter({parameters.CategoryId},NULL,NULL,NULL,NULL,NULL,NULL) ORDER BY ProductId ASC";
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryStringAll, conn);
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    int previousProductId = 0;
                    Book book = null;
                    //Adding all entities that filtered pruducts might consist of into their dictionaries
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
                            languages.Add(languageId, new Language() { Id = languageId, Name = (string)reader["LanguageName"] });
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
                    if (book != null)
                    {   //Adding last book into the dictionary
                        books.Add(book.BookId, book);
                    }
                }

            }
            List<ProductBO> resultProducts = new List<ProductBO>();

            string orderParameter;
            //Choosing ordering type
            switch (parameters.SortingParameter)
            {
                case Business.Enums.SortingParameter.Date:
                    orderParameter = "DateAdded";
                    break;
                case Business.Enums.SortingParameter.Price:
                    orderParameter = "Price";
                    break;
                case Business.Enums.SortingParameter.Rating:
                    orderParameter = "AverageRating";
                    break;
                case Business.Enums.SortingParameter.Name:
                    orderParameter = "ProductName";
                    break;
                default:
                    orderParameter = "AverageRating";
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
            //Preparing query for filter
            string queryString = $"SELECT * FROM [dbo].[ProductFilterLite] (@category, @languages,  @formats, @publishers , @authors  , @minPrice, @maxPrice) ORDER BY {orderParameter} {orderType};";
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@category", parameters.CategoryId);
                command.Parameters.AddWithValue("@languages", parameters.Languages != null ? (object)string.Join(",", parameters.Languages) : DBNull.Value);
                command.Parameters.AddWithValue("@formats", parameters.Formats != null ? (object)string.Join(",", parameters.Formats) : DBNull.Value);
                command.Parameters.AddWithValue("@publishers", parameters.Publishers != null ? (object)string.Join(",", parameters.Publishers) : DBNull.Value);
                command.Parameters.AddWithValue("@authors", parameters.Authors != null ? (object)string.Join(",", parameters.Authors) : DBNull.Value);
                command.Parameters.AddWithValue("@minPrice", parameters.MinPrice != null ? (object)parameters.MinPrice : DBNull.Value);
                command.Parameters.AddWithValue("@maxPrice", parameters.MaxPrice != null ? (object)parameters.MaxPrice : DBNull.Value);
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    ProductBO currentProduct = null;
                    //read all products
                    while (reader.Read())
                    {
                        //Has to be reviewed if column names change by chance
                        int id = (int)reader["ProductId"];

                        if (currentProduct != null)
                        {
                            resultProducts.Add(currentProduct);
                        }
                        //Getting columns from DB
                        int bookId = (int)reader["BookId"];
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
                        //creating the product using entities in dictionaries and provided ids
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

                    if (currentProduct != null)
                    {
                        resultProducts.Add(currentProduct);
                    }
                    conn.Close();
                }

            }
            //removing dummy country with id 0
            countries.Remove(0);
            var result = new QueryResultWrapper
            {
                Authors = new List<Author>(authors.Values),
                Publishers = new List<Publisher>(publishers.Values),
                Languages = new List<Language>(languages.Values),
                Formats = new List<Format>(formats.Values),
                ResultCount = resultProducts.Count,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                Products = resultProducts.AsQueryable()
                    .Skip((parameters.PageNum - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToList()
            };
            return result;

        }
        /// <summary>
        /// Method that gets first n products from database sorted by specified column. 
        /// </summary>
        /// <param name="parameter">Column to sort by</param>
        /// <param name="type">Sorting order (ASC/DESC)</param>
        /// <param name="count">Number of products to fetch</param>
        /// <param name="categoryId">Category of fetched products</param>
        /// <param name="timeOffset">Maximal age (in days) of orders that are included in calculation of the amount of sold products</param>
        /// <returns>Requested products</returns>
        public IQueryable<ProductBO> GetProducts(SortingParameter parameter, SortType type, int count, int categoryId, int? timeOffset = null)
        {
            string sortParameter;
            string sortType;
            switch (parameter)
            {
                case SortingParameter.Date:
                    sortParameter = "DateAdded";
                    break;
                case SortingParameter.Price:
                    sortParameter = "Price";
                    break;
                case SortingParameter.Rating:
                    sortParameter = "AverageRating";
                    break;
                case SortingParameter.Count:
                    sortParameter = "Count";
                    break;
                case SortingParameter.Name:
                    sortParameter = "ProductName";
                    break;
                default:
                    sortParameter = "AverageRating";
                    break;
            }
            switch (type)
            {
                case SortType.Asc:
                    sortType = "ASC";
                    break;
                case SortType.Desc:
                    sortType = "DESC";
                    break;
                default:
                    sortType = "DESC";
                    break;
            }
            var queryString = $"SELECT * FROM dbo.Frontpage(@timeOffset,@category) ORDER BY {sortParameter} {sortType}";
            var result = new List<ProductBO>();
            DateTime? date = null;
            if (timeOffset != null)
            {
                date = DateTime.Now.AddDays(-(double)timeOffset).Date;
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@timeOffset", (object)date ?? DBNull.Value);
                command.Parameters.AddWithValue("@category", categoryId);
                conn.Open();

                using (var reader = command.ExecuteReader())
                {
                    int productCount = 0;
                    int previousProductId = 0;
                    ProductBO currentProduct = null;
                    //read all products
                    while (reader.Read() && productCount <= count )
                    {
                        //Has to be reviewed if column names change by chance
                        int id = (int)reader["ProductId"];
                        int bookId = (int)reader["BookId"];

                        //if they differ, stop adding AuthorsBooks and make a new product
                        if (id != previousProductId)
                        {
                            productCount++;
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
                            decimal? avgRating = reader["AverageRating"] != DBNull.Value ? (decimal?)reader["AverageRating"] : null;

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
                        }
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
                    if (result.Count < count)
                    {
                        result.Add(currentProduct);
                    }



                }
            }

            return result.AsQueryable();


        }
    }
}
