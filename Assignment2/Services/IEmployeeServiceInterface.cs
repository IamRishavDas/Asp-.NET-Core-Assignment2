using Assignment2.Dtos;

namespace Assignment2.Services
{
    public interface IEmployeeServiceInterface
    {
        Task<ICollection<EmployeeDto>> GetEmployeesAsync();
        Task<ICollection<EmployeeDto>> GetEmployeesByDepartmentIdAsync(string departmentId);
        Task<EmployeeDto> GetEmployeeByIdAsync(string departmentId, string employeeId);
        Task<bool> CreateEmployeeAsync(string departmentId, EmployeeDto employeeDto);
        Task<bool> DeleteEmployeeByIdAsync(string departmentId, string employeeId);
        Task<bool> UpdateEmployeeAsync(string departmentId, string employeeId, EmployeeDto employeeDto);
        Task<bool> IsEmployeeExistAsync(string employeeId);
    }
}
