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
    /// UserRepositoryTest 的摘要说明
    /// </summary>
    [TestClass]
    public class UserRepositoryTest
    {
        public UserRepositoryTest()
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

        [TestMethod]
        public void Add()
        {
            try
            {
                _unitOfWork = RepositoryFactory.GetUnitOfWork();

                IUserRepository userRepository = RepositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                if (userRepository.GetByKey(_id) == null)
                {
                    userRepository.Add(new User() { Id = _id, Duty = "管理员", Name = "hy", Email = "bbs", RoleId = "SuperManager",Password="hy" });
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void GetAll()
        {
            try
            {
                _unitOfWork = RepositoryFactory.GetUnitOfWork();

                IUserRepository userRepository = RepositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                List<User> list = userRepository.GetAll().ToList();
                Assert.IsTrue(list != null && list.Count > 0);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        private System.Guid _id = System.Guid.Parse("FC22A4D5-0BD7-4AD8-B77F-184B21A3BBF0");

        [TestMethod]
        public void Get()
        {
            try
            {
                _unitOfWork = RepositoryFactory.GetUnitOfWork();

                IUserRepository userRepository = RepositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                User user = userRepository.GetByKey(_id);

                Assert.AreEqual<System.Guid>(user.Id, _id);

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
                _unitOfWork = RepositoryFactory.GetUnitOfWork();

                IUserRepository userRepository = RepositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;

                User user = userRepository.GetByKey(_id);
                user.Name = "polan";
                userRepository.Save(user);

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
                _unitOfWork = RepositoryFactory.GetUnitOfWork();

                IUserRepository userRepository = RepositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                userRepository.Del(new User() { Id = _id });

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

    }
}
