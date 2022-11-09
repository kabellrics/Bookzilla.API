using Bookzilla.API.DataAccessLayer;
using Bookzilla.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Repository
{
    internal class Repository
    {
    }
    public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(BookzillaDbContext context) : base(context)
        {
        }
    }
    public class SerieRepository : GenericRepository<Serie>, ISerieRepository
    {
        public SerieRepository(BookzillaDbContext context) : base(context)
        {
        }
    }
    public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(BookzillaDbContext context) : base(context)
        {
        }
    }
}
