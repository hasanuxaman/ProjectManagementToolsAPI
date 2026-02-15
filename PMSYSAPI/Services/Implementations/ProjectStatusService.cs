using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Services.Interfaces;

public class ProjectStatusService : IProjectStatusService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProjectStatusService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ProjectStatusDto>>> GetAllStatusesAsync()
    {
        var statuses = await _context.tbStatus.ToListAsync();
        var dtos = _mapper.Map<IEnumerable<ProjectStatusDto>>(statuses);
        return new ApiResponse<IEnumerable<ProjectStatusDto>>
        {
            Success = true,
            Message = "Project statuses retrieved successfully",
            Data = dtos
        };
    }

    public async Task<ApiResponse<ProjectStatusDto>> GetStatusByIdAsync(int id)
    {
        var status = await _context.tbStatus.FindAsync(id);
        if (status == null)
            return ApiResponse<ProjectStatusDto>.Fail($"Project status with ID {id} not found");

        var dto = _mapper.Map<ProjectStatusDto>(status);
        return ApiResponse<ProjectStatusDto>.Ok("Project status retrieved successfully", dto);
    }

    public async Task<ApiResponse<ProjectStatusDto>> CreateStatusAsync(CreateProjectStatusDto dto)
    {
        var status = _mapper.Map<tbStatus>(dto);
        _context.tbStatus.Add(status);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ProjectStatusDto>(status);
        return ApiResponse<ProjectStatusDto>.Ok("Project status created successfully", resultDto);
    }

    public async Task<ApiResponse<ProjectStatusDto>> UpdateStatusAsync(int id, UpdateProjectStatusDto dto)
    {
        var status = await _context.tbStatus.FindAsync(id);
        if (status == null)
            return ApiResponse<ProjectStatusDto>.Fail($"Project status with ID {id} not found");

        _mapper.Map(dto, status);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ProjectStatusDto>(status);
        return ApiResponse<ProjectStatusDto>.Ok("Project status updated successfully", resultDto);
    }

    public async Task<ApiResponse> DeleteStatusAsync(int id)
    {
        var status = await _context.tbStatus.FindAsync(id);
        if (status == null)
            return ApiResponse.Fail($"Project status with ID {id} not found");

        _context.tbStatus.Remove(status);
        await _context.SaveChangesAsync();
        return ApiResponse.Ok("Project status deleted successfully");
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }
}
