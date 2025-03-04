using Assignment2.Dtos;
using Assignment2.Models;
using Assignment2.Repositories;
using Assignment2.CustomExceptions;
using AutoMapper;

namespace Assignment2.Services
{
    public class EmployeeService : IEmployeeServiceInterface
    {
        private readonly IEmployeeRepositoryInterface _employeeRepository;
        private readonly IDepartmentRepositoryInterface _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepositoryInterface employeeRepository, IDepartmentRepositoryInterface departmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateEmployeeAsync(string departmentId, EmployeeDto employeeDto)
        {
            if (!departmentId.Equals(employeeDto.DepartmentId)) return false;
            Employee employee = new Employee();
            try
            {
                var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (department.DepartmentName == null) return false;
                employee = new Employee()
                {
                    EmployeeId = employeeDto.EmployeeId,
                    EmployeeName = employeeDto.EmployeeName,
                    EmployeeAge = employeeDto.EmployeeAge,
                    Salary = employeeDto.Salary,
                    DepartmentId = departmentId,
                    Department = department
                };
                return await _employeeRepository.CreateEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while creating {_mapper.Map<EmployeeDto>(employee)}, Exception: {ex}", false, ex);
            }
        }

        public async Task<bool> DeleteEmployeeByIdAsync(string departmentId, string employeeId)
        {
            try
            {
                var deprartment = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (deprartment == null || deprartment.DepartmentId == null) return false;
                return await _employeeRepository.DeleteEmployeeByIdAsync(departmentId, employeeId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while Deleting EmployeeId: {employeeId}, Exception: {ex}", false, ex);
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(string departmentId, string employeeId)
        {
            try
            {
                return _mapper.Map<EmployeeDto>(await _employeeRepository.GetEmployeeByIdAsync(departmentId, employeeId));
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching {employeeId}, Exception: {ex}", false, ex);
            }
        }

        public async Task<ICollection<EmployeeDto>> GetEmployeesAsync()
        {
            try
            {
                return _mapper.Map<ICollection<EmployeeDto>>(await _employeeRepository.GetEmployeesAsync());
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList", false, ex);
            }
        }

        public async Task<ICollection<EmployeeDto>> GetEmployeesByDepartmentIdAsync(string departmentId)
        {
            try
            {
                var employeesByDepartmentId = await _employeeRepository.GetEmployeesByDepartmentIdAsync(departmentId);
                return _mapper.Map<ICollection<EmployeeDto>>(employeesByDepartmentId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching EmployeesListByDepartmentId", false, ex);
            }
        }

        public async Task<bool> IsEmployeeExistAsync(string employeeId)
        {
            try
            {
                return await _employeeRepository.IsEmployeeExistAsync(employeeId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Exception: ", false, ex);
            }
        }

        public async Task<bool> UpdateEmployeeAsync(string departmentId, string employeeId, EmployeeDto employeeDto)
        {
            //if (!departmentId.Equals(employeeDto.DepartmentId)) return false;
            if (!employeeId.Equals(employeeDto.EmployeeId)) return false;
            Employee employee = new Employee();
            try
            {
                var departmentByPathParam = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (departmentByPathParam == null || departmentByPathParam.DepartmentId == null) return false;
                
                var departmentByEmployeeDto = await _departmentRepository.GetDepartmentByIdAsync(employeeDto.DepartmentId);
                if (departmentByEmployeeDto == null || departmentByEmployeeDto.DepartmentId == null) return false;

                employee.EmployeeId = employeeDto.EmployeeId;
                employee.EmployeeName = employeeDto.EmployeeName;
                employee.EmployeeAge = employeeDto.EmployeeAge;
                employee.Salary = employeeDto.Salary;
                employee.DepartmentId = departmentByEmployeeDto.DepartmentId;
                employee.Department = departmentByEmployeeDto;

                return await _employeeRepository.UpdateEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while updating Employee: {employeeDto}, Exception: {ex}", false, ex);
            }
        }
    }
}
