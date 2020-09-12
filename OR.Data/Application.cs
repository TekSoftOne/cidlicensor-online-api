using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OR.Data
{
    public class Application
    {
        [Key]
        public int ApplicationNumber { get; set; }

        public int MembershipId { get; set; }
        [ForeignKey("MembershipId")]
        public virtual MembershipRequest MembershipRequest { get; set; }
    }
}
