using Assignment2.CustomResponses;
using Assignment2.Dtos;

namespace Assignment2.Services
{
    public interface IEmployeeServiceInterface
    {
        Task<ServiceResponse<ICollection<EmployeeDto>>> GetEmployeesAsync();
        Task<ServiceResponse<ICollection<EmployeeDto>>> GetEmployeesByDepartmentIdAsync(string departmentId);
        Task<ServiceResponse<EmployeeDto>> GetEmployeeByIdAsync(string departmentId, string employeeId);
        Task<ServiceResponse<bool>> CreateEmployeeAsync(string departmentId, EmployeeDto employeeDto);
        Task<ServiceResponse<bool>> DeleteEmployeeByIdAsync(string departmentId, string employeeId);
        Task<ServiceResponse<bool>> UpdateEmployeeAsync(string departmentId, string employeeId, EmployeeDto employeeDto);
        Task<ServiceResponse<bool>> IsEmployeeExistAsync(string employeeId);
        }
}
