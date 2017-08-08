
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
        private readonly BusinessDbContext _context;
        string _connectionString;

        public CategoryRelationshipRepository(BusinessDbContext context)
        {
            _context = context;
        }

        /*
        public CategoryRelationshipRepository(string connectionString)
        {
            _connectionString = connectionString;
        }*/
        public IQueryable<CategoryRelationshipBO> GetAllRelationships()
        {
            var blbina = _context.CategoryRelationships.AsQueryable();

            var ctrsId = _context.CategoryRelationships.Select(c => c.Id);
            var ctrsChId = _context.CategoryRelationships.Select(c => c.ChildId);

            int count = ctrsId.Count();

            List<CategoryRelationshipBO> list = new List<CategoryRelationshipBO>();
            var a = ctrsId.Concat(ctrsChId).ToList();

            for (int i = 0; i < a.Count; i++)
            {
                if (i < count)
                {
                    CategoryRelationshipBO c = new CategoryRelationshipBO { Id = a[i] };
                    list.Add(c);
                }
                else
                {
                    list[i - count].ChildId = a[i];
                }
            }



            //                var ctrs = _context.CategoryRelationships.FromSql(@"
            //                                                                SELECT c2.Id as Id, c1.Id as ChildId
            //                                                                FROM dbo.Categories as c1
            //                                                                INNER JOIN dbo.Categories as c2 ON  c2.Id = c1.ParentId
            //WHERE c1.ParentId is not null
            //                                                            ").ToList();



            //return _context.CategoryRelationships.AsQueryable();

            return list.AsQueryable();

            /*
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
            */
        }
    }
}
