using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using TuringL.Models;

namespace TuringL.Repository
{
    public class RepositoryFactory :IRepositoryFactory
    {
        private List<IUnitOfWorkRepository> _list = new List<IUnitOfWorkRepository>();

        private static readonly Dictionary<Type, Type> _dict = new Dictionary<Type, Type>() { 
                                                                {typeof(IRoleRepository),typeof(RoleUnitOfWorkRepository)},
                                                                {typeof(IAuthorityRepository),typeof(AuthorityUnitOfWorkRepository)},
                                                                {typeof(IAuthorizeRepository),typeof(AuthorizeUnitOfWorkRepository)},
                                                                {typeof(IUserRepository),typeof(UserUnitOfWorkRepository)}
                                                                };

        public IUnitOfWork UnitOfWork { get; set; }

        public RepositoryFactory()
        {
        }

        public void Begin()
        {
            ContextFactory.StoreContext();
            UnitOfWork = new UnitOfWork();
        }

        public IUnitOfWorkRepository Get(Type type,IUnitOfWork unitOfWork)
        {
            IUnitOfWorkRepository uow = _list.Where(it => it.GetType() == type).FirstOrDefault();
            if (uow == null)
            {
                try
                {
                    uow = (IUnitOfWorkRepository)Activator.CreateInstance(_dict[type], new object[] { unitOfWork });
                }
                catch (Exception ex)
                {
                    throw new Exception("error in create Iunitofwork in RepositoryFactory!");
                }
            }
            return uow;
        }

        public void End()
        {
            _list.Clear();
            UnitOfWork = null;
            ContextFactory.Dispose();
        }
    }
}
