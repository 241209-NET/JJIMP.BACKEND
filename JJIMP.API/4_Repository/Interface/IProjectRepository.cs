using JJIMP.API.Model;

namespace JJIMP.API.Repository;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllProjects();
    Task<Project?> GetProjectById(int projectId);
    Task<Project> CreateProject(Project project);
    Task<Project> UpdateProject(Project project);
    Task<Project?> DeleteProject(int projectId);
}