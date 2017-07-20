using Alza.Core.Module.Abstraction;
using Alza.Core.Module.Http;
using Alza.Module.Catalog.Dal.Entities;
using Alza.Module.Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Business
{
    public class CatalogService
    {
        private ILogger<CatalogService> _logger;
        private IProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private IMediaRepository _mediaRepo;


        public CatalogService(IProductRepository productRepo,
                              ICategoryRepository categoryRepo,
                              IMediaRepository mediaRepo,
                              ILogger<CatalogService> logger)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _mediaRepo = mediaRepo;
            _logger = logger;
        }



        /**********************************************/
        /*       GET  COLLECTIONS                     */
        /**********************************************/

        public AlzaAdminDTO GetProducts()
        {
            try
            {
                var result = _productRepo.Query().ToList();

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetProducts(int pageNumber, int itemsPerPage)
        {
            try
            {
                //var temp = _productRepo.Query().ToList();

                ////strankovani
                //var result = temp.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();

                //return AlzaAdminDTO.Data(result);




                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber - 1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@CategoryName", null);

                var result = _productRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetProducts(string categoryName)
        {
            try
            {
                ////zjisteni vsech produktu v dane kategorii
                //var allproducts = _productRepo.Query().ToList();
                //List<Product> productWithCategory = new List<Product>();
                //foreach (var pr in allproducts)
                //{
                //    foreach (var prCat in pr.ProductCategories)
                //    {
                //        if (prCat.Category.Name == categoryName)
                //        {
                //            productWithCategory.Add(pr);
                //        }
                //    }
                //}

                ////strankovani
                //var result = productWithCategory.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();

                //return AlzaAdminDTO.Data(result);



                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", "0");
                filter.Add("@PageSize", "1000000");
                filter.Add("@CategoryName", categoryName);

                var result = _productRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetProducts(string categoryName, int pageNumber, int itemsPerPage)
        {
            try
            {
                ////zjisteni vsech produktu v dane kategorii
                //var allproducts = _productRepo.Query().ToList();
                //List<Product> productWithCategory = new List<Product>();
                //foreach (var pr in allproducts)
                //{
                //    foreach (var prCat in pr.ProductCategories)
                //    {
                //        if (prCat.Category.Name == categoryName)
                //        {
                //            productWithCategory.Add(pr);
                //        }
                //    }
                //}

                ////strankovani
                //var result = productWithCategory.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();

                //return AlzaAdminDTO.Data(result);



                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber - 1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@CategoryName", categoryName);

                var result = _productRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }




        public AlzaAdminDTO GetCategories()
        {
            try
            {
                var result = _categoryRepo.Query().ToList().Where(c => c.ParentId != null).ToList();

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetCategoriesWithImages()
        {
            try
            {
                List<string> categories = new List<string>();

                var result = _categoryRepo.GetStats1("Image");

                foreach (var item in result)
                {
                    if (item.Value > 0)
                        categories.Add(item.Key);
                }

                return AlzaAdminDTO.Data(categories);
            }
            catch (Exception e)
            {

                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetImagesCount(string categoryName)
        {
            try
            {
                var result = _categoryRepo.GetStats1ByCategoryName(categoryName, "Image");

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {

                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetCategoriesWithVideos()
        {
            try
            {
                List<string> categories = new List<string>();

                var result = _categoryRepo.GetStats1("Video");

                foreach (var item in result)
                {
                    if (item.Value > 0)
                        categories.Add(item.Key);
                }

                return AlzaAdminDTO.Data(categories);
            }
            catch (Exception e)
            {

                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetVideosCount(string categoryName)
        {
            try
            {
                var result = _categoryRepo.GetStats1ByCategoryName(categoryName, "Video");

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {

                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetCategoriesWithGames()
        {
            try
            {
                List<string> categories = new List<string>();

                var result = _categoryRepo.GetStats1("Game");

                foreach (var item in result)
                {
                    if (item.Value > 0)
                        categories.Add(item.Key);
                }

                return AlzaAdminDTO.Data(categories);
            }
            catch (Exception e)
            {

                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetGamesCount(string categoryName)
        {
            try
            {
                var result = _categoryRepo.GetStats1ByCategoryName(categoryName, "Game");

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {

                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }






        /// <summary>
        /// Full - IMAGES collection
        /// </summary>
        /// <returns></returns>
        public AlzaAdminDTO GetImages()
        {
            try
            {
                var result = _mediaRepo.Query("Image").ToList();

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// PAGING - IMAGES collection
        /// </summary>
        /// <returns></returns>
        public AlzaAdminDTO GetImages(int pageNumber, int itemsPerPage)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber - 1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@MediaType", "Image");
                filter.Add("@CategoryName", null);

                var result = _mediaRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// BY CATEGORY - IMAGES collection
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public AlzaAdminDTO GetImages(string categoryName)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", "0");
                filter.Add("@PageSize", "1000000");
                filter.Add("@MediaType", "Image");
                filter.Add("@CategoryName", categoryName);

                var result = _mediaRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// BY CATEGORY WITH PAGING - IMAGES collection
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public AlzaAdminDTO GetImages(string categoryName, int pageNumber, int itemsPerPage)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber - 1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@MediaType", "Image");
                filter.Add("@CategoryName", categoryName);

                var result = _mediaRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }


        /// <summary>
        /// Full - VIDEOS collection
        /// </summary>
        /// <returns></returns>
        public AlzaAdminDTO GetVideos()
        {
            try
            {
                var result = _mediaRepo.Query("Video").ToList();

                return AlzaAdminDTO.Data(result);

            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }

        }
        /// <summary>
        /// PAGING - VIDEOS collection
        /// </summary>
        /// <returns></returns>
        public AlzaAdminDTO GetVideos(int pageNumber, int itemsPerPage)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber-1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@MediaType", "Video");
                filter.Add("@CategoryName", null);

                var result = _mediaRepo.Query(filter).ToList();


                //var result = temp.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// BY CATEGORY - VIDEOS collection
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public AlzaAdminDTO GetVideos(string categoryName)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", "0");
                filter.Add("@PageSize", "1000000");
                filter.Add("@MediaType", "Video");
                filter.Add("@CategoryName", categoryName);

                var result = _mediaRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// BY CATEGORY WITH PAGING - VIDEOS collection
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public AlzaAdminDTO GetVideos(string categoryName, int pageNumber, int itemsPerPage)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber - 1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@MediaType", "Video");
                filter.Add("@CategoryName", categoryName);

                var result = _mediaRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }




        /// <summary>
        /// Full - GAMES collection
        /// </summary>
        /// <returns></returns>
        public AlzaAdminDTO GetGames()
        {
            try
            {
                var result = _mediaRepo.Query("Game").ToList();

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// PAGING - GAMES collection
        /// </summary>
        /// <returns></returns>
        public AlzaAdminDTO GetGames(int pageNumber, int itemsPerPage)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber - 1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@MediaType", "Game");
                filter.Add("@CategoryName", null);

                var result = _mediaRepo.Query(filter).ToList();
                
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// BY CATEGORY - GAMES collection
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public AlzaAdminDTO GetGames(string categoryName)
        {
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", "0");
                filter.Add("@PageSize", "1000000");
                filter.Add("@MediaType", "Game");
                filter.Add("@CategoryName", categoryName);

                var result = _mediaRepo.Query(filter).ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        /// <summary>
        /// BY CATEGORY WITH PAGING - GAMES collection
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public AlzaAdminDTO GetGames(string categoryName, int pageNumber, int itemsPerPage)
        {
            try
            {
                ////zjisteni vsech produktu v dane kategorii
                //var allproducts = _productRepo.Query().ToList();
                //List<Product> productWithCategory = new List<Product>();
                //foreach (var pr in allproducts)
                //{
                //    foreach (var prCat in pr.ProductCategories)
                //    {
                //        if (prCat.Category.Name == categoryName)
                //        {
                //            productWithCategory.Add(pr);
                //        }
                //    }
                //}

                ////zjisteni vsech her pro dane produkty
                //List<Media> gamesWithCategory = new List<Media>();
                //List<string> tempArray = new List<string>();
                //foreach (var item in productWithCategory)
                //{
                //    var fullproduct = _productRepo.Get(item.Id);

                //    foreach (var med in fullproduct.ProductMedia)
                //    {
                //        if (med.Media.MediaType.Value == "Game")
                //        {
                //            if (!tempArray.Contains(med.Media.Name))
                //            {
                //                gamesWithCategory.Add(med.Media);
                //                tempArray.Add(med.Media.Name);
                //            }
                //        }
                //    }
                //}

                ////strankovani
                //var result = gamesWithCategory.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();






                var filter = new Dictionary<string, string>();
                filter.Add("@PageOffset", ((pageNumber - 1) * itemsPerPage).ToString());
                filter.Add("@PageSize", itemsPerPage.ToString());
                filter.Add("@MediaType", "Game");
                filter.Add("@CategoryName", categoryName);

                var result = _mediaRepo.Query(filter).ToList();










                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }





        /**********************************************/
        /*              GET ITEM                      */
        /**********************************************/


        public AlzaAdminDTO GetProduct(int id)
        {
            try
            {
                var result = _productRepo.Get(id);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetProductByName(string name)
        {
            try
            {
                var result = _productRepo.GetByName(name);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetProductBySEOName(string seoName)
        {
            try
            {
                var result = _productRepo.GetBySEOName(seoName);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetProductByCode(string code)
        {
            try
            {
                var result = _productRepo.GetByCode(code);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetCategory(int id)
        {
            try
            {
                var result = _categoryRepo.Get(id);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetCategoryByName(string name)
        {
            try
            {
                var result = _categoryRepo.GetByName(name);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetCategoryBySEOName(string seoName)
        {
            try
            {
                var result = _categoryRepo.GetBySEOName(seoName);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetMedia(int id)
        {
            try
            {
                var result = _mediaRepo.Get(id);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO GetMedia(string url, string name)
        {
            try
            {
                Media result = null;

                if (url != null)
                {
                    result = _mediaRepo.GetByUrl(url);
                }
                else if (name != null)
                {
                    result = _mediaRepo.GetByName(name);
                }
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }



        /**********************************************/
        /*              ADD ITEM                      */
        /**********************************************/

        public AlzaAdminDTO AddProduct(Product item)
        {
            try
            {
                _productRepo.Add(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO AddCategory(Category item)
        {
            try
            {
                string seoNameTemp = "";
                if(String.IsNullOrEmpty(item.SEOName))
                {
                    seoNameTemp = item.Name.Replace(",", "").Replace("'", "").Replace(" ", "-");

                    item.SEOName = seoNameTemp;
                }

                _categoryRepo.Add(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO AddMedia(Media item)
        {
            try
            {
                _mediaRepo.Add(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }




        /**********************************************/
        /*              REMOVE ITEM                      */
        /**********************************************/

        public AlzaAdminDTO RemoveProduct(int id)
        {
            try
            {
                _productRepo.Remove(id);
                return AlzaAdminDTO.True;
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO RemoveCategory(int id)
        {
            try
            {
                _categoryRepo.Remove(id);
                return AlzaAdminDTO.True;
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO RemoveMedia(int id)
        {
            try
            {
                _mediaRepo.Remove(id);
                return AlzaAdminDTO.True;
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }




        /**********************************************/
        /*              UPDATE ITEM                      */
        /**********************************************/
        public AlzaAdminDTO UpdateProduct(Product item)
        {
            try
            {
                _productRepo.Update(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        public AlzaAdminDTO UpdateCategory(Category item)
        {
            try
            {
                _categoryRepo.Update(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO UpdateMedia(Media item)
        {
            try
            {
                _mediaRepo.Update(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }











        /**********************************************/
        /*             HELPERS                     */
        /**********************************************/
        

        /// </// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO Error(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogCritical(errNo + " - " + text);
            return AlzaAdminDTO.Error(errNo, "SomeText");
        }
        
    }

    
}
