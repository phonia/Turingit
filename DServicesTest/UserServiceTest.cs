using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TuringL.DServices;
using TuringL.DServices.Messages;
using TuringL.ViewModels;

namespace DServicesTest
{
    /// <summary>
    /// UserServiceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class UserServiceTest
    {
        public UserServiceTest()
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

        private UserService _userService = null;

        [TestInitialize]
        public void Init()
        {
            _userService = new UserService();
        }

        [TestMethod]
        public void GetRolesList()
        {
            try
            {
                GetRolesListResponse response = _userService.GetRolesList(new GetRolesListRequest());
                Assert.IsTrue(response.IsSucess && response.Roles != null && response.Roles.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void GetAuthoritiesList()
        {
            try
            {
                GetAuthoritiesListResponse response = _userService.GetAuthoritiesList(new GetAuthoritiesListRequest());
                Assert.IsTrue(response.IsSucess && response.Authorities != null && response.Authorities.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void GetUserList()
        {
            try
            {
                GetUsersListRequest request = null;
                GetUsersListResponse response = null;

                request = new GetUsersListRequest();
                response = _userService.GetUserList(request);
                Assert.IsTrue(response.IsSucess && response.Users != null && response.Users.Count > 0);

                request = new GetUsersListRequest() { SearchValue = "hy", SearchProperty = "Name"};
                response = _userService.GetUserList(request);
                Assert.IsTrue(response.IsSucess && response.Users != null && response.Users.Count > 0);

            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void RegisterUser()
        {
            try
            {
                RegisterUserResponse response = null;

                response = _userService.RegisterUser(new RegisterUserRequest() { Duty="技术员", RoleId="SuperManager", Email="hy@turingit.com", Password="polan", Name="hy" });
                Assert.IsTrue(response.IsSucess && response.User != null);

                response = _userService.RegisterUser(new RegisterUserRequest() { Duty = "技术员", RoleId = "SuperManager", Email = "hy@turingit.com", Password = "polan", Name = string.Empty });
                Assert.IsTrue(response.IsSucess == false&&response.Message.Contains("Name"));

                response = _userService.RegisterUser(new RegisterUserRequest() { Duty = "技术员", RoleId = "SuperManager", Email = "hy@turingit.com", Password = "polan", Name = "" });
                Assert.IsTrue(response.IsSucess == false && response.Message.Contains("Name"));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void Login()
        {
            try
            {
                LoginUserResponse response = null;

                response = _userService.Login(new LoginUserRequset() { Name="hy",Password="polan" });
                Assert.IsTrue(response.IsSucess && response.User != null);

                response = _userService.Login(new LoginUserRequset() { Name = "hy", Password = "" });
                Assert.IsTrue(response.IsSucess == false && response.Message.Contains("no suitable user!"));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void GetUserById()
        {
            try
            {
                GetUserByIdResponse response = null;

                response = _userService.GetUserById(new GetUserByIdRequest() { Id="" });
                Assert.IsTrue(response.IsSucess == false);

                response = _userService.GetUserById(new GetUserByIdRequest() { Id = "41A12BCC-E2A1-423E-B6A6-4E7196FA1279" });
                Assert.IsTrue(response.IsSucess);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            try
            {
                UpdateUserResponse response = null;

                LoginUserResponse login = _userService.Login(new LoginUserRequset() { Name = "hy", Password = "polan" });

                response = _userService.UpdateUser(new UpdateUserRequest()
                {
                    Id = login.User.Id,
                    RoleId = login.User.RoleId,
                    Email = login.User.Email,
                    Duty = login.User.Duty,
                    Password = "hy",
                    Name = login.User.Name
                });
                Assert.IsTrue(response.IsSucess && response.User != null);

                response = _userService.UpdateUser(new UpdateUserRequest()
                {
                    Id = login.User.Id,
                    RoleId = login.User.RoleId,
                    Email = login.User.Email,
                    Duty = login.User.Duty,
                    Password = "polan",
                    Name = login.User.Name
                });
                Assert.IsTrue(response.IsSucess && response.User != null);

            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
