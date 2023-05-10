using AutoMapper;
using WEBAPIForUserAuthorization.Dto;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
