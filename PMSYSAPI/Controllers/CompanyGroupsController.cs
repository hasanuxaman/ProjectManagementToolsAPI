using Microsoft.AspNetCore.Mvc;
using PMSYSAPI.DTOs.Company;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyGroupsController : ControllerBase
    {
        private readonly ICompanyGroupService _companyGroupService;

        public CompanyGroupsController(ICompanyGroupService companyGroupService)
        {
            _companyGroupService = companyGroupService;
        }

        // ================== GET ALL ==================
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyGroupDto>>>> GetCompanyGroups()
        {
            var response = await _companyGroupService.GetAllGroupsAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // ================== GET BY ID ==================
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CompanyGroupDto>>> GetCompanyGroup(int id)
        {
            var response = await _companyGroupService.GetGroupByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // ================== CREATE ==================
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CompanyGroupDto>>> CreateCompanyGroup([FromBody] CreateCompanyGroupDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<CompanyGroupDto> { Success = false, Message = "Invalid data" });

            var response = await _companyGroupService.CreateGroupAsync(dto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetCompanyGroup), new { id = response.Data!.GroupCod }, response);
        }

        // ================== UPDATE ==================
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CompanyGroupDto>>> UpdateCompanyGroup(int id, [FromBody] UpdateCompanyGroupDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<CompanyGroupDto> { Success = false, Message = "Invalid data" });

            var response = await _companyGroupService.UpdateGroupAsync(id, dto);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // ================== DELETE ==================
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteCompanyGroup(int id)
        {
            var response = await _companyGroupService.DeleteGroupAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}
