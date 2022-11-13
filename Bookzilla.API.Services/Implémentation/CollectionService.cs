
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
    public class CollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFTPService _ftpservice;
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
        public void Add(CollectionDTO entity,String filename, Stream ImageArtStream)
        {
            var ext = Path.GetExtension(filename);
            _ftpservice.UploadCollectionArt(ImageArtStream, $"{entity.Name}{ext}");
            this.Add(entity);
        }
        public void Add(CollectionDTO entity)
        {
            _unitOfWork.Collections.Add(_mapper.Map<Collection>(entity));
            _unitOfWork.Complete();
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
            _unitOfWork.Collections.RemoveRange(_mapper.Map<IEnumerable<Collections>>(entities));
            _unitOfWork.Complete();
        }
    }
}
