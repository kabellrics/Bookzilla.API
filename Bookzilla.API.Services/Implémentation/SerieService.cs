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
    public class SerieService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SerieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Serie> Get()
        {
            return _unitOfWork.Series.GetAll();
        }
        public Serie GetById(int id)
        {
            return _unitOfWork.Series.GetById(id);
        }
        public IEnumerable<Serie> Find(Expression<Func<Serie, bool>> expression)
        {
            return _unitOfWork.Series.Find(expression);
        }
        public void Add(Serie entity)
        {
            _unitOfWork.Series.Add(entity);
            _unitOfWork.Complete();
        }
        public void AddRange(IEnumerable<Serie> entities)
        {
            _unitOfWork.Series.AddRange(entities);
            _unitOfWork.Complete();
        }
        public void Remove(Serie entity)
        {
            _unitOfWork.Series.Remove(entity);
            _unitOfWork.Complete();
        }
        public void RemoveRange(IEnumerable<Serie> entities)
        {
            _unitOfWork.Series.RemoveRange(entities);
            _unitOfWork.Complete();
        }
    }
}
