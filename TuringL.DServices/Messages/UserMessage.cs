using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.ViewModels;

namespace TuringL.DServices.Messages
{
    public class GetAuthoritiesListRequest : RequestBase
    { }

    public class GetAuthoritiesListResponse : ResponseBase
    {
        public List<AuthorityView> Authorities { get; set; }
    }

    public class GetRolesListRequest : RequestBase
    { }

    public class GetRolesListResponse : ResponseBase
    {
        public List<RoleView> Roles { get; set; }
    }

    public class GetUsersListRequest : RequestBase
    {
        public string SearchProperty { get; set; }
        public string SearchValue { get; set; }
    }

    public class GetUsersListResponse : ResponseBase
    {
        public List<UserView> Users { get; set; }
    }

    public class GetUserByIdRequest : RequestBase
    {
        public string Id { get; set; }
    }

    public class GetUserByIdResponse : ResponseBase
    {
        public ViewModels.UserView User { get; set; }
    }

    public class RegisterUserRequest : RequestBase
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Duty { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
    }

    public class RegisterUserResponse : ResponseBase
    {
        public UserView User { get; set; }
    }

    public class UpdateUserRequest : RequestBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Duty { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
    }

    public class UpdateUserResponse : ResponseBase
    {
        public UserView User { get; set; }
    }

    public class LoginUserRequset : RequestBase
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserResponse : ResponseBase
    {
        public UserView User { get; set; }
    }

    public class DelUserRequest : RequestBase
    {
        public string Id { get; set; }
    }

    public class DelUserResponse : ResponseBase
    { }
}
