using JJIMP.API.DTO;
using JJIMP.API.Model;

namespace JJIMP.API.Service;

public interface IProjectService
{
    Task<IEnumerable<ProjectOutDTO>> GetAllProjects();
    Task<ProjectOutDTO> GetProjectById(int projectId);
    Task<ProjectOutDTO> CreateProject(ProjectInDTO projectDto);
    Task<ProjectOutDTO> UpdateProject(ProjectInDTO projectDto);
    Task<ProjectOutDTO> DeleteProject(int projectId);
}