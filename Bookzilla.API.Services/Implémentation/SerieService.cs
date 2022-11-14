
using AutoMapper;
using Bookzilla.API.Mapper;
using Bookzilla.API.Models;
using Bookzilla.API.Repository;
using Bookzilla.API.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Implémentation
{
    public class SerieService : ISerieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFTPService _ftpservice;
        private readonly ICoverExtractorService _coverextractorservice;
        public SerieService(IUnitOfWork unitOfWork, IMapper mapper, IFTPService ftpservice, ICoverExtractorService coverextractorservice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ftpservice = ftpservice;
            _coverextractorservice = coverextractorservice;
        }
        public IEnumerable<SerieDTO> Get()
        {
            return _mapper.Map<IEnumerable<SerieDTO>>(_unitOfWork.Series.GetAll());
        }
        public SerieDTO GetById(int id)
        {
            return _mapper.Map<SerieDTO>(_unitOfWork.Series.GetById(id));
        }
        //public IEnumerable<SerieDTO> Find(Expression<Func<SerieDTO, bool>> expression)
        //{
        //    return _unitOfWork.Series.Find(expression);
        //}

        public async Task GetCoverForSeries(SerieDTO entity)
        {
            var firstfile = _unitOfWork.Albums.Find(x => x.SerieId == entity.Id).OrderBy(x => x.Order).FirstOrDefault();
            if (firstfile != null)
            {
                await _ftpservice.DownloadOnLocalFile(Path.Combine("temp", $"{Path.GetFileName(firstfile.Path)}"), firstfile.Path);
                var coverpath = await _coverextractorservice.ExtractCoverForFile(Path.Combine("temp", $"{Path.GetFileName(firstfile.Path)}"));
                var ext = Path.GetExtension(coverpath);
                entity.CoverArtPath = await _ftpservice.UploadSerieArt(coverpath, $"{entity.Name}{ext}");
                Directory.Delete("temp", true);
            }
        }
        public async Task GetCoverForSeries(int entityID)
        {
            var entity = _unitOfWork.Series.GetById(entityID);
            await GetCoverForSeries(_mapper.Map<SerieDTO>(entity));
        }
        public async void Add(SerieDTO entity, String filename, Stream ImageArtStream)
        {
            var ext = Path.GetExtension(filename);
            entity.CoverArtPath = await _ftpservice.UploadSerieArt(ImageArtStream, $"{entity.Name}{ext}");
            this.Add(entity);
        }
        public void Update(SerieDTO entity)
        {
            var item = _unitOfWork.Series.GetById(entity.Id);
            item.Name = entity.Name;
            item.CoverArtPath = entity.CoverArtPath;
            item.CollectionId = entity.CollectionId;
            _unitOfWork.Complete();
        }
        public void Add(SerieDTO entity)
        {
            _unitOfWork.Series.Add(_mapper.Map<Serie>(entity));
            _unitOfWork.Complete();
        }
        public void AddRange(IEnumerable<SerieDTO> entities)
        {
            _unitOfWork.Series.AddRange(_mapper.Map<IEnumerable<Serie>>(entities));
            _unitOfWork.Complete();
        }
        public void Remove(SerieDTO entity)
        {
            _unitOfWork.Series.Remove(_mapper.Map<Serie>(entity));
            _unitOfWork.Complete();
        }
        public void RemoveRange(IEnumerable<SerieDTO> entities)
        {
            _unitOfWork.Series.RemoveRange(_mapper.Map<IEnumerable<Serie>>(entities));
            _unitOfWork.Complete();
        }
    }
}
