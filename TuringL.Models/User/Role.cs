using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class Role:EntityBase,IAggregateRoot
    {
        public string Id { get; set; }
        public string Name { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }

    public interface IRoleRepository : IRepository<Role, System.String>
    { }
}
