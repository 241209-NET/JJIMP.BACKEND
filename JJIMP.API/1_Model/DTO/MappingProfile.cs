using AutoMapper;
using JJIMP.API.Model;

namespace JJIMP.API.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserDTO>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
        CreateMap<User, UserOutDTO>().ReverseMap();
        CreateMap<Project, CreateProjectDTO>().ReverseMap();
        CreateMap<Project, UpdateProjectDTO>().ReverseMap();
        CreateMap<Project, ProjectOutDTO>().ReverseMap();
        CreateMap<Issue, CreateIssueDTO>().ReverseMap();
        CreateMap<Issue, UpdateIssueDTO>().ReverseMap();
        CreateMap<Issue, IssueOutDTO>().ReverseMap();
        CreateMap<Comment, CreateCommentDTO>().ReverseMap();
        CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
        CreateMap<Comment, CommentOutDTO>().ReverseMap();
    }
}