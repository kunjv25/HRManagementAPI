using HRManagementAPI.DTO.Department;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Interfaces;
using HRManagementAPI.Services.Repositories.Interfaces;

namespace HRManagementAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();

            return departments.Select(d => new DepartmentResponseDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName
            });
        }

        public async Task<DepartmentResponseDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
                return null;

            return new DepartmentResponseDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }

        public async Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto dto)
        {
            var nameExists = await _departmentRepository.IsDepartmentNameExistsAsync(dto.DepartmentName);

            if (nameExists)
                throw new InvalidOperationException("A department with this name already exists.");

            var department = new Department
            {
                DepartmentName = dto.DepartmentName
            };

            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveChangesAsync();

            return new DepartmentResponseDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }

        public async Task<DepartmentResponseDto?> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
                return null;

            var nameExists = await _departmentRepository.IsDepartmentNameExistsAsync(dto.DepartmentName, id);

            if (nameExists)
                throw new InvalidOperationException("A department with this name already exists.");

            department.DepartmentName = dto.DepartmentName;

            _departmentRepository.Update(department);
            await _departmentRepository.SaveChangesAsync();

            return new DepartmentResponseDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
                return false;

            _departmentRepository.Delete(department);
            await _departmentRepository.SaveChangesAsync();

            return true;
        }
    }
}