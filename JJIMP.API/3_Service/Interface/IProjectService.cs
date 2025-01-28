using JJIMP.API.DTO;
using JJIMP.API.Model;

namespace JJIMP.API.Service;

public interface IProjectService
{
    Task<ProjectOutDTO> GetProjectById(int projectId);
    Task<ProjectOutDTO> CreateProject(CreateProjectDTO projectDto);
    Task<ProjectOutDTO> UpdateProject(UpdateProjectDTO projectDto);
    Task<ProjectOutDTO> DeleteProject(int projectId);
    Task<ProjectOutDTO> AddUserToProject(int projectId, int userId);
    Task<ProjectOutDTO> RemoveUserFromProject(int projectId, int userId);
}