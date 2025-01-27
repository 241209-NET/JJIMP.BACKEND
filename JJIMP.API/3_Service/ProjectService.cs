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

    public async Task<IEnumerable<ProjectOutDTO>> GetAllProjects()
    {
        var projects = await _projectRepository.GetAllProjects();
        return _mapper.Map<IEnumerable<ProjectOutDTO>>(projects);
    }

    public async Task<ProjectOutDTO> GetProjectById(int projectId)
    {
        var project = await _projectRepository.GetProjectById(projectId) ?? throw new ArgumentException("Project not found");
        return _mapper.Map<ProjectOutDTO>(project);
    }

    public async Task<ProjectOutDTO> CreateProject(ProjectInDTO projectDTO)
    {
        var project = _mapper.Map<Project>(projectDTO);
        var createdProject = await _projectRepository.CreateProject(project);
        return _mapper.Map<ProjectOutDTO>(createdProject);
    }

    public async Task<ProjectOutDTO> UpdateProject(ProjectInDTO projectDTO)
    {
        if (!projectDTO.Id.HasValue)
        {
            throw new ArgumentException("Project ID is required");
        }
        var projectToUpdate = await _projectRepository.GetProjectById(projectDTO.Id.Value) ?? throw new ArgumentException("Project not found");
        _mapper.Map(projectDTO, projectToUpdate);
        var updatedProject = await _projectRepository.UpdateProject(projectToUpdate);
        return _mapper.Map<ProjectOutDTO>(updatedProject);
    }

    public async Task<ProjectOutDTO> DeleteProject(int projectId)
    {
        var deletedProject = await _projectRepository.DeleteProject(projectId);
        return _mapper.Map<ProjectOutDTO>(deletedProject);
    }
}
