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
        public async Task<ActionResult<ICollection<EmployeeDto>>> GetEmployeesByDepartmentAsync([FromRoute] string departmentId)
        {
            var employeesByDepartmentIdServiceResponse = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
            return employeesByDepartmentIdServiceResponse.IsSuccess ? Ok(employeesByDepartmentIdServiceResponse.Data) : NotFound(employeesByDepartmentIdServiceResponse.Message);
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByIdAsync([FromRoute] string departmentId, [FromRoute] string employeeId)
        {
            var employeeByIdServiceResponse = await _employeeService.GetEmployeeByIdAsync(departmentId, employeeId);
            return (employeeByIdServiceResponse.IsSuccess) ? Ok(employeeByIdServiceResponse.Data) : NotFound(employeeByIdServiceResponse.Message);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeByIdAsync([FromRoute] string departmentId, [FromRoute] string employeeId, [FromBody] EmployeeDto employeeDto)
        {
            var isEmployeeUpdatedServiceResponse = await _employeeService.UpdateEmployeeAsync(departmentId, employeeId, employeeDto);
            return isEmployeeUpdatedServiceResponse.IsSuccess ? Ok(employeeDto) : NotFound(isEmployeeUpdatedServiceResponse.Message);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateNewEmployeeAsync([FromRoute] string departmentId, [FromBody] EmployeeDto employeeDto)
        {
            var isEmployeeCreatedServiceResponse = await _employeeService.CreateEmployeeAsync(departmentId, employeeDto);
            return isEmployeeCreatedServiceResponse.IsSuccess ? Ok(employeeDto) : Conflict(isEmployeeCreatedServiceResponse.Message);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync([FromRoute] string departmentId, [FromRoute] string employeeId)
        {
            var isEmployeeDeletedServiceResponse = await _employeeService.DeleteEmployeeByIdAsync(departmentId, employeeId);
            return isEmployeeDeletedServiceResponse.IsSuccess ? Ok() : NotFound(isEmployeeDeletedServiceResponse.Message);
        }
        


    }
}
