using AutoMapper;
using hsl.api.Models;
using hsl.api.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace hsl.api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationUserViewModel, User>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            CreateMap<IdentityUser, Customer>();
            // CreateMap<RegistrationUserViewModel, Customer>();
            // CreateMap<Customer, RegistrationUserViewModel>();
        }
    }
}