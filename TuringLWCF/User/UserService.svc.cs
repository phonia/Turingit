using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TuringL.DServices;
using TuringL.DServices.Messages;
using TuringL.Infrasturcture;
using TuringL.ViewModels;
using TuringL.Infrasturcture.Json;
using System.ServiceModel.Activation;


namespace TuringLWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserService : IUserService
    {
        private TuringL.DServices.IUserService _userService = new TuringL.DServices.UserService();

        public string Login(string name, string password)
        {
            LoginUserResponse response = _userService.Login(new LoginUserRequset() { Name = name, Password = password });
            if (response.IsSucess&&response.User!=null)
            {
                return JsonHelper.SerializeObject(response.User);
            }
            else
            {
                return JsonHelper.SerializeObject("false" + response.Message);
            }
        }

        public string Register(string registerUser)
        {
            UserView register = null;
            try
            {
                register = JsonHelper.DeserializeObject<UserView>(registerUser);
            }
            catch (Exception ex)
            { }
            if (register == null) return JsonHelper.SerializeObject("false" + "无效的注册信息！");


            GetUsersListResponse userListResponse = _userService.GetUserList(new GetUsersListRequest() { SearchProperty = "Name", SearchValue = register.Name });
            if (userListResponse.IsSucess != false && userListResponse.Users != null && userListResponse.Users.Count > 0)
                return JsonHelper.SerializeObject("false" + "已注册的用户名！");
            RegisterUserResponse response = _userService.RegisterUser(
                new RegisterUserRequest() 
                { 
                    Name = register.Name, 
                    Password = register.Password,
                    Email = register.Email, 
                    RoleId = register.RoleId, 
                    Duty = register.Duty
                });


            if (response.IsSucess && response.User != null)
            {
                return JsonHelper.SerializeObject(response.User);
            }
            else
            {
                return JsonHelper.SerializeObject("false" + response.Message);
            }
        }

        public string Update(string loginUser, string updateUser)
        {
            throw new NotImplementedException();
        }

        public string GetRoles()
        {
            GetRolesListResponse response = _userService.GetRolesList(new GetRolesListRequest());
            if (response.IsSucess && response.Roles != null)
            {
                return JsonHelper.SerializeObject(response.Roles);
            }
            else
            {
                return JsonHelper.SerializeObject("false" + response.Message);
            }
        }

        public string UpdateUser(string updateUser)
        {
            throw new NotImplementedException();
        }

        public string DelUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public string GetAllUsers()
        {
            GetRolesListResponse response = _userService.GetRolesList(new GetRolesListRequest());
            if (response.IsSucess && response.Roles != null && response.Roles.Count > 0)
            {
                return JsonHelper.SerializeObject(response.Roles);
            }
            else
            {
                return JsonHelper.SerializeObject("false:" + response.Message);
            }
        }

        public string GetUserRoleAuthorities(string roleId)
        {
            GetRolesListResponse response = _userService.GetRolesList(new GetRolesListRequest());
            if (response.IsSucess&&response.Roles!=null&&response.Roles.Count>0)
            {
                RoleView roleView = response.Roles.Where(it => it.Id.Equals(roleId)).FirstOrDefault();
                if (roleView != null)
                    return JsonHelper.SerializeObject(roleView);
                else
                    return JsonHelper.SerializeObject("false:" + "no authorityView!");
            }
            else
            {
                return JsonHelper.SerializeObject("false:" + response.Message);
            }
        }

    }
}
