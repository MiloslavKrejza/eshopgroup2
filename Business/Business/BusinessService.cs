using Alza.Core.Module.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Business.Business
{
    public class BusinessService
    {
        ICategoryRelationshipRepository _categoryRelationshipRepository;
        IProductRatingRepository _productRatingRepository;
        IReviewRepository _reviewRepository;
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;


        public AlzaAdminDTO<QueryResultWrapper> GetPage(QueryParametersWrapper parameters)
        {
            var childCategoriesId = _categoryRelationshipRepository.GetAllRelationships().Where(c => c.Id == parameters.CategoryId).Select(c => c.ChildId);
            IQueryable<ProductBase> query = _productRepository.GetAllProducts();
            query = query.Where(p => childCategoriesId.Contains(p.CategoryId));
            decimal minPrice = 0;
            decimal maxPrice = decimal.MaxValue;
            QueryResultWrapper result = new QueryResultWrapper();
            HashSet<Language> languages = new HashSet<Language>();
            HashSet<Publisher> publishers = new HashSet<Publisher>();
            HashSet<Format> formats = new HashSet<Format>();
            HashSet<Author> authors = new HashSet<Author>();
            foreach (ProductBase product in query)
            {
                minPrice = product.Price < minPrice ? product.Price : minPrice;
                maxPrice = product.Price > maxPrice ? product.Price : maxPrice;
                languages.Add(product.Language);
                publishers.Add(product.Publisher);
                formats.Add(product.Format);
                foreach (Author author in product.Book.AuthorsBooks.Select(ab => ab.Author))
                {
                    authors.Add(author);
                }
            }
            result.Authors = authors.OrderBy(a => a.Surname).ToList();
            result.Languages = languages.OrderBy(l => l.Name).ToList();
            result.Publishers = publishers.OrderBy(p => p.Name).ToList();
            result.Formats = formats.OrderBy(f => f.Name).ToList();

            if (parameters.MinPrice != null)
            {
                query = query.Where(p => p.Price >= parameters.MinPrice);
            }
            if (parameters.MaxPrice != null)
            {
                query = query.Where(p => p.Price <= parameters.MaxPrice);
            }
            if (parameters.Languages != null)
            {
                query = query.Where(p => parameters.Languages.Contains(p.LanguageId));
            }
            if (parameters.Publishers != null)
            {
                query = query.Where(p => parameters.Publishers.Contains(p.PublisherId));
            }
            if (parameters.Formats != null)
            {
                query = query.Where(p => parameters.Formats.Contains(p.FormatId));
            }
            if (parameters.Authors != null)
            {
                query = query.Where(p => p.Book.AuthorsBooks.Select(ab => ab.Author).Select(a => a.Id).Intersect(parameters.Authors).Count() > 0);
            }

            IQueryable<int> pIds = query.Select(p => p.Id);
            IQueryable<ProductRating> ratings = _productRatingRepository.GetRatings().Where(pr => pIds.Contains(pr.ProductId));
            //might be bullshite
            var products = query.Join(ratings, p => p.Id, r => r.ProductId, (p, r) => new ProductBO(p, r, null));

            Func<ProductBO, IComparable> sortingParameter;
            switch (parameters.SortingParameter)
            {
                case Enums.SortingParameter.Price:
                    sortingParameter = p => p.Price;
                    break;
                case Enums.SortingParameter.Rating:
                    sortingParameter = p => p.AverageRating;
                    break;
                case Enums.SortingParameter.Date:
                    sortingParameter = p => p.DateAdded;
                    break;
                default:
                    sortingParameter = p => p.AverageRating;
                    break;
            }
            switch (parameters.SortingType)
            {
                case Enums.SortType.Asc:
                    products = products.OrderBy(sortingParameter).AsQueryable();
                    break;
                case Enums.SortType.Desc:
                    products = products.OrderByDescending(sortingParameter).AsQueryable();
                    break;
                default:
                    break;
            }
            result.ResultCount = products.Count();
            products = products.Skip(parameters.PageNum * parameters.PageSize).Take(parameters.PageSize);
            result.Products = products.ToList();
            return AlzaAdminDTO<QueryResultWrapper>.Data(result);

        }
        public AlzaAdminDTO<ProductBO> GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            var rating = _productRatingRepository.GetRating(id);
            var reviews = _reviewRepository.GetReviews().Where(r => r.ProductId == id).ToList();
            var result = new ProductBO(product, rating, reviews);
            return AlzaAdminDTO<ProductBO>.Data(result);

        }
    }
}
