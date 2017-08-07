using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Business.DAL.Entities
{
    /// <summary>
    /// Business entity created on service-level
    /// </summary>
    public class ProductBO : ProductBase
    {
        public decimal? AverageRating { get; set; }
        public List<Review> Reviews { get; set; }
        internal ProductBO(ProductBase product, ProductRating rating, List<Review> reviews = null) 
        {
           /* if (product.Id != rating.ProductId)
            {
                throw new Exception("Nesouhlasi ID");
            }*/ //Does not work yet

            // AverageRating = rating.AverageRating;
            Reviews = reviews;
            Id = product.Id;
            LanguageId = product.LanguageId;
            PublisherId = product.PublisherId;
            CategoryId = product.CategoryId;
            StateId = product.StateId;
            BookId = product.BookId;

            Name = product.Name;
            PicAddress = product.PicAddress;
            Text = product.Text;
            Price = product.Price;
            PageCount = product.PageCount;
            Year = product.Year;
            ISBN = product.ISBN;
            EAN = product.EAN;

            Format = product.Format;
            State = product.State;
            Publisher = product.Publisher;
            Language = product.Language;
            Category = product.Category;
            Book = product.Book;
        }
    }
}
