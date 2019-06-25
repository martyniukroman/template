namespace hsl.dto.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, RegistrationUserViewModel>();
            CreateMap<RegistrationUserViewModel, AppUser>();
        }
    }
}