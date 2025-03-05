using Assignment2.Data;
using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Repositories
{
    public class EmployeeRepository : IEmployeeRepositoryInterface
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            if(!await IsEmployeeExistAsync(employee.EmployeeId))
            {
                await _context.Employees.AddAsync(employee);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public async Task<bool> DeleteEmployeeByIdAsync(string departmentId, string employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return false;
            if (!employee.DepartmentId.Equals(departmentId)) return false;
            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<Employee> GetEmployeeByIdAsync(string departmentId, string employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return new Employee();
            return employee.DepartmentId.Equals(departmentId) ? employee : new Employee();
        }

        public async Task<ICollection<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<ICollection<Employee>> GetEmployeesByDepartmentIdAsync(string departmentId)
        {
            var employeesByDepartmentId = await _context.Employees.Where(e => e.DepartmentId.Equals(departmentId)).ToListAsync();
            return employeesByDepartmentId;
        }

        public async Task<bool> IsEmployeeExistAsync(string employeeId)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId.Equals(employeeId));
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var emp = await _context.Employees.FindAsync(employee.EmployeeId);
            if (emp == null) return false;

            emp.EmployeeId = employee.EmployeeId;
            emp.EmployeeName = employee.EmployeeName;
            emp.EmployeeAge = employee.EmployeeAge;
            emp.Salary = employee.Salary;
            emp.DepartmentId = employee.DepartmentId;
            emp.Department = employee.Department;

            _context.Employees.Update(emp);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
