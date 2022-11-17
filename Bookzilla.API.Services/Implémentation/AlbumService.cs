
using AutoMapper;
using Bookzilla.API.Mapper;
using Bookzilla.API.Models;
using Bookzilla.API.Repository;
using Bookzilla.API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Implémentation
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFTPService _ftpservice;
        private readonly ICoverExtractorService _coverextractorservice;
        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper, IFTPService ftpservice, ICoverExtractorService coverextractorservice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ftpservice = ftpservice;
            _coverextractorservice = coverextractorservice;
        }
        public IEnumerable<AlbumDTO> Get()
        {
            return _mapper.Map<IEnumerable<AlbumDTO>>(_unitOfWork.Albums.GetAll());
        }
        public AlbumDTO GetById(int id)
        {
            return _mapper.Map<AlbumDTO>(_unitOfWork.Albums.GetById(id));
        }
        //public IEnumerable<AlbumDTO> Find(Expression<Func<AlbumDTO, bool>> expression)
        //{
        //    return _unitOfWork.Albums.Find(expression);
        //}
        public async Task<AlbumDTO> AddWithFile(AlbumDTO entity, String filename, Stream ImageArtStream)
        {
            var dbentity = _unitOfWork.Albums.Add(_mapper.Map<Album>(entity));
            _unitOfWork.Complete();
            dbentity.Path = await _ftpservice.UploadFileArt(ImageArtStream, $"{dbentity.Id}{Path.GetExtension(filename)}");
            await _ftpservice.DownloadOnLocalFile(Path.Combine("temp", $"{Path.GetFileName(dbentity.Path)}"), dbentity.Path);
            var coverpath = await _coverextractorservice.ExtractCoverForFile(Path.Combine("temp", $"{Path.GetFileName(dbentity.Path)}"));
            var ext = Path.GetExtension(coverpath);
            dbentity.CoverArtPath = await _ftpservice.UploadAlbumCover(coverpath, $"{dbentity.Id}{ext}");
            Directory.Delete("temp", true);
            _unitOfWork.Complete();
            return _mapper.Map<AlbumDTO>(dbentity);
        }
        public void Update(AlbumDTO entity)
        {
            var item = _unitOfWork.Albums.GetById(entity.Id);
            item.Name = entity.Name;
            item.Order = entity.Order;
            item.CurrentPage = entity.CurrentPage;
            item.SerieId = entity.SerieId;
            item.Path = entity.Path;
            item.CoverArtPath = entity.CoverArtPath;  
            item.ReadingStatus = entity.ReadingStatus;
            _unitOfWork.Complete();
        }
        public AlbumDTO Add(AlbumDTO entity)
        {
            var alb = _unitOfWork.Albums.Add(_mapper.Map<Album>(entity));
            _unitOfWork.Complete();
            return _mapper.Map<AlbumDTO>(alb);
        }
        public void AddRange(IEnumerable<AlbumDTO> entities)
        {
            _unitOfWork.Albums.AddRange(_mapper.Map<IEnumerable<Album>>(entities));
            _unitOfWork.Complete();
        }
        public void Remove(AlbumDTO entity)
        {
            _unitOfWork.Albums.Remove(_mapper.Map<Album>(entity));
            _unitOfWork.Complete();
        }
        public void RemoveRange(IEnumerable<AlbumDTO> entities)
        {
            _unitOfWork.Albums.RemoveRange(_mapper.Map<IEnumerable<Album>>(entities));
            _unitOfWork.Complete();
        }
    }
}
