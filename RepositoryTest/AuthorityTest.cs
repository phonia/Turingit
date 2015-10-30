using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TuringL.Models;
using TuringL.Repository;

namespace RepositoryTest
{
    /// <summary>
    /// AuthorityTest 的摘要说明
    /// </summary>
    [TestClass]
    public class AuthorityTest
    {
        public AuthorityTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

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

        protected IUnitOfWork _unitOfWork = null;
        protected IRepositoryFactory _repositoryFactory = null;

        [TestMethod]
        public void Add()
        {
            try
            {
                _repositoryFactory = new RepositoryFactory();
                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IAuthorityRepository authorityRepository = _repositoryFactory.Get(typeof(IAuthorityRepository),_unitOfWork) as IAuthorityRepository;
                List<Authority> list = authorityRepository.GetAll().ToList();
                if (list == null) list = new List<Authority>();
                if (list.Where(it => it.Id.Equals("Login")).FirstOrDefault() == null)
                {
                    authorityRepository.Add(new Authority() { Id = "Login", Name = "登录" });
                }
                if (list.Where(it => it.Id.Equals("Register")).FirstOrDefault() == null)
                {
                    authorityRepository.Add(new Authority() { Id = "Register", Name = "注册" });
                }
                

                _unitOfWork.Commit();
                _repositoryFactory.End();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void GetByKey()
        {
            _repositoryFactory = new RepositoryFactory();
            _repositoryFactory.Begin();
            _unitOfWork = _repositoryFactory.UnitOfWork;

            IAuthorityRepository authorityRepository = _repositoryFactory.Get(typeof(IAuthorityRepository), _unitOfWork) as IAuthorityRepository;
            Authority authority= authorityRepository.GetByKey("Login");
            Assert.AreEqual<Authority>(new Authority() { Id = "Login", Name = "登录" }, authority);

            _unitOfWork.Commit();
            _repositoryFactory.End();
        }

        [TestMethod]
        public void GetAll()
        {
            _repositoryFactory = new RepositoryFactory();
            _repositoryFactory.Begin();
            _unitOfWork = _repositoryFactory.UnitOfWork;

            IAuthorityRepository authorityRepository = _repositoryFactory.Get(typeof(IAuthorityRepository), _unitOfWork) as IAuthorityRepository;
            List<Authority> list = authorityRepository.GetAll().ToList();
            Assert.IsTrue(list != null && list.Count > 0);

            _unitOfWork.Commit();
            _repositoryFactory.End();
        }

        [TestMethod]
        public void Update()
        {
            _repositoryFactory = new RepositoryFactory();
            _repositoryFactory.Begin();
            _unitOfWork = _repositoryFactory.UnitOfWork;

            IAuthorityRepository authorityRepository = _repositoryFactory.Get(typeof(IAuthorityRepository), _unitOfWork) as IAuthorityRepository;
            authorityRepository.Save(new Authority() { Id = "Register", Name = "二次注册" });

            _unitOfWork.Commit();
            _repositoryFactory.End();
        }

        [TestMethod]
        public void Del()
        {
            _repositoryFactory = new RepositoryFactory();
            _repositoryFactory.Begin();
            _unitOfWork = _repositoryFactory.UnitOfWork;

            IAuthorityRepository authorityRepository = _repositoryFactory.Get(typeof(IAuthorityRepository), _unitOfWork) as IAuthorityRepository;
            authorityRepository.Del(new Authority() { Id = "Register", Name = "二次注册" });

            _unitOfWork.Commit();
            _repositoryFactory.End();
        }
    }
}
