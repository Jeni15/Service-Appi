using Base.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Service.Infrastructure
{
    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        protected IUnitOfWork _unitOfWork;
        protected IRepository<TEntity> _repository;

        public EntityService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;

        }


        public virtual void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _unitOfWork.BeginTransaction();
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
        }


        public virtual void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            _unitOfWork.BeginTransaction();
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            _unitOfWork.BeginTransaction();
            _repository.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        
    }

}
