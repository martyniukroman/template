using AutoMapper;
using hsl.api.Models;
using hsl.api.ViewModels;

namespace hsl.api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationUserViewModel, User>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
           // CreateMap<RegistrationUserViewModel, Customer>();
           // CreateMap<Customer, RegistrationUserViewModel>();
        }
    }
}