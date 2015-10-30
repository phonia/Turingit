using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TuringL.ViewModels
{
    [DataContract]
    public class RoleView
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<AuthorityView> Authorities { get; set; }
    }
}
