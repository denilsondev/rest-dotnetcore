using RestWithAspNet.Model.Base;
using System;
using System.Runtime.Serialization;

namespace RestWithAspNet.Data.VO
{
    [DataContract]
    public class BookVO
    {
        [DataMember (Order = 1, Name = "id")]
        public long Id { get; set; }

        [DataMember(Order = 2, Name = "title")]
        public string Title { get; set; }

        [DataMember(Order = 3, Name = "name")]
        public string Author { get; set; }

        [DataMember(Order = 4, Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Order = 5, Name = "laucnData")]
        public DateTime LaunchDate { get; set; }
    }
}
