using AutoMapper;
using JJIMP.API.Model;

namespace JJIMP.API.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserInDTO>().ReverseMap();
        CreateMap<User, UserOutDTO>().ReverseMap();
        CreateMap<Project, ProjectInDTO>().ReverseMap();
        CreateMap<Project, ProjectOutDTO>().ReverseMap();
        CreateMap<Issue, IssueInDTO>().ReverseMap();
        CreateMap<Issue, IssueOutDTO>().ReverseMap();
        CreateMap<Comment, CommentInDTO>().ReverseMap();
        CreateMap<Comment, CommentOutDTO>().ReverseMap();
    }
}