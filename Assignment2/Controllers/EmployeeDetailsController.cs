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
            var employees = await _employeeService.GetEmployeesAsync();
            if (employees == null) return NotFound();
            return employees.Count() > 0 ? Ok(employees) : NotFound();
        }
    }
}
