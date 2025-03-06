using Assignment2.Dtos;
using Assignment2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/departments/{departmentId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServiceInterface _employeeService;

        public EmployeeController(IEmployeeServiceInterface employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<EmployeeDto>>> GetEmployeesByDepartment([FromRoute] string departmentId)
        {
            var employeesByDepartmentId = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
            if (employeesByDepartmentId == null) return NotFound();
            return employeesByDepartmentId.Count() > 0 ? Ok(employeesByDepartmentId) : NotFound();
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByIdAsync([FromRoute] string departmentId, [FromRoute] string employeeId)
        {
            var employeeById = await _employeeService.GetEmployeeByIdAsync(departmentId, employeeId);
            return (employeeById == null || employeeById.EmployeeId == null) ? NotFound() : Ok(employeeById);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeByIdAsync([FromRoute] string departmentId, [FromRoute] string employeeId, [FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var isEmployeeUpdated = await _employeeService.UpdateEmployeeAsync(departmentId, employeeId, employeeDto);
            return isEmployeeUpdated ? Ok(employeeDto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateNewEmployeeAsync([FromRoute] string departmentId, [FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var isEmployeeCreated = await _employeeService.CreateEmployeeAsync(departmentId, employeeDto);
            return isEmployeeCreated ? Ok(employeeDto) : Conflict();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync([FromRoute] string departmentId, [FromRoute] string employeeId)
        {
            var isEmployeeDeleted = await _employeeService.DeleteEmployeeByIdAsync(departmentId, employeeId);
            return isEmployeeDeleted ? Ok() : NotFound();
        }
        


    }
}
