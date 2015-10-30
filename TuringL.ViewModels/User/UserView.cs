using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TuringL.ViewModels
{
    [DataContract]
    public class UserView
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Duty { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string RoleId { get; set; }
        [DataMember]
        public string RoleName { get; set; }
    }
}
