using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OR.Data.Constants.Data;

namespace OR.Data
{
    public class MembershipRequest
    {
        [Key]
        public int MembershipRequestId { get; set; }
        [MaxLength(1000)]
        public string FullAddress { get; set; }
        [MaxLength(250)]
        public string FullName { get; set; }//T
        public int LocationId { get; set; }//T
        public int Gender { get; set; }//T
        public DateTime BirthDate { get; set; }//T
        public int NationId { get; set; }//T
        public int ReligionId { get; set; }//K
        [MaxLength(250)]
        public string Address { get; set; }//T -- //
        public int VisaResidency { get; set; }
        [MaxLength(250)]
        public string EmiratesIDNumber { get; set; }
        [MaxLength(250)]
        public string PassportNumber { get; set; }//T
        [MaxLength(250)]
        public string ProfilePhotoUrl { get; set; }//T

        public string EmiratesIdBackUrl { get; set; }//T

        public string EmiratesIdFrontUrl { get; set; }//

        public string AuthorizationLetterUrl { get; set; }//

        public string PassportAttachementUrl { get; set; }//T
        [MaxLength(250)]
        public string Email { get; set; }//T
        public int RequestCategory { get; set; }
        public string PhoneNumber { get; set; }
        public int TypeOfCustomer { get; set; }
        public string AreaId { get; set; }
        public int AgentId { get; set; }
        public int MembershipId { get; set; }
        public string MembershipNumber { get; set; }

    }
}
