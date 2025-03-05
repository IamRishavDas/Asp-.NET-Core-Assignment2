using Assignment2.Dtos;
using Assignment2.Models;
using Assignment2.Repositories;
using Assignment2.CustomExceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
            Employee employee = new Employee();

            try
            {
                if (departmentId == null || employeeDto == null) throw new ArgumentNullException("DepartmentId or EmployeeDto is null!");
                if (!departmentId.Equals(employeeDto.DepartmentId)) return false;
                if (await _employeeRepository.IsEmployeeExistAsync(employeeDto.EmployeeId)) return false;
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
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while creating {_mapper.Map<EmployeeDto>(employee)}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while creating {_mapper.Map<EmployeeDto>(employee)}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while creating {_mapper.Map<EmployeeDto>(employee)}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while creating {_mapper.Map<EmployeeDto>(employee)}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while creating {_mapper.Map<EmployeeDto>(employee)}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<bool> DeleteEmployeeByIdAsync(string departmentId, string employeeId)
        {
            try
            {
                if (departmentId == null || employeeId == null) throw new ArgumentNullException("DepartmentId or EmployeeId is null!");
                var deprartment = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (deprartment == null || deprartment.DepartmentId == null) return false;
                return await _employeeRepository.DeleteEmployeeByIdAsync(departmentId, employeeId);
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while Deleting EmployeeId: {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while Deleting EmployeeId: {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while Deleting EmployeeId: {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while Deleting EmployeeId: {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while Deleting EmployeeId: {employeeId}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(string departmentId, string employeeId)
        {
            try
            {
                if (departmentId == null || employeeId == null) throw new ArgumentNullException("DepartmentId or EmployeeId is null!");
                return _mapper.Map<EmployeeDto>(await _employeeRepository.GetEmployeeByIdAsync(departmentId, employeeId));
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while fetching {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while fetching {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while fetching {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while fetching {employeeId}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching {employeeId}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<ICollection<EmployeeDto>> GetEmployeesAsync()
        {
            try
            {
                return _mapper.Map<ICollection<EmployeeDto>>(await _employeeRepository.GetEmployeesAsync());
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<ICollection<EmployeeDto>> GetEmployeesByDepartmentIdAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException("DepartmentId is null!");
                var employeesByDepartmentId = await _employeeRepository.GetEmployeesByDepartmentIdAsync(departmentId);
                return _mapper.Map<ICollection<EmployeeDto>>(employeesByDepartmentId);
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching EmployeesList for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<bool> IsEmployeeExistAsync(string employeeId)
        {
            try
            {
                if (employeeId == null) throw new ArgumentNullException("EmployeeId is null!");
                return await _employeeRepository.IsEmployeeExistAsync(employeeId);
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while fetching details of employeeId: {employeeId}, Exception; {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while fetching details of employeeId: {employeeId}, Exception; {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while fetching details of employeeId: {employeeId}, Exception; {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while fetching details of employeeId: {employeeId}, Exception; {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching details of employeeId: {employeeId}, Exception; {ex.Message}", false, ex);
            }
        }

        public async Task<bool> UpdateEmployeeAsync(string departmentId, string employeeId, EmployeeDto employeeDto)
        {
            Employee employee = new Employee();

            try
            {
                if (departmentId == null || employeeId == null || employeeDto == null) throw new ArgumentNullException("DepartmentId or EmployeeId or EmployeeDto is null!");
                if (!employeeId.Equals(employeeDto.EmployeeId)) return false;

                var departmentByPathParam = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (departmentByPathParam == null || departmentByPathParam.DepartmentId == null) return false;

                var empValidationWithDepartment = await _employeeRepository.GetEmployeeByIdAsync(departmentId, employeeDto.EmployeeId);
                if (empValidationWithDepartment == null || empValidationWithDepartment.EmployeeId == null) return false;
                
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
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while updating Employee: {employeeDto}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while updating Employee: {employeeDto}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while updating Employee: {employeeDto}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while updating Employee: {employeeDto}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while updating Employee: {employeeDto}, Exception: {ex.Message}", false, ex);
            }
        }
    }
}
