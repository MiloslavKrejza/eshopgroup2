using Eshop2.Models.CatalogueViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.Business;
using Trainee.Business.DAL.Entities;

namespace Eshop2.Abstraction
{
    public class BookLoader
    {
        private readonly BusinessService _businessService;

        public BookLoader(BusinessService service)
        {
            _businessService = service;
        }

        public BookViewModel LoadBookModel(int id)
        {
            var dto = _businessService.GetProduct(id);
            if (!dto.isOK || dto.isEmpty)
            {
                return null;
            }

            ProductBO product = dto.data;

            BookViewModel model = new BookViewModel
            {
                Name = product.Name,

                Category = product.Category,

                Authors = product.Book.AuthorsBooks.Select(ab => ab.Author).ToList(),
                ProductFormat = product.Format.Name,
                AverageRating = product.AverageRating,
                Annotation = product.Book.Annotation,
                ProductText = product.Text,
                PicAddress = product.PicAddress,
                ProductId = product.Id,

                State = product.State.Name,
                Price = product.Price,
                Language = product.Language.Name,

                PageCount = product.PageCount,
                Year = product.Year,
                Publisher = product.Publisher.Name,
                ISBN = product.ISBN,
                EAN = product.EAN,

                Reviews = product.Reviews

            };

            return model;

        }

    }
}
