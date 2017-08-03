using Alza.Core.Module.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.Business.Wrappers;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Business.Business
{
    public class BusinessService
    {
        IRatedProductRepository _ratedProductRepository;
        IRatedProductRepository _productRepository;

        AlzaAdminDTO GetPage(QueryParametersWrapper parameters)
        {
            //TODO Select products from category
            IQueryable<Product> query = IQueryable<Product>();
            decimal minPrice = 0;
            decimal maxPrice = decimal.MaxValue;
            QueryResultWrapper result = new QueryResultWrapper();
            HashSet<Language> languages = new HashSet<Language>();
            HashSet<Publisher> publishers = new HashSet<Publisher>();
            HashSet<Format> formats = new HashSet<Format>();
            HashSet<Author> authors = new HashSet<Author>();
            foreach (Product product in query)
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
                query = query.Where(p => p.Book.AuthorsBooks.Select(ab=>ab.Author).Select(a => a.Id).Intersect(parameters.Authors).Count() > 0);
            }
            Func<Product, object> sortingParameter;
            //switch (parameters.SortingParameter)
            //{
            //    case Enums.SortingParameter.Price:
            //        sortingParameter = new Func<Product, object>(p => p.Price);
            //        break;
            //    case Enums.SortingParameter.Rating:
            //        sortingParameter = new Func<Product, object>(p => p.Ra);
            //        break;
            //    case Enums.SortingParameter.Date:
            //        break;
            //}

        }
    }
}
