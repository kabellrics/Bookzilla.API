
using AutoMapper;
using Bookzilla.API.Mapper;
using Bookzilla.API.Models;
using Bookzilla.API.Repository;
using Bookzilla.API.Services.Interface;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Implémentation
{
    public class CollectionService : ICollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFTPService _ftpservice;
        private const string MimepngType = "image/png";
        private const string MimejpgType = "image/jpg";
        private const string MimeJPEGType = "image/jpeg";
        public CollectionService(IUnitOfWork unitOfWork, IMapper mapper, IFTPService ftpservice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ftpservice = ftpservice;
        }

        public IEnumerable<CollectionDTO> Get()
        {
            return _mapper.Map<IEnumerable<CollectionDTO>>(_unitOfWork.Collections.GetAll());
        }
        public CollectionDTO GetById(int id)
        {
            return _mapper.Map<CollectionDTO>(_unitOfWork.Collections.GetById(id));
        }
        //public IEnumerable<CollectionDTO> Find(Expression<Func<CollectionDTO, bool>> expression)
        //{
        //    return _unitOfWork.Collections.Find(expression);
        //}
        public async Task<(Stream, String, String)> GetCoverData(int id)
        {
            var item = _unitOfWork.Collections.GetById(id);
            if (!string.IsNullOrEmpty(item.ImageArtPath))
            {
                var filename = Path.GetFileName(item.ImageArtPath);
                using (var imgstream = await _ftpservice.GetStreamAsync(item.ImageArtPath))
                {
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
                        return (null,String.Empty,String.Empty);
                }
            }
            return (null, String.Empty, String.Empty);
        }
        public async Task<CollectionDTO> Add(CollectionDTO entity, String filename, Stream ImageArtStream)
        {
            var ext = Path.GetExtension(filename);
            var collec = this.Add(entity);
            collec.ImageArtPath = await _ftpservice.UploadCollectionArt(ImageArtStream, $"{collec.Id}{ext}");
            return collec;
        }
        public async Task<CollectionDTO> AddFile(int id, String filename, Stream ImageArtStream)
        {
            var ext = Path.GetExtension(filename);
            var collec = this.GetById(id);
            collec.ImageArtPath = await _ftpservice.UploadCollectionArt(ImageArtStream, $"{collec.Id}{ext}");
            this.Update(collec);
            return collec;
        }
        public void Update(CollectionDTO entity)
        {
            var item = _unitOfWork.Collections.GetById(entity.Id);
            item.Name = entity.Name;
            item.ImageArtPath = entity.ImageArtPath;            
            _unitOfWork.Complete();
        }
        public CollectionDTO Add(CollectionDTO entity)
        {
            var collec = _unitOfWork.Collections.Add(_mapper.Map<Collection>(entity));
            _unitOfWork.Complete();
            return _mapper.Map<CollectionDTO>(collec);
        }
        public void AddRange(IEnumerable<CollectionDTO> entities)
        {
            _unitOfWork.Collections.AddRange(_mapper.Map<IEnumerable<Collection>>(entities));
            _unitOfWork.Complete();
        }
        public void Remove(CollectionDTO entity)
        {
            _unitOfWork.Collections.Remove(_mapper.Map<Collection>(entity));
            _unitOfWork.Complete();
        }
        public void RemoveRange(IEnumerable<CollectionDTO> entities)
        {
            _unitOfWork.Collections.RemoveRange(_mapper.Map<IEnumerable<Collection>>(entities));
            _unitOfWork.Complete();
        }
    }
}
