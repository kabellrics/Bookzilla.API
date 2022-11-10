using Bookzilla.API.Models;
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
    public class AlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AlbumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Album> Get()
        {
            return _unitOfWork.Albums.GetAll();
        }
        public Album GetById(int id)
        {
            return _unitOfWork.Albums.GetById(id);
        }
        public IEnumerable<Album> Find(Expression<Func<Album, bool>> expression)
        {
            return _unitOfWork.Albums.Find(expression);
        }
        public void Add(Album entity)
        {
            _unitOfWork.Albums.Add(entity);
            _unitOfWork.Complete();
        }
        public void AddRange(IEnumerable<Album> entities)
        {
            _unitOfWork.Albums.AddRange(entities);
            _unitOfWork.Complete();
        }
        public void Remove(Album entity)
        {
            _unitOfWork.Albums.Remove(entity);
            _unitOfWork.Complete();
        }
        public void RemoveRange(IEnumerable<Album> entities)
        {
            _unitOfWork.Albums.RemoveRange(entities);
            _unitOfWork.Complete();
        }
    }
}
