using Assignment2.Dtos;
using Assignment2.Models;

namespace Assignment2.Services
{
    public interface IDepartmentServiceInterface
    {
        Task<ICollection<DepartmentDto>> GetDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(string departmentId);
        Task<bool> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task<bool> DeleteDepartmentByIdAsync(string departmentId);
        Task<bool> UpdateDepartmentAsync(string departmentId, DepartmentDto departmentDto);
        Task<bool> IsDepartmentExistAsync(string departmentId);
    }
}
