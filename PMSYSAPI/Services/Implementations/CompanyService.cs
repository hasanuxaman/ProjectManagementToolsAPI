using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.DTOs.Company;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyGroupRepository _companyGroupRepository;
        private readonly IStoredProcedureService _storedProcedureService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CompanyService(
            ICompanyRepository companyRepository,
            ICompanyGroupRepository companyGroupRepository,
            IStoredProcedureService storedProcedureService,
            IMapper mapper,
            AppDbContext context)
        {
            _companyRepository = companyRepository;
            _companyGroupRepository = companyGroupRepository;
            _storedProcedureService = storedProcedureService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<CompanyDto>>> GetAllCompaniesAsync()
        {
            try
            {
                var companies = await (
                    from c in _context.tbComp
                    join g in _context.tbCompGrp on c.CmpGroupCod equals g.GroupCod into grp
                    from g in grp.DefaultIfEmpty()
                    select new CompanyDto
                    {
                        CompCod = c.CompCod,
                        Compname = c.Compname,
                        CompShortname = c.CompShortname,
                        CmpGroupCod = c.CmpGroupCod,
                        GroupName = g != null ? g.GroupName : null
                    }
                ).ToListAsync();

                return new ApiResponse<IEnumerable<CompanyDto>>
                {
                    Success = true,
                    Message = "Companies retrieved successfully",
                    Data = companies
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CompanyDto>>
                {
                    Success = false,
                    Message = "Error retrieving companies",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return new ApiResponse<CompanyDto>
                    {
                        Success = false,
                        Message = $"Company  ID {id} not found"
                    };
                }

                var companyDto = _mapper.Map<CompanyDto>(company);

                return new ApiResponse<CompanyDto>
                {
                    Success = true,
                    Message = "Company retrieved successfully",
                    Data = companyDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanyDto>
                {
                    Success = false,
                    Message = "Error retrieving company",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanyDto>> CreateCompanyAsync(CreateCompanyDto createCompanyDto)
        {
            try
            {
                // Validate company group exists
                if (!await _companyGroupRepository.ExistsAsync(createCompanyDto.CmpGroupCod))
                {
                    return new ApiResponse<CompanyDto>
                    {
                        Success = false,
                        Message = $"Company group  ID {createCompanyDto.CmpGroupCod} does not exist"
                    };
                }

                // Check if company with same name already exists
                var existingCompany = await _companyRepository.GetCompanyByNameAsync(createCompanyDto.Compname);
                if (existingCompany != null)
                {
                    return new ApiResponse<CompanyDto>
                    {
                        Success = false,
                        Message = $"Company  name '{createCompanyDto.Compname}' already exists"
                    };
                }

                var company = _mapper.Map<tbComp>(createCompanyDto);
                var createdCompany = await _companyRepository.CreateCompanyAsync(company);
                var companyDto = _mapper.Map<CompanyDto>(createdCompany);

                return new ApiResponse<CompanyDto>
                {
                    Success = true,
                    Message = "Company created successfully",
                    Data = companyDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanyDto>
                {
                    Success = false,
                    Message = "Error creating company",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanyDto>> CreateCompanyUsingSpAsync(CreateCompanyDto createCompanyDto)
        {
            try
            {
                var result = await _storedProcedureService.AddCompanyUsingSp(
                    createCompanyDto.Compname,
                    createCompanyDto.CompShortname,
                    createCompanyDto.CmpGroupCod);

                if (!result)
                {
                    var company = await _companyRepository.GetCompanyByNameAsync(createCompanyDto.Compname);
                    if (company == null)
                    {
                        return new ApiResponse<CompanyDto>
                        {
                            Success = false,
                            Message = "Company may already exist  this name"
                        };
                    }

                    var companyDto = _mapper.Map<CompanyDto>(company);
                    return new ApiResponse<CompanyDto>
                    {
                        Success = true,
                        Message = "Company created successfully using stored procedure",
                        Data = companyDto
                    };
                }

                var createdCompany = await _companyRepository.GetCompanyByNameAsync(createCompanyDto.Compname);
                if (createdCompany == null)
                {
                    return new ApiResponse<CompanyDto>
                    {
                        Success = false,
                        Message = "Company was created but could not be retrieved"
                    };
                }

                var createdCompanyDto = _mapper.Map<CompanyDto>(createdCompany);
                return new ApiResponse<CompanyDto>
                {
                    Success = true,
                    Message = "Company created successfully using stored procedure",
                    Data = createdCompanyDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanyDto>
                {
                    Success = false,
                    Message = "Error creating company using stored procedure",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanyDto>> UpdateCompanyAsync(int id, UpdateCompanyDto updateCompanyDto)
        {
            try
            {
                var existingCompany = await _companyRepository.GetCompanyByIdAsync(id);
                if (existingCompany == null)
                {
                    return new ApiResponse<CompanyDto>
                    {
                        Success = false,
                        Message = $"Company  ID {id} not found"
                    };
                }

                // Check if another company with the same name exists
                var companyWithSameName = await _companyRepository.GetCompanyByNameAsync(updateCompanyDto.Compname);
                if (companyWithSameName != null && companyWithSameName.CompCod != id)
                {
                    return new ApiResponse<CompanyDto>
                    {
                        Success = false,
                        Message = $"Another company  name '{updateCompanyDto.Compname}' already exists"
                    };
                }

                // Validate company group exists
                if (!await _companyGroupRepository.ExistsAsync(updateCompanyDto.CmpGroupCod))
                {
                    return new ApiResponse<CompanyDto>
                    {
                        Success = false,
                        Message = $"Company group  ID {updateCompanyDto.CmpGroupCod} does not exist"
                    };
                }

                _mapper.Map(updateCompanyDto, existingCompany);
                await _companyRepository.UpdateCompanyAsync(existingCompany);

                var updatedCompanyDto = _mapper.Map<CompanyDto>(existingCompany);

                return new ApiResponse<CompanyDto>
                {
                    Success = true,
                    Message = "Company updated successfully",
                    Data = updatedCompanyDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanyDto>
                {
                    Success = false,
                    Message = "Error updating company",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse> DeleteCompanyAsync(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Company  ID {id} not found"
                    };
                }

                await _companyRepository.DeleteCompanyAsync(id);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Company deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Error deleting company",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<CompanySummaryDto>> GetCompanySummaryAsync()
        {
            try
            {
                var totalCompanies = await _context.tbComp.CountAsync();
                var companiesWithProjects = await _context.tbComp
                   
                    .Select(c => new
                    {
                        c.Compname,
                       
                    })
                 
                    .Take(10)
                    .ToListAsync();

                var summary = new CompanySummaryDto
                {
                    TotalCompanies = totalCompanies,
                    TopCompanies = companiesWithProjects.Select(c => new CompanyProjectSummaryDto
                    {
                        CompanyName = c.Compname,
                    
                    }).ToList()
                };

                return new ApiResponse<CompanySummaryDto>
                {
                    Success = true,
                    Message = "Company summary retrieved successfully",
                    Data = summary
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CompanySummaryDto>
                {
                    Success = false,
                    Message = "Error retrieving company summary",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<CompanyDto>>> SearchCompaniesAsync(string searchTerm)
        {
            var response = new ApiResponse<IEnumerable<CompanyDto>>();

            try
            {
                // Null or empty check
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = ""; // return all companies if no searchTerm
                }

                // Query database (case-insensitive search)
                var companiesQuery = _context.tbComp
                    .Where(c => c.Compname.Contains(searchTerm)
                             || c.CompShortname.Contains(searchTerm));

                var companiesList = await companiesQuery.ToListAsync();

                // Map to DTOs
                var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companiesList);

                // Return success response
                response.Success = true;
                response.Message = companyDtos.Any() ? "Companies found" : "No companies found";
                response.Data = companyDtos;

                return response;
            }
            catch (Exception ex)
            {
                // Return error response
                response.Success = false;
                response.Message = "Error searching companies";
                response.Data = null;
                response.Errors = new List<string> { ex.Message };

                return response;
            }
        }

    }
}