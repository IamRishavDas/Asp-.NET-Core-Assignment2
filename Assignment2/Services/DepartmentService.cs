using System.Runtime.Intrinsics.Arm;
using Assignment2.CustomExceptions;
using Assignment2.CustomResponses;
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

        public async Task<ServiceResponse<bool>> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto == null) throw new ArgumentNullException("DepartmentDto is null!");

                if ((await IsDepartmentExistAsync(departmentDto.DepartmentId)).IsSuccess) return ServiceResponse<bool>.Failure($"DepartmentId: {departmentDto.DepartmentId} already exist");

                var isDepartmentCreated = await _departmentRepository.CreateDepartmentAsync(_mapper.Map<Department>(departmentDto));

                var response = isDepartmentCreated ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Failure($"Error Creating new Department");
                return response;
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

        public async Task<ServiceResponse<bool>> DeleteDepartmentByIdAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException($"DepartmentId is null!");

                var isDepartmentExistServiceResponse = await IsDepartmentExistAsync(departmentId);
                if (!isDepartmentExistServiceResponse.IsSuccess) return ServiceResponse<bool>.Failure($"DepartmentId: {departmentId} not exist");

                var isDepartmentDeleted = await _departmentRepository.DeleteDepartmentByIdAsync(departmentId);
                return isDepartmentDeleted ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Failure($"Error while deleting DepartmentId: {departmentId}");
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

        public async Task<ServiceResponse<DepartmentDto>> GetDepartmentByIdAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException("DepartmentId is null!");

                var departmentDto = _mapper.Map<DepartmentDto>(await _departmentRepository.GetDepartmentByIdAsync(departmentId));
                if (departmentDto == null || departmentDto.DepartmentId == null) return ServiceResponse<DepartmentDto>.Failure($"DepartmentId: {departmentId} not exist");

                return ServiceResponse<DepartmentDto>.Success(departmentDto);
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

        public async Task<ServiceResponse<ICollection<DepartmentDto>>> GetDepartmentsAsync()
        {
            try
            {
                var departmentList = _mapper.Map<ICollection<DepartmentDto>>(await _departmentRepository.GetDepartmentsAsync());
                if (departmentList == null || departmentList.Count() == 0) return ServiceResponse<ICollection<DepartmentDto>>.Failure("No Department found");

                return ServiceResponse<ICollection<DepartmentDto>>.Success(departmentList);
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

        public async Task<ServiceResponse<bool>> IsDepartmentExistAsync(string departmentId)
        {
            try
            {
                if (departmentId == null) throw new ArgumentNullException("DepartmentId is null!");
                var isDepartmentExist =  await _departmentRepository.IsDepartmentExistAsync(departmentId);
                return isDepartmentExist ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Failure($"DepartmentId: {departmentId} Not Exist");
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

        public async Task<ServiceResponse<bool>> UpdateDepartmentAsync(string departmentId, DepartmentDto departmentDto)
        {
            try
            {
                if (departmentId == null || departmentId == null) throw new ArgumentNullException("DepartmentId or DepartmentDto is null!");

                if (!departmentId.Equals(departmentDto.DepartmentId)) return ServiceResponse<bool>.Failure($"Path DepartmentId: {departmentId} not match Request body DepartmentId: {departmentDto.DepartmentId}");

                if (!(await IsDepartmentExistAsync(departmentId)).IsSuccess) return ServiceResponse<bool>.Failure($"DepartmentId: {departmentId} not exist");

                var isDepartmentUpdated = await _departmentRepository.UpdateDepartmentAsync(departmentId, _mapper.Map<Department>(departmentDto));
                return isDepartmentUpdated ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Failure($"Error while updating DepartmentId: {departmentId}");
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
