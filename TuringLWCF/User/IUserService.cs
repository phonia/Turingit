using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TuringLWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// login
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录成功返回UserView，失败则返回失败信息</returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string Login(string name, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerUser">新注册用户信息：UserView</param>
        /// <returns>注册成功则返回新用户UserView,失败则返回失败信息:false</returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string Register(string registerUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateUser">修改后的用户信息:UserView</param>
        /// <returns>修改成功则返回用户信息：UserView,失败则返回失败信息:false</returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateUser(string updateUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns>成功返回True:False</returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DelUser(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>返回用户列表：UserView；失败返回失败信息：false</returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetAllUsers();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>成功则返回角色数据：AuthorityView,失败则返回失败信息:False</returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns>修改成功则返回用户权限：AuthorityView,失败则返回失败信息：false</returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetUserRoleAuthorities(string roleId);
    }
}
