using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.Models;
using System.Transactions;
using System.Threading;

namespace TuringL.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private Queue<UnitItem> _ItemQueue = new Queue<UnitItem>();
        private DataContext _dataContext = null;

        public UnitOfWork()
        {
            ContextFactory.StoreContext(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name);
            _dataContext = ContextFactory.GetDataContext(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name);
        }

        public void RegisterAdd(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            if (_ItemQueue == null)
                _ItemQueue = new Queue<UnitItem>();
            UnitItem unitItem=new UnitItem() 
            { Entity = entity, UnitOfWorkRepository = unitOfWorkRepository, Operator = UnitOperator.Add };
            if (_ItemQueue.Contains(unitItem))
            {
                throw new Exception("exist Entity in RegistorAdd!");
            }
            else
            {
                _ItemQueue.Enqueue(unitItem);
            }
        }

        public void RegisterDel(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            if (_ItemQueue == null)
                _ItemQueue = new Queue<UnitItem>();
            UnitItem unitItem = new UnitItem() { Entity = entity, UnitOfWorkRepository = unitOfWorkRepository, Operator = UnitOperator.Del };
            if (_ItemQueue.Contains(unitItem))
            {
                throw new Exception("exist Entity in RegistorDel!");
            }
            else
            {
                _ItemQueue.Enqueue(unitItem);
            }
        }

        public void RegisterSave(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            if (_ItemQueue == null)
                _ItemQueue = new Queue<UnitItem>();
            UnitItem unitItem = new UnitItem() { Entity = entity, UnitOfWorkRepository = unitOfWorkRepository, Operator = UnitOperator.Save };
            if (_ItemQueue.Contains(unitItem))
            {
                throw new Exception("exist Entity in RegistorSave!");
            }
            else
            {
                _ItemQueue.Enqueue(unitItem);
            }
        }

        public void Commit()
        {
            if (_ItemQueue != null && _ItemQueue.Count > 0)
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
                {
                    while (_ItemQueue.Count > 0)
                    {
                        UnitItem item = _ItemQueue.Dequeue();
                        switch (item.Operator)
                        {
                            case UnitOperator.Add:
                                item.UnitOfWorkRepository.PersistAdd(item.Entity);
                                break;
                            case UnitOperator.Del:
                                item.UnitOfWorkRepository.PersistDel(item.Entity);
                                break;
                            case UnitOperator.Save:
                                item.UnitOfWorkRepository.PersistSave(item.Entity);
                                break;
                            default: break;
                        }
                    }
                    ContextFactory.GetDataContext(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name).SaveChanges();
                    trans.Complete();
                }
            }
        }

        public void Dispose()
        {
            ContextFactory.Remove(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name);
            //GC.SuppressFinalize(false);
        }
    }

    internal class UnitItem
    {
        public IAggregateRoot Entity { get; set; }
        public IUnitOfWorkRepository UnitOfWorkRepository { get; set; }
        public UnitOperator Operator { get; set; }
    }

    internal enum UnitOperator
    {
        Add,Save,Del
    }
}
