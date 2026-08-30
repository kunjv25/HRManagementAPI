using HRManagementAPI.Data;
using HRManagementAPI.DTO;
using HRManagementAPI.DTOs;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagementAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HRDbContext _context;

        public DepartmentService(HRDbContext context)
        {
            _context = context;
        }


        // Get all departments
        public async Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentResponseDto
                {
                    Id = d.Id,
                    DepartmentName = d.DepartmentName
                })
                .ToListAsync();
        }


        // Get department by ID
        public async Task<DepartmentResponseDto?> GetDepartmentByIdAsync(int id) 
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return null;

            return new DepartmentResponseDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }


        // Create department
        public async Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName
            };

            _context.Departments.Add(department);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Department name already exists.");
            }

            return new DepartmentResponseDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }


        // Update department
        public async Task<DepartmentResponseDto?> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto) 
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return null;

            department.DepartmentName = dto.DepartmentName;

            await _context.SaveChangesAsync();

            return new DepartmentResponseDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }


        // Delete department
        public async Task<bool> DeleteDepartmentAsync(int id) 
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return false;

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}