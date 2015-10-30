using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class User:EntityBase,IAggregateRoot
    {
        protected override void Validate()
        {
            if (this.Name == string.Empty) AddBusinessRule(new BusinessRule() { Property = "Name", Rule = "用户名不为空" });
            if (this.Password == string.Empty) AddBusinessRule(new BusinessRule() { Property = "Password", Rule = "不为空" });
            if (this.Duty == string.Empty) AddBusinessRule(new BusinessRule() { Property = "Duty", Rule = "不为空" });
            if (this.Email == string.Empty) AddBusinessRule(new BusinessRule() { Property = "Email", Rule = "不为空" });
            if (this.RoleId == string.Empty) AddBusinessRule(new BusinessRule() { Property = "RoleId", Rule = "不为空" });
        }
    }

    public interface IUserRepository : IRepository<User, System.Guid>
    { }
}
