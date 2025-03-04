using Assignment2.Dtos;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServiceInterface _departmentService;

        public DepartmentController(IDepartmentServiceInterface departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("departments")]
        public async Task<ActionResult<ICollection<DepartmentDto>>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            if (departments == null) return NotFound();
            return departments?.Count() > 0 ? Ok(departments) : NotFound();
        }

        [HttpGet("departments/{departmentId}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentByIdAsync([FromRoute] string departmentId)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(departmentId);
            if (department == null || department.DepartmentId == null) return NotFound();
            return Ok(department);
        }

        [HttpPost("departments")]
        public async Task<ActionResult<DepartmentDto>> CreateDepartmentAsync([FromBody] DepartmentDto departmentDto)
        {
            var isDepartmentCreated = await _departmentService.CreateDepartmentAsync(departmentDto);
            return isDepartmentCreated ? Ok(departmentDto) : Conflict();
        }

        [HttpPatch("departments/{departmentId}")]
        public async Task<ActionResult<DepartmentDto>> UpdateDepartmentAsync([FromRoute] string departmentId, [FromBody] DepartmentDto departmentDto)
        {
            var isDepartmentUpdated = await _departmentService.UpdateDepartmentAsync(departmentId, departmentDto);
            return isDepartmentUpdated ? Ok(departmentDto) : NotFound();
        }

        [HttpDelete("departments/{departmentId}")]
        public async Task<IActionResult> DeleteDepartmentById([FromRoute] string departmentId)
        {
            var isDepartmentDeleted = await _departmentService.DeleteDepartmentByIdAsync(departmentId);
            return isDepartmentDeleted ? Ok() : NotFound();
        }
    }
}
