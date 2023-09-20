using Application.Users;
using AuthAPI.ViewModels;
using AutoMapper;

namespace AuthAPI.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserResponse, ApplicationUser>()
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(user => user.Email, opt => opt.MapFrom(src => src.Login.ToString()))
                .ForMember(user => user.UserName, opt => opt.MapFrom(src => src.Name.ToString()))
                .ForMember(user => user.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<ApplicationUser, CreateUserCommand>()
                .ForCtorParam("Password", opt => opt.MapFrom(src => src.PasswordHash))
                .ForCtorParam("Login", opt => opt.MapFrom(src => src.Email.ToString()))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.UserName.ToString()))
                .ForCtorParam("Role", opt => opt.MapFrom(src => src.Role));
        }
    }
}
