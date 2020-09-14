﻿using System;
using Microsoft.AspNetCore.Http;
using static OR.Data.Constants.Data;

namespace OR.Data.ViewModels
{
    public class MembershipRequestModel
    {
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string EmiratesIdNumber { get; set; }
        public int MembershipRequestId { get; set; }
        public int LocationId { get; set; }//T
        public Gender GenderId { get; set; }//T
        public DateTime BirthDay { get; set; }//T
        public int NationId { get; set; }//T
        public int ReligionId { get; set; }//K

        public string Occupation { get; set; }//T -- //
        public int VisaResidency { get; set; }
        public string PassportNumber { get; set; }//T
        public IFormFile ProfilePhoto { get; set; }//T
        public IFormFile EmiratesIdBack { get; set; }//T
        public IFormFile EmiratesIdFront { get; set; }//
        public IFormFile AuthorizationLetter { get; set; }//
        public IFormFile PassportAttachement { get; set; }//T
        public string ProfilePhotoUrl { get; set; }//T
        public string EmiratesIdBackUrl { get; set; }//T
        public string EmiratesIdFrontUrl { get; set; }//
        public string AuthorizationLetterUrl { get; set; }//
        public string PassportAttachementUrl { get; set; }//T
        public string EmailAddress { get; set; }//T
    }
}
