using Bookzilla.API.Mapper;

namespace Bookzilla.API.Services.Interface
{
    public interface ICollectionService
    {
        void Update(CollectionDTO entity);
        void Add(CollectionDTO entity);
        void Add(CollectionDTO entity, string filename, Stream ImageArtStream);
        void AddRange(IEnumerable<CollectionDTO> entities);
        IEnumerable<CollectionDTO> Get();
        CollectionDTO GetById(int id);
        void Remove(CollectionDTO entity);
        void RemoveRange(IEnumerable<CollectionDTO> entities);
    }
}