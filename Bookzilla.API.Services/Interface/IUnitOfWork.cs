using Bookzilla.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ISerieRepository Series { get; }
        ICollectionRepository Collections { get; }
        IAlbumRepository Albums { get; }
        int Complete();
    }
}
