using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class Authorize:EntityBase,IAggregateRoot
    {
        public string RoleName { get; set; }
        public string AuthorityName { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }

    public interface IAuthorizeRepository : IRepository<Authorize, System.String>
    { }
}
