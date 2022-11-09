using Bookzilla.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Repository
{
    internal interface IRepository
    {
    }
    public interface IAlbumRepository : IGenericRepository<Album>
    {
    }
    public interface ISerieRepository : IGenericRepository<Serie>
    {
    }
    public interface ICollectionRepository : IGenericRepository<Collection>
    {
    }
}
