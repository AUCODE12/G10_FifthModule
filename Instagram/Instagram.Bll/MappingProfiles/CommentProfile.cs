using AutoMapper;
using Instagram.Bll.Dtos;
using Instagram.Dal.Entities;

namespace Instagram.Bll.MappingProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(_ => DateTime.UtcNow));


        CreateMap<Comment, CommentGetDto>();
    }
}
