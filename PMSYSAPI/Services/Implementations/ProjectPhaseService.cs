using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Services.Implementations
{
    public class ProjectPhaseService : IProjectPhaseService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectPhaseService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ================== GET ALL ==================
        public async Task<ApiResponse<IEnumerable<ProjectPhaseDto>>> GetAllPhasesAsync()
        {
            try
            {
                var phases = await _context.tbProjPhase.ToListAsync();
                var dtos = _mapper.Map<IEnumerable<ProjectPhaseDto>>(phases);

                return new ApiResponse<IEnumerable<ProjectPhaseDto>>
                {
                    Success = true,
                    Message = "Project phases retrieved successfully",
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ProjectPhaseDto>>
                {
                    Success = false,
                    Message = "Error retrieving project phases",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        // ================== GET BY ID ==================
        public async Task<ApiResponse<ProjectPhaseDto>> GetPhaseByIdAsync(int id)
        {
            try
            {
                var phase = await _context.tbProjPhase.FindAsync(id);
                if (phase == null)
                    return new ApiResponse<ProjectPhaseDto>
                    {
                        Success = false,
                        Message = $"Project phase  ID {id} not found"
                    };

                var dto = _mapper.Map<ProjectPhaseDto>(phase);
                return new ApiResponse<ProjectPhaseDto>
                {
                    Success = true,
                    Message = "Project phase retrieved successfully",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProjectPhaseDto>
                {
                    Success = false,
                    Message = "Error retrieving project phase",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        // ================== CREATE ==================
        public async Task<ApiResponse<ProjectPhaseDto>> CreatePhaseAsync(CreateProjectPhaseDto dto)
        {
            try
            {
                // Check duplicate name
                if (await _context.tbProjPhase.AnyAsync(p => p.PhaseName == dto.PhaseName))
                    return new ApiResponse<ProjectPhaseDto>
                    {
                        Success = false,
                        Message = $"Project phase  name '{dto.PhaseName}' already exists"
                    };

                var phase = _mapper.Map<tbProjPhase>(dto);
                _context.tbProjPhase.Add(phase);
                await _context.SaveChangesAsync();

                var phaseDto = _mapper.Map<ProjectPhaseDto>(phase);
                return new ApiResponse<ProjectPhaseDto>
                {
                    Success = true,
                    Message = "Project phase created successfully",
                    Data = phaseDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProjectPhaseDto>
                {
                    Success = false,
                    Message = "Error creating project phase",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        // ================== UPDATE ==================
        public async Task<ApiResponse<ProjectPhaseDto>> UpdatePhaseAsync(int id, UpdateProjectPhaseDto dto)
        {
            try
            {
                var phase = await _context.tbProjPhase.FindAsync(id);
                if (phase == null)
                    return new ApiResponse<ProjectPhaseDto>
                    {
                        Success = false,
                        Message = $"Project phase  ID {id} not found"
                    };

                // Check duplicate name for other phases
                if (await _context.tbProjPhase.AnyAsync(p => p.PhaseName == dto.PhaseName && p.PhaseCod != id))
                    return new ApiResponse<ProjectPhaseDto>
                    {
                        Success = false,
                        Message = $"Another project phase  name '{dto.PhaseName}' already exists"
                    };

                _mapper.Map(dto, phase);
                await _context.SaveChangesAsync();

                var phaseDto = _mapper.Map<ProjectPhaseDto>(phase);
                return new ApiResponse<ProjectPhaseDto>
                {
                    Success = true,
                    Message = "Project phase updated successfully",
                    Data = phaseDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProjectPhaseDto>
                {
                    Success = false,
                    Message = "Error updating project phase",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        // ================== DELETE ==================
        public async Task<ApiResponse> DeletePhaseAsync(int id)
        {
            try
            {
                var phase = await _context.tbProjPhase.FindAsync(id);
                if (phase == null)
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Project phase  ID {id} not found"
                    };

                _context.tbProjPhase.Remove(phase);
                await _context.SaveChangesAsync();

                return new ApiResponse
                {
                    Success = true,
                    Message = "Project phase deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Error deleting project phase",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.tbProjPhase.AnyAsync(p => p.PhaseCod == id);
        }
    }
}
