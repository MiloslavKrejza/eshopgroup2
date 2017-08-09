
using System.Linq;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Trainee.Business.DAL.Repositories
{

    public class CategoryRelationshipRepository : ICategoryRelationshipRepository
    {
        
        private readonly string _connectionString;

       
        public CategoryRelationshipRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IQueryable<CategoryRelationshipBO> GetAllRelationships()
        {

            List<CategoryRelationshipBO> categories = new List<CategoryRelationshipBO>();
            using (var conn = new SqlConnection(_connectionString))
            {

                SqlCommand command = new SqlCommand("SELECT * FROM CategoryRelationships;");
                command.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryRelationshipBO { Id = (int)reader[0], ChildId = (int)reader[1] });
                    }
                }
            }
            return categories.AsQueryable();

        }
    }
}
