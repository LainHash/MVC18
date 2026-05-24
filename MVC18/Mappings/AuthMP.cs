using AutoMapper;
using MVC18.DTOs.Auth;
using MVC18.Models;

namespace MVC18.Mappings
{
    public class AuthMP : Profile
    {
        public AuthMP()
        {
            CreateMap<RegisterDTO, User>();
            CreateMap<RegisterDTO, PersonalInformation>();
        }
    }
}
