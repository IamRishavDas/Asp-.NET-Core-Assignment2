using Assignment2.CustomExceptions;
using Assignment2.Dtos;
using Assignment2.Models;
using Assignment2.Repositories;
using AutoMapper;

namespace Assignment2.Services
{
    public class DepartmentService : IDepartmentServiceInterface
    {
        private readonly IDepartmentRepositoryInterface _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepositoryInterface departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            try
            {
                //Department department = new Department()
                //{
                //    DepartmentId = departmentDto.DepartmentId,
                //    DepartmentName = departmentDto.DepartmentName
                //};
                return await _departmentRepository.CreateDepartmentAsync(_mapper.Map<Department>(departmentDto));
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while creating {departmentDto}, Exception: {ex}", false, ex);
            }
        }

        public async Task<bool> DeleteDepartmentByIdAsync(string departmentId)
        {
            try
            {
                return await _departmentRepository.DeleteDepartmentByIdAsync(departmentId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while Deleting DepartmentId: {departmentId}, Exception: {ex}", false, ex);
            }
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(string departmentId)
        {
            try
            {
                return _mapper.Map<DepartmentDto>(await _departmentRepository.GetDepartmentByIdAsync(departmentId));
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching DepartmentId: {departmentId}, Exception: {ex}", false, ex);
            }
        }

        public async Task<ICollection<DepartmentDto>> GetDepartmentsAsync()
        {
            try
            {
                return _mapper.Map<ICollection<DepartmentDto>>(await _departmentRepository.GetDepartmentsAsync());
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching the Departments, Exception: {ex}", false, ex);
            }
        }

        public async Task<bool> IsDepartmentExistAsync(string departmentId)
        {
            try
            {
                return await _departmentRepository.IsDepartmentExistAsync(departmentId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching the Department from Database for DepartmentId: {departmentId}", false, ex);
            }
        }

        public async Task<bool> UpdateDepartmentAsync(string departmentId, DepartmentDto departmentDto)
        {
            try
            {
                if (!departmentId.Equals(departmentDto.DepartmentId)) return false;
                return await _departmentRepository.UpdateDepartmentAsync(departmentId, _mapper.Map<Department>(departmentDto));
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while updating {departmentDto}, Exception: {ex}", false, ex);
            }
        }
    }
}
