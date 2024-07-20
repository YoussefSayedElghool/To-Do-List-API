using AutoMapper;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, User>();
        }
    }
}
