using Assignment2.Data;
using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Repositories
{
    public class DepartmentRepository : IDepartmentRepositoryInterface
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateDepartmentAsync(Department department)
        {
            if (await IsDepartmentExistAsync(department.DepartmentId)) return false;
            await _context.Departments.AddAsync(department);
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<bool> DeleteDepartmentByIdAsync(string departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            if (department == null) return false;
            _context.Departments.Remove(department);
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<Department> GetDepartmentByIdAsync(string departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            return department ?? new Department();
        }

        public async Task<ICollection<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<bool> IsDepartmentExistAsync(string departmentId)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentId.Equals(departmentId));
        }

        public async Task<bool> UpdateDepartmentAsync(string departmentId, Department department)
        {
            var dept = await _context.Departments.FindAsync(departmentId);
            if (dept == null || dept.DepartmentId == null) return false;

            dept.DepartmentId = department.DepartmentId;
            dept.DepartmentName = department.DepartmentName;

            _context.Departments.Update(dept);

            return (await _context.SaveChangesAsync()) == 1;
        }
    }
}
