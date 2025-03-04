using Assignment2.Models;

namespace Assignment2.Repositories
{
    public interface IDepartmentRepositoryInterface
    {
        Task<ICollection<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(string departmentId);
        Task<bool> CreateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentByIdAsync(string departmentId);
        Task<bool> UpdateDepartmentAsync(string departmentId, Department department);
        Task<bool> IsDepartmentExistAsync(string departmentId);
    }
}
