using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TuringL.Models;
using TuringL.Repository;
using TuringL.DServices.Messages;
using System.Linq.Expressions;

namespace TuringL.DServices
{
    public class UserService : IUserService
    {
        protected IUnitOfWork _unitOfWork = null;
        protected IRepositoryFactory _repositoryFactory = null;

        public UserService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public UserService()
        {
            _repositoryFactory = new  RepositoryFactory();
        }

        public GetAuthoritiesListResponse GetAuthoritiesList(GetAuthoritiesListRequest request)
        {
            GetAuthoritiesListResponse response = new GetAuthoritiesListResponse();
            try
            {
                if (request == null) throw new Exception("null Input!");

                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IAuthorityRepository authorityRepository = _repositoryFactory.Get(typeof(IAuthorityRepository), _unitOfWork) as IAuthorityRepository;
                List<Authority> authorities = authorityRepository.GetAll().ToList();
                if (authorities == null) throw new Exception("no Authority in GetAuthoritiesList of UserService!");

                _unitOfWork.Commit();
                _repositoryFactory.End();

                foreach (var item in authorities)
                {
                    ViewModels.AuthorityView node = new ViewModels.AuthorityView() { Id = item.Id, Name = item.Name };
                    if (response.Authorities == null) response.Authorities = new List<ViewModels.AuthorityView>();
                    response.Authorities.Add(node);
                }
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }
            return response;
        }

        public GetRolesListResponse GetRolesList(GetRolesListRequest request)
        {
            GetRolesListResponse response = new GetRolesListResponse();
            try
            {
                if (request == null) throw new Exception("null Input!");

                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
                IAuthorizeRepository authorizeRepository = _repositoryFactory.Get(typeof(IAuthorizeRepository), _unitOfWork) as IAuthorizeRepository;
                IAuthorityRepository authorityRepositroy = _repositoryFactory.Get(typeof(IAuthorityRepository), _unitOfWork) as IAuthorityRepository;
                List<Authorize> authorizes = authorizeRepository.GetAll().ToList();
                List<Authority> authorities = authorityRepositroy.GetAll().ToList();
                List<Role> roles = roleRepository.GetAll().ToList();
                if (authorities == null || authorizes == null || roles == null) throw new Exception("no Authority.Authorize.Role in GetRoleList of UserService!");

                _unitOfWork.Commit();
                _repositoryFactory.End();

                response.Roles = new List<ViewModels.RoleView>();
                foreach (var item in roles)
                {
                    ViewModels.RoleView node = new ViewModels.RoleView();
                    node.Id = item.Id;
                    node.Name = item.Name;
                    List<Authorize> selectedAuthorizes = authorizes.Where(it => it.RoleName.Contains(item.Id)).ToList();
                    if (selectedAuthorizes != null)
                    {
                        foreach (var seletecd in selectedAuthorizes)
                        {
                            var selectedAuthority = authorities.Where(it => it.Id == seletecd.AuthorityName).FirstOrDefault();
                            if (selectedAuthority != null)
                            {
                                if (node.Authorities == null) node.Authorities = new List<ViewModels.AuthorityView>();
                                node.Authorities.Add(new ViewModels.AuthorityView() { Id = selectedAuthority.Id, Name = selectedAuthority.Name });
                            }
                        }
                    }

                    response.Roles.Add(node);
                }
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }
            return response;
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest request)
        {
            RegisterUserResponse response = new RegisterUserResponse();
            try
            {
                if (request == null) throw new Exception("null Input!");

                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IUserRepository userRepository = _repositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
                User user = new User() { Id = System.Guid.NewGuid(), Name = request.Name, Password = request.Password, Duty = request.Duty, Email = request.Email, RoleId = request.RoleId };
                user.IsValidated();
                userRepository.Add(user);
                Role role = roleRepository.GetByKey(request.RoleId);
                if (role == null) throw new Exception("no role in RegisterUser of UserService!");

                _unitOfWork.Commit();
                _repositoryFactory.End();

                response.IsSucess = true;
                response.User = new ViewModels.UserView()
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Password = user.Password,
                    Duty = user.Duty,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = role.Name
                };
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }
            return response;
        }

        public LoginUserResponse Login(LoginUserRequset request)
        {
            LoginUserResponse response = new LoginUserResponse();
            try
            {
                if (request == null) throw new Exception("null Input!");

                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IUserRepository userRepository = _repositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
                List<User> list=userRepository.GetAll().ToList();
                if (list == null) throw new Exception("no User in Login of Userservic!");
                User user = list.Where(it => it.Name == request.Name && it.Password == request.Password).FirstOrDefault();
                if (user == null) throw new Exception("no suitable user where name:"+request.Name+" password:"+request.Password);
                Role role = roleRepository.GetByKey(user.RoleId);
                if (role == null) throw new Exception("no role in Login of UserService!");

                _unitOfWork.Commit();
                _repositoryFactory.End();

                ViewModels.UserView userView = new ViewModels.UserView()
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    Duty = user.Duty,
                    RoleId = user.RoleId,
                    RoleName = role.Name
                };
                response.IsSucess = true;
                response.User = userView;

            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }
            return response;
        }

        public GetUserByIdResponse GetUserById(GetUserByIdRequest request)
        {
            GetUserByIdResponse response = new GetUserByIdResponse();
            try
            {
                if (request == null) throw new Exception("null Input!");

                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IUserRepository userRepository = _repositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
                User user = userRepository.GetByKey(System.Guid.Parse(request.Id));
                if (user == null) throw new Exception("no user in GetUserById of UserService!");
                Role role = roleRepository.GetByKey(user.RoleId);
                if (role == null) throw new Exception("no role in GetUserById of UserService!");

                _unitOfWork.Commit();
                _repositoryFactory.End();

                response.IsSucess = true;
                response.User = new ViewModels.UserView() { 
                Id=user.Id.ToString(),
                Name=user.Name,
                Duty=user.Duty,
                Email=user.Email,
                RoleId=user.RoleId,
                RoleName=role.Name
                };
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }
            return response;
        }

        public GetUsersListResponse GetUserList(GetUsersListRequest request)
        {
            GetUsersListResponse response = new GetUsersListResponse();
            try
            {
                if (request == null) throw new Exception("null Input!");

                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IUserRepository userRepository = _repositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                IRoleRepository roleRepository = _repositoryFactory.Get(typeof(IRoleRepository), _unitOfWork) as IRoleRepository;
                List<Role> roles = roleRepository.GetAll().ToList();
                List<User> list = null;

                if (request.SearchProperty != null && request.SearchProperty != string.Empty)
                {
                    if (typeof(User).GetProperty(request.SearchProperty) != null)
                    {
                        list = userRepository.GetAll().Where(LambdaConstruct.ParameterPropertyEqualConstant<User>(request.SearchProperty, request.SearchValue)).ToList();
                    }
                    else
                        throw new Exception("no SearchProperty in GetUserlist of Userservice!");
                }
                else
                {
                    list = userRepository.GetAll().ToList();
                }

                if (list == null) throw new Exception("no User in GetUserList of UserService!");

                response.Users = new List<ViewModels.UserView>();
                foreach (var item in list)
                {
                    Role role = roles.Where(it => it.Id == item.RoleId).FirstOrDefault();
                    if (role == null) throw new Exception("no Role in GetUserList of UserService!");
                    else
                    {
                        ViewModels.UserView useView = new ViewModels.UserView()
                        {
                            Id = item.Id.ToString(),
                            Name = item.Name,
                            Password = item.Password,
                            Duty = item.Duty,
                            Email = item.Email,
                            RoleId = item.RoleId,
                            RoleName = role.Name
                        };
                        response.Users.Add(useView);
                    }
                }
                response.IsSucess = true;

                _unitOfWork.Commit();
                _repositoryFactory.End();
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }

            return response;
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            UpdateUserResponse response = new UpdateUserResponse();
            try
            {
                if (request == null) throw new Exception("null Input!");

                _repositoryFactory.Begin();
                _unitOfWork=_repositoryFactory.UnitOfWork;

                IUserRepository userRepository=_repositoryFactory.Get(typeof(IUserRepository),_unitOfWork) as IUserRepository;
                IRoleRepository roleRepository=_repositoryFactory.Get(typeof(IRoleRepository),_unitOfWork) as IRoleRepository;
                User user = userRepository.GetByKey(System.Guid.Parse(request.Id));
                Role role = roleRepository.GetByKey(request.RoleId);
                if (user == null) throw new Exception("no User in UpdateUser of UserService!");
                if (role == null) throw new Exception("no role in UpdateUser of UserService!");
                user.Name = request.Name;
                user.Password = request.Password;
                user.RoleId = request.RoleId;
                user.Duty = request.Duty;
                user.Email = request.Email;
                userRepository.Save(user);

                _unitOfWork.Commit();
                _repositoryFactory.End();

                response.IsSucess = true;
                response.User = new ViewModels.UserView() { 
                Id=user.Id.ToString(),
                Name=user.Name,
                Duty=user.Duty,
                Email=user.Email,
                RoleId=user.RoleId,
                RoleName=role.Name
                };
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }
            return response;
        }

        public DelUserResponse DelUser(DelUserRequest request)
        {
            DelUserResponse response = new DelUserResponse();
            try
            {
                if (request == null) throw new Exception("null input!");

                _repositoryFactory.Begin();
                _unitOfWork = _repositoryFactory.UnitOfWork;

                IUserRepository userRepository = _repositoryFactory.Get(typeof(IUserRepository), _unitOfWork) as IUserRepository;
                userRepository.Del(new User() { Id = System.Guid.Parse(request.Id) });

                _unitOfWork.Commit();
                _repositoryFactory.End();

                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (_repositoryFactory != null) _repositoryFactory.End();
            }
            return response;
        }

    }
}
