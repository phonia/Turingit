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

        public string UpdateUser(string updateUser)
        {
            if (updateUser == null || updateUser == String.Empty)
            {
                return JsonHelper.SerializeObject("false:no enough parms!");
            }
            UserView userView = JsonHelper.DeserializeObject<UserView>(updateUser);
            if(userView==null)
            {
                return JsonHelper.SerializeObject("false:no enough parms!");
            }
            UpdateUserResponse response = _userService.UpdateUser(new UpdateUserRequest() { Duty=userView.Duty,Email=userView.Email,Id=userView.Id,Name=userView.Name,
            Password=userView.Password,RoleId=userView.RoleId});
            if (response.IsSucess == false || response.User == null)
            {
                return JsonHelper.SerializeObject("false:update error!");
            }
            return JsonHelper.SerializeObject(response.User);
        }

        public string GetRoles()
        {
            GetRolesListResponse response = _userService.GetRolesList(new GetRolesListRequest());
            if (response.IsSucess && response.Roles != null)
            {
                List<RoleView> list = response.Roles.Where(it => !it.Id.Equals("SuperManager")).ToList();
                return JsonHelper.SerializeObject(list);
            }
            else
            {
                return JsonHelper.SerializeObject("false" + response.Message);
            }
        }

        public string DelUser(string userId)
        {
            DelUserResponse response = _userService.DelUser(new DelUserRequest() {Id=userId });
            if (response.IsSucess == false)
                return JsonHelper.SerializeObject("false:error!");
            else
                return JsonHelper.SerializeObject("sucess!");
        }

        public string GetAllUsers()
        {
            GetUsersListResponse response = _userService.GetUserList(new GetUsersListRequest() { SearchProperty = string.Empty, SearchValue = string.Empty });
            if (response.IsSucess && response.Users != null)
            {
                List<UserView> list = response.Users.Where(it => !it.RoleId.Equals("SuperManager")).ToList();
                return JsonHelper.SerializeObject(list);
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
