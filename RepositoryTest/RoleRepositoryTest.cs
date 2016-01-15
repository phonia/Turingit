using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TuringL.Repository;
using TuringL.Models;

namespace RepositoryTest
{
    /// <summary>
    /// RoleRepositoryTest 的摘要说明
    /// </summary>
    [TestClass]
    public class RoleRepositoryTest
    {
        public RoleRepositoryTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        protected IUnitOfWork _unitOfWork = null;

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //[TestMethod]
        //public void Create()
        //{
        //    _repositoryFactory = new RepositoryFactory();
        //    _repositoryFactory.Begin();
        //    _unitOfWork = _repositoryFactory.UnitOfWork;
        //    IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
        //    Role role = new Role() {Id="",Name="超级管理员" };
        //    //roleRepository.Add(role);
        //    _unitOfWork.Commit();
        //    _repositoryFactory.End();
        //}

        //[TestMethod]
        //public void CreatWithNullUnitOfWork()
        //{
        //    try
        //    {
        //        _repositoryFactory = new RepositoryFactory();
        //        _repositoryFactory.Begin();
        //        _repositoryFactory.UnitOfWork = null;
        //        _unitOfWork = _repositoryFactory.UnitOfWork;
        //        IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
        //        Role role = new Role() { Id = "", Name = "超级管理员" };
        //        //roleRepository.Add(role);
        //        _unitOfWork.Commit();
        //        _repositoryFactory.End();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //[TestMethod]
        //public void CreateWithWrongRepositoryImpl()
        //{
        //    try
        //    {
        //        _repositoryFactory = new RepositoryFactory();
        //        _repositoryFactory.Begin();
        //        _unitOfWork = _repositoryFactory.UnitOfWork;
        //        IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IRoleRepository;
        //        Role role = new Role() { Id = "", Name = "超级管理员" };
        //        //roleRepository.Add(role);
        //        _unitOfWork.Commit();
        //        _repositoryFactory.End();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}


        //[TestMethod]
        //public void Save()
        //{
        //    try
        //    {
        //        //_repositoryFactory = new RepositoryFactory();
        //        //_repositoryFactory.Begin();
        //        //_unitOfWork = _repositoryFactory.UnitOfWork;
        //        //IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
        //        //Role role = roleRepository.GetByKey(System.Guid.Parse("E53A6CD8-04F9-4362-B7CF-7A9796259863"));
        //        //role.Name = "伪超级管理员";
        //        //roleRepository.Save(role);
        //        //_unitOfWork.Commit();

        //        //Role ret = roleRepository.GetByKey(System.Guid.Parse("E53A6CD8-04F9-4362-B7CF-7A9796259863"));
        //        //Assert.AreEqual(role, ret);

        //        //_repositoryFactory.End();
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        //[TestMethod]
        //public void Del()
        //{
        //    //_repositoryFactory = new RepositoryFactory();
        //    //_repositoryFactory.Begin();
        //    //_unitOfWork = _repositoryFactory.UnitOfWork;
        //    //IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
        //    //Role role = roleRepository.GetByKey(System.Guid.Parse("E53A6CD8-04F9-4362-B7CF-7A9796259863"));
        //    //roleRepository.Del(role);
        //    //_unitOfWork.Commit();

        //    //Role ret = roleRepository.GetByKey(System.Guid.Parse("E53A6CD8-04F9-4362-B7CF-7A9796259863"));
        //    ////Assert.AreEqual(role, ret);
        //    //Assert.IsNull(ret);

        //    //_repositoryFactory.End();
        //}

        [TestMethod]
        public void GetByKey()
        {
            try
            {
                _unitOfWork = new UnitOfWork();
                IRoleRepository roleRepository = new RoleUnitOfWorkRepository(_unitOfWork);
                Role role = roleRepository.GetByKey("TestUser");
                Role expect = new Role() { Id = "TestUser", Name = "测试用户" };
                Assert.AreEqual<Role>(expect,role);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void Add()
        {
            try
            {
                _unitOfWork = new UnitOfWork();
                IRoleRepository roleRepository = new RoleUnitOfWorkRepository(_unitOfWork);
                if (roleRepository.GetByKey("TestUser") == null)
                {
                    roleRepository.Add(new Role() { Id = "TestUser", Name = "测试用户" });
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void Save()
        {
            try
            {
                _unitOfWork = new UnitOfWork();
                IRoleRepository roleRepository = new RoleUnitOfWorkRepository(_unitOfWork);
                Role role = new Role() { Id = "TestUser", Name = "二次测试用户" };
                roleRepository.Save(role);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void Del()
        {
            try
            {
                _unitOfWork = new UnitOfWork();
                IRoleRepository roleRepository = new RoleUnitOfWorkRepository(_unitOfWork);
                Role role = new Role() { Id = "TestUser", Name = "二次测试用户" };
                roleRepository.Del(role);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
