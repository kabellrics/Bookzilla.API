using Bookzilla.API.Mapper;

namespace Bookzilla.API.Services.Interface
{
    public interface ISerieService
    {
        void Update(SerieDTO entity);
        SerieDTO Add(SerieDTO entity);
        Task<SerieDTO> AddFile(int id, string filename, Stream ImageArtStream);
        void AddRange(IEnumerable<SerieDTO> entities);
        IEnumerable<SerieDTO> Get();
        SerieDTO GetById(int id);
        String GetCoverForSeries(int entityID);
        void GetCoverForSeries(SerieDTO entity);
        void SetDefaultCoverForSeries();
        void Remove(SerieDTO entity);
        void RemoveRange(IEnumerable<SerieDTO> entities);
    }
}