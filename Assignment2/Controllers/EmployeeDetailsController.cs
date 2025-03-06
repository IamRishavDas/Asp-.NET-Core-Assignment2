using Assignment2.Dtos;
using Assignment2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeDetailsController : ControllerBase
    {

        private readonly IEmployeeServiceInterface _employeeService;

        public EmployeeDetailsController(IEmployeeServiceInterface employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<EmployeeDto>>> GetEmployeesAsync()
        {
            if (!ModelState.IsValid) return BadRequest();
            var employeesServiceResponse = await _employeeService.GetEmployeesAsync();
            return employeesServiceResponse.IsSuccess ? Ok(employeesServiceResponse.Data) : NotFound(employeesServiceResponse.Message);
        }
    }
}
