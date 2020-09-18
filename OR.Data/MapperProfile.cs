using System;
using AutoMapper;
using OR.Data.ViewModels;

namespace OR.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<MembershipRequest, MembershipRequestModel>();
            CreateMap<MembershipRequest, MembershipRequestResultModel>();
        }
    }
}
