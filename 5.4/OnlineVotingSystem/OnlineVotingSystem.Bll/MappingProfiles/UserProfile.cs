using AutoMapper;
using OnlineVotingSystem.Bll.Dtos;
using OnlineVotingSystem.Dal.Entities;

namespace OnlineVotingSystem.Bll.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}
