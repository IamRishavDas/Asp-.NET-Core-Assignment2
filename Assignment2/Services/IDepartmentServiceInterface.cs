using Assignment2.CustomResponses;
using Assignment2.Dtos;
using Assignment2.Models;

namespace Assignment2.Services
{
    public interface IDepartmentServiceInterface
    {
        Task<ServiceResponse<ICollection<DepartmentDto>>> GetDepartmentsAsync();
        Task<ServiceResponse<DepartmentDto>> GetDepartmentByIdAsync(string departmentId);
        Task<ServiceResponse<bool>> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task<ServiceResponse<bool>> DeleteDepartmentByIdAsync(string departmentId);
        Task<ServiceResponse<bool>> UpdateDepartmentAsync(string departmentId, DepartmentDto departmentDto);
        Task<ServiceResponse<bool>> IsDepartmentExistAsync(string departmentId);
    }
}
