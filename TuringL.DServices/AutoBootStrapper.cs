using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.Models;
using TuringL.Repository;

namespace TuringL.DServices
{
    public class AutoBootStrapper
    {
        public static void Start()
        {
            AutoMapperBootStrapper.ConfigurationMapper();
        }
    }


    internal class RepositoryFactory
    {

        private static readonly Dictionary<Type, Type> _dict = new Dictionary<Type, Type>() { 
                                                                {typeof(IRoleRepository),typeof(RoleUnitOfWorkRepository)},
                                                                {typeof(IAuthorityRepository),typeof(AuthorityUnitOfWorkRepository)},
                                                                {typeof(IAuthorizeRepository),typeof(AuthorizeUnitOfWorkRepository)},
                                                                {typeof(IUserRepository),typeof(UserUnitOfWorkRepository)},
                                                                {typeof(IProductInfoRepository),typeof(ProductInfoUnitOfWorkRepository)},
                                                                {typeof(IMaintanceRecordRepository),typeof(MaintanceRecordUnitOfWorkRepository)},
                                                                {typeof(IInstallInfoRepository),typeof(InstallUnitOfWorkRepository)}
                                                                };

        public static IUnitOfWorkRepository Get(Type type, IUnitOfWork unitOfWork)
        {
            IUnitOfWorkRepository uow = null;// _list.Where(it => it.GetType() == type).FirstOrDefault();
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

        public static IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}
