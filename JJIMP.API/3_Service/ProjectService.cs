using AutoMapper;
using JJIMP.API.DTO;
using JJIMP.API.Model;
using JJIMP.API.Repository;

namespace JJIMP.API.Service;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectOutDTO> GetProjectById(int projectId)
    {
        var project = await _projectRepository.GetProjectById(projectId) ?? throw new ArgumentException("Project not found");
        return _mapper.Map<ProjectOutDTO>(project);
    }

    public async Task<ProjectOutDTO> CreateProject(CreateProjectDTO projectDTO)
    {
        var project = _mapper.Map<Project>(projectDTO);
        var createdProject = await _projectRepository.CreateProject(project);
        return _mapper.Map<ProjectOutDTO>(createdProject);
    }

    public async Task<ProjectOutDTO> UpdateProject(UpdateProjectDTO projectDTO)
    {
        var projectToUpdate = await _projectRepository.GetProjectById(projectDTO.Id) ?? throw new ArgumentException("Project not found");
        _mapper.Map(projectDTO, projectToUpdate);
        // Assign the new values to the projectToUpdate
        if (projectDTO.Name != null)
        {
            projectToUpdate.Name = projectDTO.Name;
        }
        if (projectDTO.Description != null)
        {
            projectToUpdate.Description = projectDTO.Description;
        }
        if (projectDTO.ProjectManagerId != null)
        {
            projectToUpdate.ProjectManagerId = (int)projectDTO.ProjectManagerId;
        }
        var updatedProject = await _projectRepository.UpdateProject(projectToUpdate);
        return _mapper.Map<ProjectOutDTO>(updatedProject);
    }

    public async Task<ProjectOutDTO> DeleteProject(int projectId)
    {
        var deletedProject = await _projectRepository.DeleteProject(projectId);
        return _mapper.Map<ProjectOutDTO>(deletedProject);
    }

    public async Task<ProjectOutDTO> AddUserToProject(int projectId, int userId)
    {
        var project = await _projectRepository.AddUserToProject(projectId, userId);
        return _mapper.Map<ProjectOutDTO>(project);
    }

    public async Task<ProjectOutDTO> RemoveUserFromProject(int projectId, int userId)
    {
        var project = await _projectRepository.RemoveUserFromProject(projectId, userId);
        return _mapper.Map<ProjectOutDTO>(project);
    }
}
