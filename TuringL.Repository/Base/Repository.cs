using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.Models;

namespace TuringL.Repository
{
    public abstract class Repository<T,Tld>:IUnitOfWorkRepository where T:IAggregateRoot
    {
        private IUnitOfWork _unitOfWork = null;

        public Repository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Save(T entity)
        {
            _unitOfWork.RegisterSave(entity, this);
        }

        public void Add(T entity)
        {
            _unitOfWork.RegisterAdd(entity, this);
        }

        public void Del(T entity)
        {
            _unitOfWork.RegisterDel(entity, this);
        }

        public abstract T GetByKey(Tld Id);

        public abstract void PersistAdd(IAggregateRoot entity);

        public abstract void PersistDel(IAggregateRoot entity);

        public abstract void PersistSave(IAggregateRoot entity);

        public abstract IQueryable<T> GetAll();
    }
}
