using AutoMapper;
using WebAPI.Business.User.Response;
using WebAPI.Data.Entity;

namespace WebAPI.Utilities.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>().ReverseMap();
    }
}