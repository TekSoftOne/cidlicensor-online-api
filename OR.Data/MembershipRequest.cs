using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OR.Data
{
    public class MembershipRequest
    {
        [Key]
        public int MembershipRequestId { get; set; }
        [MaxLength(1000)]
        public string FullAddress { get; set; }
    }
}
