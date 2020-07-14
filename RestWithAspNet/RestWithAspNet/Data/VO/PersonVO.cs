using RestWithAspNet.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RestWithAspNet.Data.VO
{
    [DataContract]
    public class PersonVO
    {
        [DataMember (Order = 1, Name = "id")]
        public long Id { get; set; }

        [DataMember(Order = 2, Name = "lastName")]
        public string FirstName { get; set; }

        [DataMember(Order = 3, Name = "order")]
        public string LastName { get; set; }

        [DataMember(Order = 4, Name = "codigo")]
        public string Address { get; set; }

        [DataMember(Order = 5, Name = "gender")]
        public string Gender { get; set; }
    }
}
