﻿using System;
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
        [Required]
        public string FullName { get; set; }//T
        [Required]
        public int LocationId { get; set; }//T
        [Required]
        public int Gender { get; set; }//T
        [Required]
        public DateTime BirthDate { get; set; }//T
        [Required]
        public int NationId { get; set; }//T
        public int ReligionId { get; set; }//K
        [MaxLength(250)]
        public string Address { get; set; }//T -- //
        public int VisaResidency { get; set; }
        [MaxLength(250)]
        public string EmiratesIDNumber { get; set; }
        [MaxLength(250)]
        public string PassportNumber { get; set; }//T
        [Required]
        public string ProfilePic { get; set; }//T

        public string EmiratesIdBackUrl { get; set; }//T

        public string EmiratesIdFrontUrl { get; set; }//

        public string AuthorizationLetterUrl { get; set; }//

        public string PassportAttachementUrl { get; set; }//T
        [MaxLength(250)]
        [Required]
        public string Email { get; set; }//T
        [Required]
        public int RequestCategory { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int TypeOfCustomer { get; set; }
        public string AreaId { get; set; }
        [Required]
        public int AgentId { get; set; }
        [Required]
        public int MembershipId { get; set; }
        [Required]
        public string MembershipNumber { get; set; }
        public int Status { get; set; }
        [MaxLength(250)]
        public string OrderRef { get; set; }
        [MaxLength(50)]
        public string PaymentType { get; set; }
        public decimal monthlyQuota { get; set; }
        public decimal monthlySalary { get; set; }
        public string Comment { get; set; }

    }
}
