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
    /// AuthorizeRepositoryTest 的摘要说明
    /// </summary>
    [TestClass]
    public class AuthorizeRepositoryTest
    {
        public AuthorizeRepositoryTest()
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

                IAuthorizeRepository authorizeRepository = _repositoryFactory.Get(typeof(IAuthorizeRepository), _unitOfWork) as IAuthorizeRepository;
                List<Authorize> list = authorizeRepository.GetAll().ToList();
                if (list == null) list = new List<Authorize>();
                if (list.Where(it => it.AuthorityName.Equals("Login")).FirstOrDefault() == null)
                {
                    authorizeRepository.Add(new Authorize() { AuthorityName = "Login", RoleName = "SuperManager,RegisterUser" });
                }
                if (list.Where(it => it.AuthorityName.Equals("Register")).FirstOrDefault() == null)
                {
                    authorizeRepository.Add(new Authorize() { AuthorityName = "Register", RoleName = "SuperManager,RegisterUser" });
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

            IAuthorizeRepository authorizeRepository = _repositoryFactory.Get(typeof(IAuthorizeRepository), _unitOfWork) as IAuthorizeRepository;
            Authorize authorize = authorizeRepository.GetByKey("Login");
            Assert.AreEqual<Authorize>(new Authorize() { AuthorityName = "Login", RoleName = "SuperManager,RegisterUser" }, authorize);

            _unitOfWork.Commit();
            _repositoryFactory.End();
        }

        [TestMethod]
        public void GetAll()
        {
            _repositoryFactory = new RepositoryFactory();
            _repositoryFactory.Begin();
            _unitOfWork = _repositoryFactory.UnitOfWork;

            IAuthorizeRepository authorizeRepository = _repositoryFactory.Get(typeof(IAuthorizeRepository), _unitOfWork) as IAuthorizeRepository;
            List<Authorize> list = authorizeRepository.GetAll().ToList();
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

            IAuthorizeRepository authorizeRepository = _repositoryFactory.Get(typeof(IAuthorizeRepository), _unitOfWork) as IAuthorizeRepository;
            authorizeRepository.Save(new Authorize() { AuthorityName = "Login", RoleName = "SuperManager" });

            _unitOfWork.Commit();
            _repositoryFactory.End();
        }

        [TestMethod]
        public void Del()
        {
            _repositoryFactory = new RepositoryFactory();
            _repositoryFactory.Begin();
            _unitOfWork = _repositoryFactory.UnitOfWork;

            IAuthorizeRepository authorizeRepository = _repositoryFactory.Get(typeof(IAuthorizeRepository), _unitOfWork) as IAuthorizeRepository;
            authorizeRepository.Del(new Authorize() { AuthorityName = "Login", RoleName = "SuperManager" });

            _unitOfWork.Commit();
            _repositoryFactory.End();
        }
    }
}
