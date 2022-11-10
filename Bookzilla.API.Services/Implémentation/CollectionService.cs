using Bookzilla.API.Models;
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
        public CollectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Collection> Get()
        {
            return _unitOfWork.Collections.GetAll();
        }
        public Collection GetById(int id)
        {
            return _unitOfWork.Collections.GetById(id);
        }
        public IEnumerable<Collection> Find(Expression<Func<Collection, bool>> expression)
        {
            return _unitOfWork.Collections.Find(expression);
        }
        public void Add(Collection entity)
        {
            _unitOfWork.Collections.Add(entity);
            _unitOfWork.Complete();
        }
        public void AddRange(IEnumerable<Collection> entities)
        {
            _unitOfWork.Collections.AddRange(entities);
            _unitOfWork.Complete();
        }
        public void Remove(Collection entity)
        {
            _unitOfWork.Collections.Remove(entity);
            _unitOfWork.Complete();
        }
        public void RemoveRange(IEnumerable<Collection> entities)
        {
            _unitOfWork.Collections.RemoveRange(entities);
            _unitOfWork.Complete();
        }
    }
}
