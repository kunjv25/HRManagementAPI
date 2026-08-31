using HRManagementAPI.Data;
using HRManagementAPI.DTO.Employee;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRDbContext _context;
        private readonly ILogger<EmployeeService> _logger;


        public EmployeeService(HRDbContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET ALL EMPLOYEES
        public async Task<EmployeePagedResponseDto> GetAllAsync(int pageNumber, int pageSize, string? search)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.FirstName.Contains(search) ||
                    e.LastName.Contains(search) ||
                    e.Email.Contains(search));
            }

            var totalRecords = await query.CountAsync();

            var employees = await query
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new EmployeePagedResponseDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Employees = employees
            };
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
        public async Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto dto)
        {
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId);

            if (!departmentExists)
            {
                _logger.LogWarning($"Employee creation failed. Department {dto.DepartmentId} not found.");
                throw new KeyNotFoundException("Department not found.");
            }

            var emailExists = await _context.Employees.AnyAsync(e => e.Email == dto.Email);

            if (emailExists)
            {
                _logger.LogWarning("Employee creation failed because email already exists.");
                throw new InvalidOperationException("Employee with this email already exists.");
            }

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Salary = dto.Salary,
                JoiningDate = dto.JoiningDate,
                IsActive = dto.IsActive,
                DepartmentId = dto.DepartmentId
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Employee {employee.Id} created successfully.");

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

            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId);

            if (!departmentExists)
            {
                _logger.LogWarning($"Employee creation failed. Department {dto.DepartmentId} not found.");
                throw new KeyNotFoundException("Department not found.");
            }

            var emailExists = await _context.Employees.AnyAsync(e => e.Email == dto.Email && e.Id != id);

            if (emailExists)
            {
                _logger.LogWarning("Employee creation failed because email already exists.");
                throw new InvalidOperationException("Employee with this email already exists.");
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
            _logger.LogInformation($"Employee {employee.Id} updated successfully.");

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
            _logger.LogInformation($"Employee {employee.Id} deleted successfully.");

            return true;
        }
    }
}