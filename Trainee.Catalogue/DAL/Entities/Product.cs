using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Catalogue.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int PublisherId { get; set; }
        public int FormatId { get; set; }
        public int CategoryId { get; set; }
        public int StateId { get; set; }
        public int BookId { get; set; }

        public string Name { get; set; }
        public string PicAddress { get; set; }
        public string Text { get; set; }
        public decimal Price { get; set; }
        public int? PageCount { get; set; }
        public int? Year { get; set; }
        public string ISBN { get; set; }
        public string EAN { get; set; }

        //Referenced properties
        public Format Format { get; set; }
        public ProductState State { get; set; }
        public Language Language { get; set; }
        public Publisher Publisher { get; set; }
        public Category Category { get; set; }
        public Book Book { get; set; }
    }
}
