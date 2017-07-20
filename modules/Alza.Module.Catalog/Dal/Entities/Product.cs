using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Entities
{
    public class Product : Entity
    {
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string SEOName { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public string Description { get; set; }

        public string MainImage { get; set; }

        //NAVIGATION
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public List<ProductMedia> ProductMedia { get; set; } = new List<Entities.ProductMedia>();


        public List<Media> getVideos()
        {
            List<Media> result = new List<Media>();

            foreach (var item in ProductMedia)
            {
                if (item.Media.MediaType.Value == "Video")
                {
                    result.Add(item.Media);
                }
            }

            return result;
        }

        public List<Media> getImages()
        {
            List<Media> result = new List<Media>();

            foreach (var item in ProductMedia)
            {
                if (item.Media.MediaType.Value == "Image")
                {
                    result.Add(item.Media);
                }
            }

            return result;
        }

        public List<Media> getGames()
        {
            List<Media> result = new List<Media>();

            foreach (var item in ProductMedia)
            {
                if (item.Media.MediaType.Value == "Game")
                {
                    result.Add(item.Media);
                }
            }

            return result;
        }

        public List<Category> getCategories()
        {
            List<Category> result = new List<Category>();

            foreach (var item in ProductCategories)
            {
                result.Add(item.Category);
            }

            return result;
        }
        
    }
}
