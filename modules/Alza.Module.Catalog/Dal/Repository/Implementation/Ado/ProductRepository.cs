using Alza.Core.Module.Abstraction;
using Alza.Core.Module.Dal;
using Alza.Core.Module.Http;
using Alza.Module.Catalog.Configuration;
using Alza.Module.Catalog.Dal.Entities;
using Alza.Module.Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Repository.Implementation.Ado
{
    public class ProductRepository : IProductRepository
    {
        private readonly AlzaCatalogOptions _options;
        private ILogger<ProductRepository> _logger;

        public ProductRepository(IOptions<AlzaCatalogOptions> options, ILogger<ProductRepository> logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
        }



        /*********************************************/
        /*                                           */
        /*********************************************/

        public Product Add(Product entity)
        {
            Product en = new Product();

            return en;
        }

        public Product Update(Product entity)
        {
            Product en = new Product();

            return en;
        }

        public void Remove(int id)
        {
           
        }

        public Product Get(int id)
        {
            Product en = new Product();

            return en;
        }
        public Product GetByName(string name)
        {
            Product en = new Product();

            return en;
        }
        public Product GetBySEOName(string seoName)
        {
            Product en = new Product();

            return en;
        }

        public Product GetByCode(string code)
        {
            Product en = new Product();

            return en;
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/
        public IQueryable<Product> Query()
        {
            List<Product> result = new List<Product>();


            return result.AsQueryable();
        }


        /*********************************************/
        /*           FILTER QUERY                    */
        /*********************************************/
        public IQueryable<Product> Query(Dictionary<string, string> filter)
        {
            List<Product> result = new List<Product>();

            return result.AsQueryable();
        }







        /*********************************************/
        /*               HELPERS                     */
        /*********************************************/

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="something"></param>
        /// <returns></returns>
        public Object ToParameter(object something)
        {
            if (something == null)
            {
                return DBNull.Value;
            }

            return something;
        }
    }
}
