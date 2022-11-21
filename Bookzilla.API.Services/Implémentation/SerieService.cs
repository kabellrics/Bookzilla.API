
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
        private const string MimepngType = "image/png";
        private const string MimejpgType = "image/jpg";
        private const string MimeJPEGType = "image/jpeg";
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
        public void SetDefaultCoverForSeries()
        {
            foreach(var entity in _unitOfWork.Series.Find(x=>string.IsNullOrEmpty(x.CoverArtPath)))
            {
                entity.CoverArtPath = GetCoverForSeries(entity.Id);
                _unitOfWork.Complete();
            }
        }
        public void GetCoverForSeries(SerieDTO entity)
        {
            var firstfile = _unitOfWork.Albums.Find(x => x.SerieId == entity.Id).OrderBy(x => x.Order).FirstOrDefault();
            if (firstfile != null)
            {
                entity.CoverArtPath = firstfile.CoverArtPath;
            }
        }
        public String GetCoverForSeries(int entityID)
        {
            var firstfile = _unitOfWork.Albums.Find(x => x.SerieId == entityID).OrderBy(x => x.Order).FirstOrDefault();
            if (firstfile != null)
            {
                return firstfile.CoverArtPath;
            }
            return String.Empty;
        }
        public async Task<SerieDTO> AddFile(int id, String filename, Stream ImageArtStream)
        {
            var ext = Path.GetExtension(filename);
            var entity = this.GetById(id);
            entity.CoverArtPath = await _ftpservice.UploadSerieArt(ImageArtStream, $"{entity.Id}{ext}");
            return this.Add(entity);
        }
        public async Task<(byte[], String,String)> GetCoverData(int id)
        {
            var item = _unitOfWork.Series.GetById(id);
            if (!string.IsNullOrEmpty(item.CoverArtPath))
            {
                var filename = Path.GetFileName(item.CoverArtPath);
                var imgstream = await _ftpservice.GetStreamAsync(item.CoverArtPath);
                    if (Path.GetExtension(filename) == ".jpg")
                    {
                        return (imgstream, MimepngType, filename);
                    }
                    else if (Path.GetExtension(filename) == ".jpg")
                    {
                        return (imgstream, MimejpgType, filename);
                    }
                    else if (Path.GetExtension(filename) == ".jpg")
                    {
                        return (imgstream, MimeJPEGType, filename);
                    }
                    else
                        return (null, String.Empty, String.Empty);
                
            }
            return (null, String.Empty, String.Empty);
        }
        public void Update(SerieDTO entity)
        {
            var item = _unitOfWork.Series.GetById(entity.Id);
            item.Name = entity.Name;
            item.CoverArtPath = entity.CoverArtPath;
            item.CollectionId = entity.CollectionId;
            _unitOfWork.Complete();
        }
        public SerieDTO Add(SerieDTO entity)
        {
            var serie = _unitOfWork.Series.Add(_mapper.Map<Serie>(entity));
            _unitOfWork.Complete();
            serie.CoverArtPath = GetCoverForSeries(serie.Id);
            _unitOfWork.Complete();
            return _mapper.Map<SerieDTO>(serie);
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
