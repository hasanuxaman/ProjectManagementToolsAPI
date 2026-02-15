using Microsoft.AspNetCore.Mvc;
using PMSYSAPI.DTOs;
using PMSYSAPI.DTOs.Common;
using PMSYSAPI.DTOs.Company;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyDto>>>> GetCompanies()
        {
            var response = await _companyService.GetAllCompaniesAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CompanyDto>>> GetCompany(int id)
        {
            var response = await _companyService.GetCompanyByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CompanyDto>>> CreateCompany(CreateCompanyDto createCompanyDto)
        {
            var response = await _companyService.CreateCompanyAsync(createCompanyDto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetCompany), new { id = response.Data?.CompCod }, response);
        }

        [HttpPost("create-via-sp")]
        public async Task<ActionResult<ApiResponse<CompanyDto>>> CreateCompanyViaSp(CreateCompanyDto createCompanyDto)
        {
            var response = await _companyService.CreateCompanyUsingSpAsync(createCompanyDto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetCompany), new { id = response.Data?.CompCod }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CompanyDto>>> UpdateCompany(int id, UpdateCompanyDto updateCompanyDto)
        {
            var response = await _companyService.UpdateCompanyAsync(id, updateCompanyDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteCompany(int id)
        {
            var response = await _companyService.DeleteCompanyAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyDto>>>> SearchCompanies([FromQuery] string term)
        {
            var response = await _companyService.SearchCompaniesAsync(term);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<ApiResponse<CompanySummaryDto>>> GetCompanySummary()
        {
            var response = await _companyService.GetCompanySummaryAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}