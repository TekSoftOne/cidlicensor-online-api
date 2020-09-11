using System;
using System.ComponentModel.DataAnnotations;

namespace OR.Data
{
    public class MembershipRequest
    {
        [Key]
        public int MembershipRequestId { get; set; }
        public string FullAddress { get; set; }
        public string ApplicationNumber { get; set; }
    }
}
