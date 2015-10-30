using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.DServices
{
    public interface IUserService
    {
        TuringL.DServices.Messages.DelUserResponse DelUser(TuringL.DServices.Messages.DelUserRequest request);
        TuringL.DServices.Messages.GetAuthoritiesListResponse GetAuthoritiesList(TuringL.DServices.Messages.GetAuthoritiesListRequest request);
        TuringL.DServices.Messages.GetRolesListResponse GetRolesList(TuringL.DServices.Messages.GetRolesListRequest request);
        TuringL.DServices.Messages.GetUserByIdResponse GetUserById(TuringL.DServices.Messages.GetUserByIdRequest request);
        TuringL.DServices.Messages.GetUsersListResponse GetUserList(TuringL.DServices.Messages.GetUsersListRequest request);
        TuringL.DServices.Messages.LoginUserResponse Login(TuringL.DServices.Messages.LoginUserRequset request);
        TuringL.DServices.Messages.RegisterUserResponse RegisterUser(TuringL.DServices.Messages.RegisterUserRequest request);
        TuringL.DServices.Messages.UpdateUserResponse UpdateUser(TuringL.DServices.Messages.UpdateUserRequest request);
    }
}
