using Bookzilla.API.Mapper;

namespace Bookzilla.API.Services.Interface
{
    public interface ISerieService
    {
        void Update(SerieDTO entity);
        void Add(SerieDTO entity);
        void Add(SerieDTO entity, string filename, Stream ImageArtStream);
        void AddRange(IEnumerable<SerieDTO> entities);
        IEnumerable<SerieDTO> Get();
        SerieDTO GetById(int id);
        Task GetCoverForSeries(int entityID);
        Task GetCoverForSeries(SerieDTO entity);
        void Remove(SerieDTO entity);
        void RemoveRange(IEnumerable<SerieDTO> entities);
    }
}