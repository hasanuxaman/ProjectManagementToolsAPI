using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.DTOs.Company;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PMSYSAPI.Services.Implementations
{
    public class CompanyGroupService : ICompanyGroupService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _connectionString;

        

        public CompanyGroupService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ================== CRUD ==================

        public async Task<ApiResponse<IEnumerable<CompanyGroupDto>>> GetAllGroupsAsync()
        {
            try
            {
                var groups = await _context.tbCompGrp.ToListAsync();
                var dtos = _mapper.Map<IEnumerable<CompanyGroupDto>>(groups);
                return new ApiResponse<IEnumerable<CompanyGroupDto>>
                {
                    Success = true,
                    Message = "Company groups retrieved successfully",
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CompanyGroupDto>>
                {
                    Success = false,
                    Message = "Error retrieving company groups",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanyGroupDto>> GetGroupByIdAsync(int id)
        {
            try
            {
                var group = await _context.tbCompGrp.FindAsync(id);
                if (group == null)
                    return new ApiResponse<CompanyGroupDto>
                    {
                        Success = false,
                        Message = $"Company group with ID {id} not found"
                    };

                var dto = _mapper.Map<CompanyGroupDto>(group);
                return new ApiResponse<CompanyGroupDto>
                {
                    Success = true,
                    Message = "Company group retrieved successfully",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanyGroupDto>
                {
                    Success = false,
                    Message = "Error retrieving company group",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanyGroupDto>> CreateGroupAsync(CreateCompanyGroupDto dto)
        {
            try
            {
                // Check duplicate
                if (await _context.tbCompGrp.AnyAsync(g => g.GroupName == dto.GroupName))
                    return new ApiResponse<CompanyGroupDto>
                    {
                        Success = false,
                        Message = $"Company group with name '{dto.GroupName}' already exists"
                    };

                var group = _mapper.Map<tbCompGrp>(dto);
                //group.da = DateTime.UtcNow;

                _context.tbCompGrp.Add(group);
                await _context.SaveChangesAsync();

                var groupDto = _mapper.Map<CompanyGroupDto>(group);
                return new ApiResponse<CompanyGroupDto>
                {
                    Success = true,
                    Message = "Company group created successfully",
                    Data = groupDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanyGroupDto>
                {
                    Success = false,
                    Message = "Error creating company group",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanyGroupDto>> UpdateGroupAsync(int id, UpdateCompanyGroupDto dto)
        {
            try
            {
                var group = await _context.tbCompGrp.FindAsync(id);
                if (group == null)
                    return new ApiResponse<CompanyGroupDto>
                    {
                        Success = false,
                        Message = $"Company group with ID {id} not found"
                    };

                // Map updated fields
                _mapper.Map(dto, group);

                await _context.SaveChangesAsync();

                var groupDto = _mapper.Map<CompanyGroupDto>(group);
                return new ApiResponse<CompanyGroupDto>
                {
                    Success = true,
                    Message = "Company group updated successfully",
                    Data = groupDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanyGroupDto>
                {
                    Success = false,
                    Message = "Error updating company group",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse> DeleteGroupAsync(int id)
        {
            try
            {
                var group = await _context.tbCompGrp.FindAsync(id);
                if (group == null)
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Company group with ID {id} not found"
                    };

                _context.tbCompGrp.Remove(group);
                await _context.SaveChangesAsync();

                return new ApiResponse
                {
                    Success = true,
                    Message = "Company group deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Error deleting company group",
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        public async Task<bool> AddCompanyGroupUsingSp(CreateCompanyGroupDto dtos)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("CompGrp_add", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GroupName", dtos.GroupName);
                    command.Parameters.AddWithValue("@Group_Shortname", dtos.GroupShortname);
                   

                    await connection.OpenAsync();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.tbCompGrp.AnyAsync(g => g.GroupCod == id);
        }
    }
}
