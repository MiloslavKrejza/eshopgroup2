using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{


    public class ProductRatingRepository : IProductRatingRepository
    {
        string _connectionString;
        public ProductRatingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        BusinessDbContext _context;
        public ProductRatingRepository(BusinessDbContext context)
        {
            _context = context;
        }
        public ProductRating GetRating(int id)
        {
            ProductRating res;
            string queryString = "SELECT * FROM dbo.ProductRatings WHERE Id = @id;";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    res = new ProductRating() { ProductId = (int)reader[0], AverageRating = (decimal)reader[1] };
                }
            }
            return res;
        }

        public IQueryable<ProductRating> GetRatings()
        {
            List<ProductRating> res = new List<ProductRating>();
            string queryString = "SELECT * FROM dbo.ProductRatings;";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, conn);


                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var result = new ProductRating() { ProductId = (int)reader[0] };
                        result.AverageRating = reader[1] == DBNull.Value ? null : (decimal?)reader[1];
                        res.Add(result);
                    }
                }
            }
            return res.AsQueryable();
        }
    }
}
