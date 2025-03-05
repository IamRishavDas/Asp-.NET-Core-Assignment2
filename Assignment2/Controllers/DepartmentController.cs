using Assignment2.Dtos;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServiceInterface _departmentService;

        public DepartmentController(IDepartmentServiceInterface departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<DepartmentDto>>> GetAllDepartmentsAsync()
        {
            var departmentsServiceResponse = await _departmentService.GetDepartmentsAsync();
            return departmentsServiceResponse.IsSuccess ? Ok(departmentsServiceResponse.Data) : NotFound(departmentsServiceResponse.Message);
        }   

        [HttpGet("{departmentId}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentByIdAsync([FromRoute] string departmentId)
        {
            var departmentServiceResponse = await _departmentService.GetDepartmentByIdAsync(departmentId);
            return departmentServiceResponse.IsSuccess ? Ok(departmentServiceResponse.Data) : NotFound(departmentServiceResponse.Message);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> CreateDepartmentAsync([FromBody] DepartmentDto departmentDto)
        {
            var isDepartmentCreatedServiceResponse = await _departmentService.CreateDepartmentAsync(departmentDto);
            return isDepartmentCreatedServiceResponse.IsSuccess ? Ok(departmentDto) : Conflict(isDepartmentCreatedServiceResponse.Message);
        }

        [HttpPatch("{departmentId}")]
        public async Task<ActionResult<DepartmentDto>> UpdateDepartmentAsync([FromRoute] string departmentId, [FromBody] DepartmentDto departmentDto)
        {
            var isDepartmentUpdatedServiceResponse = await _departmentService.UpdateDepartmentAsync(departmentId, departmentDto);
            return isDepartmentUpdatedServiceResponse.IsSuccess ? Ok(departmentDto) : NotFound(isDepartmentUpdatedServiceResponse.Message);
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> DeleteDepartmentById([FromRoute] string departmentId)
        {
            var isDepartmentDeletedServiceResponse = await _departmentService.DeleteDepartmentByIdAsync(departmentId);
            return isDepartmentDeletedServiceResponse.IsSuccess ? Ok() : NotFound(isDepartmentDeletedServiceResponse.Message);
        }
    }
}
