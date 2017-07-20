using Alza.Core.Module.Abstraction;
using Alza.Module.Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Repository.Abstraction
{
    public interface IMediaRepository : IRepository<Media>
    {
        IQueryable<Media> Query(string mediaType);
        IQueryable<Media> Query(Dictionary<string, string> filter);

        Media GetByUrl(string url);
        Media GetByName(string name);
    }
}
