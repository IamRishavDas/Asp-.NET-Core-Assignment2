using Assignment2.Models;

namespace Assignment2.Repositories
{
    public interface IEmployeeRepositoryInterface
    {
        Task<ICollection<Employee>> GetEmployeesAsync();
        Task<ICollection<Employee>> GetEmployeesByDepartmentIdAsync(string departmentId);
        Task<Employee> GetEmployeeByIdAsync(string departmentId, string employeeId);
        Task<bool> CreateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeByIdAsync(string departmentId, string employeeId);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> IsEmployeeExistAsync(string employeeId);
    }
}
