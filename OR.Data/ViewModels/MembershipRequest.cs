﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static OR.Data.Constants.Data;

namespace OR.Data.ViewModels
{
    public class MembershipRequestModel
    {
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string EmiratesIDNumber { get; set; }
        public int MembershipRequestId { get; set; }
        public int LocationId { get; set; }//T
        public Gender Gender { get; set; }//T
        public DateTime BirthDate { get; set; }//T
        public int NationId { get; set; }//T
        public int ReligionId { get; set; }//K
        public RequestCategory RequestCategory { get; set; }
        public TypeOfCustomer TypeOfCustomer { get; set; }

        public string Address { get; set; }//T -- //
        public int VisaResidency { get; set; }
        public string PassportNumber { get; set; }//T

        public string ProfilePic { get; set; }//T

        public IFormFile EmiratesIdBack { get; set; }//T

        public IFormFile EmiratesIdFront { get; set; }//

        public IFormFile AuthorizationLetter { get; set; }//

        public IFormFile PassportAttachement { get; set; }//T

        public string EmiratesIdBackUrl { get; set; }//T
        public string EmiratesIdFrontUrl { get; set; }//
        public string AuthorizationLetterUrl { get; set; }//
        public string PassportAttachementUrl { get; set; }//T
        public string Email { get; set; }//T
        public string PhoneNumber { get; set; }
        public int ApplicationNumber { get; set; }
        public string AreaId { get; set; }
        public int AgentId { get; set; }
        public int MembershipId { get; set; }
        public string MembershipNumber { get; set; }
        public Status Status { get; set; }
        public string OrderRef { get; set; }
        public string PaymentType { get; set; }
        public int MonthlySalary { get; set; }
        public int monthlyQuota { get; set; }
        public string Comment { get; set; }
    }

    public class MembershipRequestResultModel
    {
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string EmiratesIDNumber { get; set; }
        public int MembershipRequestId { get; set; }
        public int LocationId { get; set; }//T
        public Gender Gender { get; set; }//T
        public DateTime BirthDate { get; set; }//T
        public int NationId { get; set; }//T
        public int ReligionId { get; set; }//K
        public string RequestCategory { get; set; }
        public string TypeOfCustomer { get; set; }

        public string Address { get; set; }//T -- //
        public int VisaResidency { get; set; }
        public string PassportNumber { get; set; }//T

        public string ProfilePic { get; set; }//T

        public IFormFile EmiratesIdBack { get; set; }//T

        public IFormFile EmiratesIdFront { get; set; }//

        public IFormFile AuthorizationLetter { get; set; }//

        public IFormFile PassportAttachement { get; set; }//T
        public string ProfilePhotoUrl { get; set; }//T
        public string EmiratesIdBackUrl { get; set; }//T
        public string EmiratesIdFrontUrl { get; set; }//
        public string AuthorizationLetterUrl { get; set; }//
        public string PassportAttachementUrl { get; set; }//T
        public string Email { get; set; }//T
        public string PhoneNumber { get; set; }
        public int ApplicationNumber { get; set; }
        public string AreaId { get; set; }
        public int AgentId { get; set; }
        public int MembershipId { get; set; }
        public string MembershipNumber { get; set; }
        public Status Status { get; set; }
        public string OrderRef { get; set; }
        public string PaymentType { get; set; }
        public int MonthlySalary { get; set; }
        public int monthlyQuota { get; set; }
        public string Comment { get; set; }
    }
}
