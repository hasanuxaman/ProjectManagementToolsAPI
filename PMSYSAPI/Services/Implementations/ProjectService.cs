using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.DTOs.Company;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IStoredProcedureService _storedProcedureService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProjectService(
            IProjectRepository projectRepository,
            ICompanyRepository companyRepository,
            IProjectPhaseRepository projectPhaseRepository,
            IProjectStatusRepository projectStatusRepository,
            IStoredProcedureService storedProcedureService,
            IMapper mapper,
            AppDbContext context)
        {
            _projectRepository = projectRepository;
            _companyRepository = companyRepository;
            _projectPhaseRepository = projectPhaseRepository;
            _projectStatusRepository = projectStatusRepository;
            _storedProcedureService = storedProcedureService;
            _mapper = mapper;
            _context = context;
        }

        // ================== CRUD METHODS ==================

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetAllProjectsAsync()
        {
            try
            {
                var projects = await _projectRepository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return ApiResponse<IEnumerable<ProjectDto>>.Ok("Projects retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.Error("Error retrieving projects", ex);
            }
        }

        public async Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(int id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);
                if (project == null)
                    return ApiResponse<ProjectDto>.Fail($"Project with ID {id} not found");

                var dto = _mapper.Map<ProjectDto>(project);
                return ApiResponse<ProjectDto>.Ok("Project retrieved successfully", dto);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.Error("Error retrieving project", ex);
            }
        }

        public async Task<ApiResponse<ProjectDto>> CreateProjectAsync(CreateProjectDto dto)
        {
            try
            {
                // Validate foreign keys
                if (!await _companyRepository.ExistsCompanyAsync(dto.ProjCompCod))
                    return ApiResponse<ProjectDto>.Fail($"Company with ID {dto.ProjCompCod} does not exist");

                if (!await _projectPhaseRepository.ExistsAsync(dto.ProjPhaseCod))
                    return ApiResponse<ProjectDto>.Fail($"Project phase with ID {dto.ProjPhaseCod} does not exist");

                if (!await _projectStatusRepository.ExistsAsync(dto.ProjStsCod))
                    return ApiResponse<ProjectDto>.Fail($"Project status with ID {dto.ProjStsCod} does not exist");

                // Check duplicate name
                if (await _projectRepository.GetByNameAsync(dto.ProjName) != null)
                    return ApiResponse<ProjectDto>.Fail($"Project with name '{dto.ProjName}' already exists");

                // Validate dates & amount
                if (dto.ProjStrtPlndt > dto.ProjEndPlandt)
                    return ApiResponse<ProjectDto>.Fail("Start date cannot be after end date");

                if (dto.ProjEstAmount <= 0)
                    return ApiResponse<ProjectDto>.Fail("Estimated amount must be greater than zero");

                var project = _mapper.Map<tbProjList>(dto);
                var created = await _projectRepository.CreateAsync(project);
                var createdDto = _mapper.Map<ProjectDto>(created);

                return ApiResponse<ProjectDto>.Ok("Project created successfully", createdDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.Error("Error creating project", ex);
            }
        }

        public async Task<ApiResponse<ProjectDto>> UpdateProjectAsync(int id, UpdateProjectDto dto)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);
                if (project == null)
                    return ApiResponse<ProjectDto>.Fail($"Project with ID {id} not found");

                var duplicate = await _projectRepository.GetByNameAsync(dto.ProjName);
                if (duplicate != null && duplicate.ProjCod != id)
                    return ApiResponse<ProjectDto>.Fail($"Another project with name '{dto.ProjName}' already exists");

                // Validate foreign keys & dates
                if (!await _companyRepository.ExistsCompanyAsync(dto.ProjCompCod))
                    return ApiResponse<ProjectDto>.Fail($"Company with ID {dto.ProjCompCod} does not exist");

                if (!await _projectPhaseRepository.ExistsAsync(dto.ProjPhaseCod))
                    return ApiResponse<ProjectDto>.Fail($"Project phase with ID {dto.ProjPhaseCod} does not exist");

                if (!await _projectStatusRepository.ExistsAsync(dto.ProjStsCod))
                    return ApiResponse<ProjectDto>.Fail($"Project status with ID {dto.ProjStsCod} does not exist");

                if (dto.ProjStrtPlndt > dto.ProjEndPlandt)
                    return ApiResponse<ProjectDto>.Fail("Start date cannot be after end date");

                if (dto.ProjEstAmount <= 0)
                    return ApiResponse<ProjectDto>.Fail("Estimated amount must be greater than zero");

                _mapper.Map(dto, project);
                await _projectRepository.UpdateAsync(project);
                var updatedDto = _mapper.Map<ProjectDto>(project);

                return ApiResponse<ProjectDto>.Ok("Project updated successfully", updatedDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.Error("Error updating project", ex);
            }
        }

        public async Task<ApiResponse> DeleteProjectAsync(int id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);
                if (project == null)
                    return ApiResponse.Fail($"Project with ID {id} not found");

                await _projectRepository.DeleteAsync(id);
                return ApiResponse.Ok("Project deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.Error("Error deleting project", ex);
            }
        }

        // ================== FILTER & SEARCH ==================

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByCompanyAsync(int companyId)
        {
            try
            {
                var projects = await _context.tbProjList
                    .Where(p => p.ProjCompCod == companyId)
                    .ToListAsync();

                var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return ApiResponse<IEnumerable<ProjectDto>>.Ok("Projects retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.Error("Error retrieving projects by company", ex);
            }
        }

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByStatusAsync(int statusId)
        {
            try
            {
                var projects = await _context.tbProjList
                    .Where(p => p.ProjStsCod == statusId)
                    .ToListAsync();

                var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return ApiResponse<IEnumerable<ProjectDto>>.Ok("Projects retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.Error("Error retrieving projects by status", ex);
            }
        }

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByPhaseAsync(int phaseId)
        {
            try
            {
                var projects = await _context.tbProjList
                    .Where(p => p.ProjPhaseCod == phaseId)
                    .ToListAsync();

                var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return ApiResponse<IEnumerable<ProjectDto>>.Ok("Projects retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.Error("Error retrieving projects by phase", ex);
            }
        }

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> SearchProjectsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllProjectsAsync();

                var projects = await _context.tbProjList
                    .Where(p => p.ProjName.Contains(searchTerm) ||
                                p.ProjShortname.Contains(searchTerm) ||
                                p.ProjDesc.Contains(searchTerm))
                    .ToListAsync();

                var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return ApiResponse<IEnumerable<ProjectDto>>.Ok($"Projects matching '{searchTerm}' retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.Error("Error searching projects", ex);
            }
        }

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var projects = await _context.tbProjList
                    .Where(p => p.ProjInitDate >= startDate && p.ProjEndPlandt <= endDate)
                    .ToListAsync();

                var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return ApiResponse<IEnumerable<ProjectDto>>.Ok($"Projects from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd} retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.Error("Error retrieving projects by date range", ex);
            }
        }

        // ================== SUMMARY ==================

        public async Task<ApiResponse<ProjectSummaryDto>> GetProjectSummaryAsync()
        {
            try
            {
                var projects = _context.tbProjList.AsQueryable();

                var totalProjects = await projects.CountAsync();
                var totalAmount = await projects.SumAsync(p => p.ProjEstAmount);
                var averageAmount = totalProjects > 0 ? totalAmount / totalProjects : 0;

                var statusSummary = await projects
                    .GroupBy(p => p.ProjStsCod)
                    .Select(g => new ProjectStatusSummaryDto
                    {
                        StatusCod = g.Key,
                        Count = g.Count(),
                        TotalAmount = g.Sum(p => p.ProjEstAmount)
                    })
                    .ToListAsync();

                var phaseSummary = await projects
                    .GroupBy(p => p.ProjPhaseCod)
                    .Select(g => new ProjectPhaseSummaryDto
                    {
                       
                        Count = g.Count(),
                        TotalAmount = g.Sum(p => p.ProjEstAmount)
                    })
                    .ToListAsync();

                var companySummary = await projects
                    .GroupBy(p => p.ProjCompCod)
                    .Select(g => new CompanyProjectSummaryDto
                    {
                        CompanyName = g.Key.ToString(),
                        ProjectCount = g.Count(),
                        TotalAmount = (decimal)g.Sum(p => p.ProjEstAmount)
                    })
                    .OrderByDescending(c => c.ProjectCount)
                    .Take(10)
                    .ToListAsync();

                var summary = new ProjectSummaryDto
                {
                    TotalProjects = totalProjects,
                    TotalEstimatedAmount = totalAmount,
                    AverageProjectAmount = averageAmount,
                    StatusSummary = statusSummary,
                    PhaseSummary = phaseSummary,
                    //CompanySummary = companySummary
                };

                return ApiResponse<ProjectSummaryDto>.Ok("Project summary retrieved successfully", summary);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectSummaryDto>.Error("Error retrieving project summary", ex);
            }
        }

        // ================== UPDATE STATUS & PHASE ==================

        public async Task<ApiResponse<ProjectDto>> UpdateProjectStatusAsync(int id, int statusId)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);
                if (project == null)
                    return ApiResponse<ProjectDto>.Fail($"Project with ID {id} not found");

                if (!await _projectStatusRepository.ExistsAsync(statusId))
                    return ApiResponse<ProjectDto>.Fail($"Status with ID {statusId} does not exist");

                project.ProjStsCod = statusId;
                await _projectRepository.UpdateAsync(project);

                var dto = _mapper.Map<ProjectDto>(project);
                return ApiResponse<ProjectDto>.Ok("Project status updated successfully", dto);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.Error("Error updating project status", ex);
            }
        }

        public async Task<ApiResponse<ProjectDto>> UpdateProjectPhaseAsync(int id, int phaseId)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);
                if (project == null)
                    return ApiResponse<ProjectDto>.Fail($"Project with ID {id} not found");

                if (!await _projectPhaseRepository.ExistsAsync(phaseId))
                    return ApiResponse<ProjectDto>.Fail($"Phase with ID {phaseId} does not exist");

                project.ProjStsCod = phaseId;
                await _projectRepository.UpdateAsync(project);

                var dto = _mapper.Map<ProjectDto>(project);
                return ApiResponse<ProjectDto>.Ok("Project phase updated successfully", dto);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.Error("Error updating project phase", ex);
            }
        }

        // ================== STORED PROCEDURE ==================

        public async Task<ApiResponse<ProjectDto>> CreateProjectUsingSpAsync(CreateProjectDto dto)
        {
            try
            {
                var result = await _storedProcedureService.AddProjectUsingSp(
                    dto.ProjName, dto.ProjShortname, dto.ProjDesc, dto.ProjCompCod,
                    dto.ProjPhaseCod, dto.ProjStsCod, dto.ProjInitDate,
                    dto.ProjStrtPlndt, dto.ProjEndPlandt, dto.ProjEstAmount
                );

                var project = await _projectRepository.GetByNameAsync(dto.ProjName);
                if (project == null)
                    return ApiResponse<ProjectDto>.Fail("Project may already exist or could not be retrieved");

                var projectDto = _mapper.Map<ProjectDto>(project);
                return ApiResponse<ProjectDto>.Ok("Project created successfully using stored procedure", projectDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.Error("Error creating project using stored procedure", ex);
            }
        }
    }
}
