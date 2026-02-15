using AutoMapper;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Services.Implementations
{
    public class ProjectStatusService : IProjectStatusService
    {
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IMapper _mapper;

        public ProjectStatusService(IProjectStatusRepository projectStatusRepository, IMapper mapper)
        {
            _projectStatusRepository = projectStatusRepository;
            _mapper = mapper;
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<IEnumerable<ProjectStatusDto>>> GetAllStatusesAsync()
        {
            try
            {
                var statuses = await _projectStatusRepository.GetAllAsync();
                var statusDtos = _mapper.Map<IEnumerable<ProjectStatusDto>>(statuses);

                return new ApiResponse<IEnumerable<ProjectStatusDto>>
                {
                    Success = true,
                    Message = "Project statuses retrieved successfully",
                    Data = statusDtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ProjectStatusDto>>
                {
                    Success = false,
                    Message = "Error retrieving project statuses",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<ProjectStatusDto>> GetStatusByIdAsync(int id)
        {
            try
            {
                var status = await _projectStatusRepository.GetByIdAsync(id);
                if (status == null)
                {
                    return new ApiResponse<ProjectStatusDto>
                    {
                        Success = false,
                        Message = $"Project status with ID {id} not found"
                    };
                }

                var statusDto = _mapper.Map<ProjectStatusDto>(status);

                return new ApiResponse<ProjectStatusDto>
                {
                    Success = true,
                    Message = "Project status retrieved successfully",
                    Data = statusDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProjectStatusDto>
                {
                    Success = false,
                    Message = "Error retrieving project status",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        Task<ApiResponse<IEnumerable<ProjectStatusDto>>> IProjectStatusService.GetAllStatusesAsync()
        {
            throw new NotImplementedException();
        }

        Task<ApiResponse<ProjectStatusDto>> IProjectStatusService.GetStatusByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}