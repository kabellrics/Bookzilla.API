using Bookzilla.API.DataAccessLayer;

namespace Bookzilla.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookzillaDbContext _context;
        public UnitOfWork(BookzillaDbContext context)
        {
            _context = context;
            Series = new SerieRepository(_context);
            Collections = new CollectionRepository(_context);
            Albums = new AlbumRepository(_context);
        }
        public ISerieRepository Series { get; private set; }
        public ICollectionRepository Collections { get; private set; }
        public IAlbumRepository Albums { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
