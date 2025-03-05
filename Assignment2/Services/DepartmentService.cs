using Assignment2.CustomExceptions;
using Assignment2.Dtos;
using Assignment2.Models;
using Assignment2.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
                if (departmentDto == null) throw new ArgumentNullException("DepartmentDto is null!");
                return await _departmentRepository.CreateDepartmentAsync(_mapper.Map<Department>(departmentDto));
            }
            catch(ArgumentNullException ex)
            {
                throw new ServiceException($"Error while creating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch(OperationCanceledException ex)
            {
                throw new ServiceException($"Error while creating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while creating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch(DbUpdateException ex)
            {
                throw new ServiceException($"Error while creating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while creating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<bool> DeleteDepartmentByIdAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException($"DepartmentId is null!");
                return await _departmentRepository.DeleteDepartmentByIdAsync(departmentId);
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while Deleting DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while Deleting DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while Deleting DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while Deleting DepartmentId:  {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while Deleting DepartmentId:  {departmentId}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException("DepartmentId is null!");
                return _mapper.Map<DepartmentDto>(await _departmentRepository.GetDepartmentByIdAsync(departmentId));
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while fetching DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while fetching DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while fetching DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while fetching DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<ICollection<DepartmentDto>> GetDepartmentsAsync()
        {
            try
            {
                return _mapper.Map<ICollection<DepartmentDto>>(await _departmentRepository.GetDepartmentsAsync());
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while fetching the Departments, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while fetching the Departments, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while fetching the Departments, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching the Departments, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<bool> IsDepartmentExistAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException("DepartmentId is null!");
                return await _departmentRepository.IsDepartmentExistAsync(departmentId);
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while fetching the Department from Database for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while fetching the Department from Database for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while fetching the Department from Database for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while fetching the Department from Database for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while fetching the Department from Database for DepartmentId: {departmentId}, Exception: {ex.Message}", false, ex);
            }
        }

        public async Task<bool> UpdateDepartmentAsync(string departmentId, DepartmentDto departmentDto)
        {
            try
            {
                if (departmentId == null || departmentId == null) throw new ArgumentNullException("DepartmentId or DepartmentDto is null!");

                if (!departmentId.Equals(departmentDto.DepartmentId)) return false;
                return await _departmentRepository.UpdateDepartmentAsync(departmentId, _mapper.Map<Department>(departmentDto));
            }
            catch (ArgumentNullException ex)
            {
                throw new ServiceException($"Error while updating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new ServiceException($"Error while updating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceException($"Error while updating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException($"Error while updating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error while updating {departmentDto}, Exception: {ex.Message}", false, ex);
            }
        }
    }
}
