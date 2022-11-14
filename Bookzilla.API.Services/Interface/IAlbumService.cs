using Bookzilla.API.Mapper;

namespace Bookzilla.API.Services.Interface
{
    public interface IAlbumService
    {
        void Update(AlbumDTO entity);
        void Add(AlbumDTO entity);
        void Add(AlbumDTO entity, string filename, Stream ImageArtStream);
        void AddRange(IEnumerable<AlbumDTO> entities);
        IEnumerable<AlbumDTO> Get();
        AlbumDTO GetById(int id);
        void Remove(AlbumDTO entity);
        void RemoveRange(IEnumerable<AlbumDTO> entities);
    }
}