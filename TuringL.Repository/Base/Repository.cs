using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using TuringL.Infrasturcture.Log;
using TuringL.Models;

namespace TuringL.Repository
{
    public abstract class Repository<T,Tld>:IUnitOfWorkRepository where T:class,IAggregateRoot
    {
        private IUnitOfWork _unitOfWork = null;
        protected DataContext _dataContext = ContextFactory.GetDataContext(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name);

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

        public virtual void PersistAdd(IAggregateRoot entity)
        {
            if (entity == null) throw new Exception("nullable data in PersistAdd of Respository<"+typeof(T).Name+">");
            _dataContext.Entry<IAggregateRoot>(entity).State = EntityState.Added;
            if (_dataContext.SaveChanges() < 1)
            {
                throw new Exception("error in PersistAdd of Respository<T>");
            }
        }

        public virtual void PersistDel(IAggregateRoot entity)
        {
            if (entity == null) throw new Exception("nullable data in PersistDel of Respository<"+typeof(T).Name+">");
            _dataContext.Entry<IAggregateRoot>(entity).State=EntityState.Deleted;
            if(_dataContext.SaveChanges()<1)
                throw new Exception("error in PersistDel of Respository<T>"); 
        }

        public virtual void PersistSave(IAggregateRoot entity)
        {
            if (entity == null) throw new Exception("nullable data in PersistSave of Respository<" + typeof(T).Name + ">");
            _dataContext.Entry<IAggregateRoot>(entity).State = EntityState.Modified;
            if (_dataContext.SaveChanges() < 1)
                throw new Exception("error in PersistSave of Respository<T>"); 
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dataContext.Set<T>() as IQueryable<T>;
        }
    }
}
