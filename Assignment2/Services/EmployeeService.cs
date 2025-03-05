using Assignment2.Dtos;
using Assignment2.Models;
using Assignment2.Repositories;
using Assignment2.CustomExceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Assignment2.CustomResponses;

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

        public async Task<ServiceResponse<bool>> CreateEmployeeAsync(string departmentId, EmployeeDto employeeDto)
        {
            Employee employee = new Employee();

            try
            {
                if (departmentId == null || employeeDto == null) throw new ArgumentNullException("DepartmentId or EmployeeDto is null!");

                if (!departmentId.Equals(employeeDto.DepartmentId)) return ServiceResponse<bool>.Failure($"Path: {departmentId} not match EmployeeDto.DepartmentId: {employeeDto.DepartmentId}");
                if (await _employeeRepository.IsEmployeeExistAsync(employeeDto.EmployeeId)) return ServiceResponse<bool>.Failure($"EmployeeId: {employeeDto.EmployeeId} already exist");
                var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (department == null || department.DepartmentId == null || department.DepartmentName == null) return ServiceResponse<bool>.Failure($"DepartmentId: {departmentId} not exist");
                
                employee = new Employee()
                {
                    EmployeeId = employeeDto.EmployeeId,
                    EmployeeName = employeeDto.EmployeeName,
                    EmployeeAge = employeeDto.EmployeeAge,
                    Salary = employeeDto.Salary,
                    DepartmentId = departmentId,
                    Department = department
                };
                var isCreateOperationSuccedd = await _employeeRepository.CreateEmployeeAsync(employee);
                var response = isCreateOperationSuccedd ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Failure($"Error while creating Employee: {_mapper.Map<EmployeeDto>(employee)}");
                return response;
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

        public async Task<ServiceResponse<bool>> DeleteEmployeeByIdAsync(string departmentId, string employeeId)
        {
            try
            {
                if (departmentId == null || employeeId == null) throw new ArgumentNullException("DepartmentId or EmployeeId is null!");

                var deprartment = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (deprartment == null || deprartment.DepartmentId == null) return ServiceResponse<bool>.Failure($"DepartmentId: {departmentId} not exist");

                var isEmployeeExist = await _employeeRepository.IsEmployeeExistAsync(employeeId);
                if (!isEmployeeExist) return ServiceResponse<bool>.Failure($"EmployeeId: {employeeId} not exist");
                
                
                var isDeleteEmployeeSucceed = await _employeeRepository.DeleteEmployeeByIdAsync(departmentId, employeeId);
                var response = isDeleteEmployeeSucceed ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Failure($"Error while deleting EmployeeId: {employeeId}");
                return response;
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

        public async Task<ServiceResponse<EmployeeDto>> GetEmployeeByIdAsync(string departmentId, string employeeId)
        {
            try
            {
                if (departmentId == null || employeeId == null) throw new ArgumentNullException("DepartmentId or EmployeeId is null!");

                var isDepartmentExist = await _departmentRepository.IsDepartmentExistAsync(departmentId);
                if(!isDepartmentExist) return ServiceResponse<EmployeeDto>.Failure($"DepartmentId: {departmentId} not exist");

                var employeeDto =  _mapper.Map<EmployeeDto>(await _employeeRepository.GetEmployeeByIdAsync(departmentId, employeeId));
                if (employeeDto == null || employeeDto.EmployeeId == null) return ServiceResponse<EmployeeDto>.Failure($"EmployeeId: {employeeId} not exist");

                return ServiceResponse<EmployeeDto>.Success(employeeDto);
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

        public async Task<ServiceResponse<ICollection<EmployeeDto>>> GetEmployeesAsync()
        {
            try
            {
                var employeeList =  _mapper.Map<ICollection<EmployeeDto>>(await _employeeRepository.GetEmployeesAsync());
                if (employeeList == null || employeeList.Count() == 0) return ServiceResponse<ICollection<EmployeeDto>>.Failure($"No employees found");

                return ServiceResponse<ICollection<EmployeeDto>>.Success(employeeList);
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

        public async Task<ServiceResponse<ICollection<EmployeeDto>>> GetEmployeesByDepartmentIdAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException("DepartmentId is null!");

                var isDepartmentExist = await _departmentRepository.IsDepartmentExistAsync(departmentId);
                if (!isDepartmentExist) return ServiceResponse<ICollection<EmployeeDto>>.Failure($"DepartmentId: {departmentId} not exist");

                var employeesByDepartmentId = await _employeeRepository.GetEmployeesByDepartmentIdAsync(departmentId);
                if (employeesByDepartmentId == null || employeesByDepartmentId.Count() == 0) return ServiceResponse<ICollection<EmployeeDto>>.Failure($"DepartmentId: {departmentId} contains no employee");
                return ServiceResponse<ICollection<EmployeeDto>>.Success(_mapper.Map<ICollection<EmployeeDto>>(employeesByDepartmentId));
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

        public async Task<ServiceResponse<bool>> IsEmployeeExistAsync(string employeeId)
        {
            try
            {
                if (employeeId == null) throw new ArgumentNullException("EmployeeId is null!");
                var isEmployeeExist =  await _employeeRepository.IsEmployeeExistAsync(employeeId);

                if (!isEmployeeExist) return ServiceResponse<bool>.Failure($"EmployeeId: {employeeId} not found");
                return ServiceResponse<bool>.Success(true);
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

        public async Task<ServiceResponse<bool>> UpdateEmployeeAsync(string departmentId, string employeeId, EmployeeDto employeeDto)
        {
            Employee employee = new Employee();

            try
            {
                if (departmentId == null || employeeId == null || employeeDto == null) throw new ArgumentNullException("DepartmentId or EmployeeId or EmployeeDto is null!");
                if (!employeeId.Equals(employeeDto.EmployeeId)) return ServiceResponse<bool>.Failure($"Path EmployeeId: {employeeId} and Request object: {employeeDto.EmployeeId} not match");

                if (!(await IsEmployeeExistAsync(employeeId)).IsSuccess) return ServiceResponse<bool>.Failure($"EmployeeId: {employeeId} not exist");

                var departmentByPathParam = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                if (departmentByPathParam == null || departmentByPathParam.DepartmentId == null) return ServiceResponse<bool>.Failure($"DepartmentId: {departmentId} not exist");

                var empValidationWithDepartment = await _employeeRepository.GetEmployeeByIdAsync(departmentId, employeeDto.EmployeeId);
                if (empValidationWithDepartment == null || empValidationWithDepartment.EmployeeId == null) return ServiceResponse<bool>.Failure($"EmployeeId: {employeeId} not exist in DepartmentId: {departmentId}");

                var departmentByEmployeeDto = await _departmentRepository.GetDepartmentByIdAsync(employeeDto.DepartmentId);
                if (departmentByEmployeeDto == null || departmentByEmployeeDto.DepartmentId == null) return ServiceResponse<bool>.Failure($"Requested DepartmentId: {employeeDto.DepartmentId} not exist"); ;

                employee.EmployeeId = employeeDto.EmployeeId;
                employee.EmployeeName = employeeDto.EmployeeName;
                employee.EmployeeAge = employeeDto.EmployeeAge;
                employee.Salary = employeeDto.Salary;
                employee.DepartmentId = departmentByEmployeeDto.DepartmentId;
                employee.Department = departmentByEmployeeDto;

                var isEmployeeUpdated =  await _employeeRepository.UpdateEmployeeAsync(employee);
                var response = isEmployeeUpdated ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Failure($"Error updating EmployeeId: {employeeId}");
                return response;
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
