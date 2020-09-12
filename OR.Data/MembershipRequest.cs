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
        [MaxLength(250)]
        public string Name { get; set; }//T
        //public int LocationId { get; set; }//T
        //public int GenderId { get; set; }//T
        //public DateTime BirthDay { get; set; }//T
        //public int NationId { get; set; }//T
        //[MaxLength(250)]
        //public string Occupation { get; set; }//T -- //
        //public int VisaResidency { get; set; }
        //[MaxLength(250)]
        //public string EmiratesIdNumber { get; set; }
        //[MaxLength(250)]
        //public string PassportNumber { get; set; }//T
        //[MaxLength(250)]
        //public string ProfilePhoto { get; set; }//T

        //public string EmiratesIdBack { get; set; }//T

        //public string EmiratesIdFront { get; set; }//

        //public string AuthorizationLetter { get; set; }//

        //public string PassportAttachement { get; set; }//T
        //[MaxLength(250)]
        //public string EmailAddress { get; set; }//T


    }
}
