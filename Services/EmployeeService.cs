using HRManagementAPI.Data;
using HRManagementAPI.DTOs.Employee;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRDbContext _context;

        public EmployeeService(HRDbContext context)
        {
            _context = context;
        }

        // GET ALL EMPLOYEES
        public async Task<List<EmployeeResponseDto>> GetAllAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    JoiningDate = e.JoiningDate,
                    IsActive = e.IsActive,
                    LeavingDate = e.LeavingDate,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : null
                })
                .ToListAsync();
        }


        // GET EMPLOYEE BY ID
        public async Task<EmployeeResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    JoiningDate = e.JoiningDate,
                    IsActive = e.IsActive,
                    LeavingDate = e.LeavingDate,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : null
                })
                .FirstOrDefaultAsync();
        }


        // CREATE EMPLOYEE
        public async Task<EmployeeResponseDto> CreateAsync(
            EmployeeCreateDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Salary = dto.Salary,
                JoiningDate = dto.JoiningDate,
                IsActive = dto.IsActive,
                LeavingDate = dto.LeavingDate,
                DepartmentId = dto.DepartmentId
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                JoiningDate = employee.JoiningDate,
                IsActive = employee.IsActive,
                LeavingDate = employee.LeavingDate,
                DepartmentId = employee.DepartmentId
            };
        }


        // UPDATE EMPLOYEE
       public async Task<EmployeeResponseDto?> UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return null;
            }

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Salary = dto.Salary;
            employee.JoiningDate = dto.JoiningDate;
            employee.IsActive = dto.IsActive;
            employee.LeavingDate = dto.LeavingDate;
            employee.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                JoiningDate = employee.JoiningDate,
                IsActive = employee.IsActive,
                LeavingDate = employee.LeavingDate,
                DepartmentId = employee.DepartmentId
            };
        }


        // DELETE EMPLOYEE
        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}