using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TuringL.Models;
using TuringL.Infrasturcture.Configuration;
using TuringL.Infrasturcture.XML;
using System.Data.Entity;

namespace TuringL.Repository
{
    public class RoleUnitOfWorkRepository : Repository<Role, System.String>, IRoleRepository
    {
        public RoleUnitOfWorkRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public string _rolePath = ConfigurationHelper.GetValue("rolePath");

        //protected List<Role> ReadXML()
        //{
        //    if (File.Exists(rolePath))
        //    {
        //        using (StreamReader streamReader = new StreamReader(rolePath))
        //        {
        //            XmlSerializer xml = new XmlSerializer(typeof(List<Role>));
        //            object ret = xml.Deserialize(streamReader) as object;
        //            if (ret != null)
        //                return (List<Role>)ret;
        //            else
        //                return null;
        //        }
        //    }
        //    return null;
        //}

        //protected void ReWriteXml(List<Role> list)
        //{
        //    if (File.Exists(rolePath))
        //    {
        //        using (StreamWriter streamWriter = new StreamWriter(rolePath))
        //        {
        //            XmlSerializer xml = new XmlSerializer(typeof(List<Role>));
        //            xml.Serialize(streamWriter, list);
        //        }
        //    }
        //}

        public override Role GetByKey(string Id)
        {
            List<Role> list = XMLSerialzationHelper.ReadXml<List<Role>>(_rolePath) as List<Role>;
            if (list != null)
                return list.Where(it => it.Id.Equals(Id)).FirstOrDefault();
            else 
                return null;
        }

        public override void PersistAdd(IAggregateRoot entity)
        {
            List<Role> list = XMLSerialzationHelper.ReadXml<List<Role>>(_rolePath) as List<Role>;
            if (list == null) list = new List<Role>();
            list.Add(entity as Role);
            XMLSerialzationHelper.WriteXML<List<Role>>(_rolePath, list);
        }

        public override void PersistDel(IAggregateRoot entity)
        {
            List<Role> list = XMLSerialzationHelper.ReadXml<List<Role>>(_rolePath) as List<Role>;
            if (list == null || !list.Contains(entity as Role))
                throw new Exception("error in PersistDel of RoleRepository!");
            list.Remove(entity as Role);
            XMLSerialzationHelper.WriteXML<List<Role>>(_rolePath, list);
        }

        public override void PersistSave(IAggregateRoot entity)
        {
            List<Role> list = XMLSerialzationHelper.ReadXml<List<Role>>(_rolePath) as List<Role>;
            if (list == null || list.Where(it => it.Id.Equals((entity as Role).Id)).FirstOrDefault() == null)
                throw new Exception("error in PersistDel of RoleRepository!");
            else
            {
                list.Remove(list.Where(it => it.Id.Equals((entity as Role).Id)).FirstOrDefault());
                list.Add(entity as Role);
            }
            XMLSerialzationHelper.WriteXML<List<Role>>(_rolePath, list);
        }

        public override IQueryable<Role> GetAll()
        {
            List<Role> list = XMLSerialzationHelper.ReadXml<List<Role>>(_rolePath) as List<Role>;
            if (list != null) 
                return list.AsQueryable();
            else
                return null;
        }
    }

    public class AuthorityUnitOfWorkRepository : Repository<Authority, System.String>, IAuthorityRepository
    {
        private string _authorityPath = ConfigurationHelper.GetValue("authorityPath");

        public AuthorityUnitOfWorkRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override Authority GetByKey(string Id)
        {
            List<Authority> list = XMLSerialzationHelper.ReadXml<List<Authority>>(_authorityPath) as List<Authority>;
            if (list != null)
                return list.Where(it => it.Id.Equals(Id)).FirstOrDefault();
            else
                return null;
        }

        public override void PersistAdd(IAggregateRoot entity)
        {
            List<Authority> list = XMLSerialzationHelper.ReadXml<List<Authority>>(_authorityPath) as List<Authority>;
            if (list == null) list = new List<Authority>();
            list.Add(entity as Authority);
            XMLSerialzationHelper.WriteXML<List<Authority>>(_authorityPath, list);
        }

        public override void PersistDel(IAggregateRoot entity)
        {
            List<Authority> list = XMLSerialzationHelper.ReadXml<List<Authority>>(_authorityPath) as List<Authority>;
            if (list == null || !list.Contains(entity as Authority))
                throw new Exception("error in persistDel of AuthorityUnitOfWorkRepository!");
            else
            {
                list.Remove(entity as Authority);
                XMLSerialzationHelper.WriteXML<List<Authority>>(_authorityPath, list);
            }
        }

        public override void PersistSave(IAggregateRoot entity)
        {
            List<Authority> list = XMLSerialzationHelper.ReadXml<List<Authority>>(_authorityPath) as List<Authority>;
            if (list == null || list.Where(it => it.Id.Equals((entity as Authority).Id)).FirstOrDefault() == null)
                throw new Exception("error in persistDel of AuthorityUnitOfWorkRepository!");
            else
            {
                list.Remove(list.Where(it => it.Id.Equals((entity as Authority).Id)).FirstOrDefault());
                list.Add(entity as Authority);
                XMLSerialzationHelper.WriteXML<List<Authority>>(_authorityPath, list);
            }
        }

        public override IQueryable<Authority> GetAll()
        {
            List<Authority> list = XMLSerialzationHelper.ReadXml<List<Authority>>(_authorityPath) as List<Authority>;
            return list.AsQueryable();
        }
    }

    public class AuthorizeUnitOfWorkRepository : Repository<Authorize, System.String>, IAuthorizeRepository
    {
        private string _authorizePath = ConfigurationHelper.GetValue("authorizePath");

        public AuthorizeUnitOfWorkRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override Authorize GetByKey(string Id)
        {
            List<Authorize> list = XMLSerialzationHelper.ReadXml<List<Authorize>>(_authorizePath) as List<Authorize>;
            if (list != null) return list.Where(it => it.AuthorityName.Equals(Id)).FirstOrDefault();
            else return null;
        }

        public override void PersistAdd(IAggregateRoot entity)
        {
            List<Authorize> list = XMLSerialzationHelper.ReadXml<List<Authorize>>(_authorizePath) as List<Authorize>;
            if (list == null) list = new List<Authorize>();
            list.Add(entity as Authorize);
            XMLSerialzationHelper.WriteXML<List<Authorize>>(_authorizePath, list);
        }

        public override void PersistDel(IAggregateRoot entity)
        {
            List<Authorize> list = XMLSerialzationHelper.ReadXml<List<Authorize>>(_authorizePath) as List<Authorize>;
            if (list == null || !list.Contains(entity as Authorize))
                throw new Exception("error in PersistDel of AuthorizeUnitOfWorkRepository!");
            else
            {
                list.Remove(entity as Authorize);
                XMLSerialzationHelper.WriteXML<List<Authorize>>(_authorizePath, list);
            }
        }

        public override void PersistSave(IAggregateRoot entity)
        {
            List<Authorize> list = XMLSerialzationHelper.ReadXml<List<Authorize>>(_authorizePath) as List<Authorize>;
            if (list == null || list.Where(it => it.AuthorityName.Equals((entity as Authorize).AuthorityName)).FirstOrDefault() == null)
                throw new Exception("");
            else
            {
                list.Remove(list.Where(it => it.AuthorityName.Equals((entity as Authorize).AuthorityName)).FirstOrDefault());
                list.Add(entity as Authorize);
                XMLSerialzationHelper.WriteXML<List<Authorize>>(_authorizePath, list);
            }
        }

        public override IQueryable<Authorize> GetAll()
        {
            List<Authorize> list = XMLSerialzationHelper.ReadXml<List<Authorize>>(_authorizePath) as List<Authorize>;
            return list.AsQueryable();
        }
    }

    public class UserUnitOfWorkRepository : Repository<User, System.Guid>, IUserRepository
    {
        public UserUnitOfWorkRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override User GetByKey(Guid Id)
        {
            var user = (GetAll() as IQueryable<User>).Where(it => it.Id == Id).FirstOrDefault();
            return user;
        }
    }
}
