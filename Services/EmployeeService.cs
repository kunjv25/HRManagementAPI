using HRManagementAPI.DTO.Employee;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Interfaces;
using HRManagementAPI.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _logger = logger;
        }

        // GET ALL EMPLOYEES
        public async Task<EmployeePagedResponseDto> GetAllAsync(int pageNumber, int pageSize, string? search, int? departmentId, bool? isActive, string? sortBy, string? sortOrder)
        {
            var query = _employeeRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                // WHERE (FirstName LIKE '%john%' OR LastName LIKE '%john%' OR Email LIKE '%john%') (only query is made)
                query = query.Where(e => e.FirstName.Contains(search) || e.LastName.Contains(search) || e.Email.Contains(search));   
            }

            if (departmentId.HasValue)
            {
                // Where () and DepartmentId = 2 (only query is made)
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }

            if (isActive.HasValue)
            {
                // Where () and () and IsActive = 1 (only query is made)
                query = query.Where(e => e.IsActive == isActive.Value);
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy))
            {                
                query = query.OrderBy(e => e.Id);                                                                                               // Default Id (Asc sorting)
            }

            else if (sortBy.ToLower() == "firstname")
            {
                query = sortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.FirstName) : query.OrderBy(e => e.FirstName);           // ?sortBy=firstname & sortOrder=desc/asc
            }

            else if (sortBy.ToLower() == "lastname")
            {
                query = sortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.LastName) : query.OrderBy(e => e.LastName);             // ?sortBy=lastname & sortOrder=desc/asc
            }

            else if (sortBy.ToLower() == "salary")
            {
                query = sortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.Salary) : query.OrderBy(e => e.Salary);                 // ?sortBy=salary & sortOrder=desc/asc
            }

            else if (sortBy.ToLower() == "joiningdate")
            {
                query = sortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.JoiningDate) : query.OrderBy(e => e.JoiningDate);       // ?sortBy=joiningdate & sortOrder=asc/desc
            }

            else
            {
                query = query.OrderBy(e => e.Id);                                                                                               // Default sorting if invalid sortBy is provided
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
                .ToListAsync();     // query execute

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
            // Get employee from repository
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            // Convert Entity → Response DTO
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
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.DepartmentName
            };
        }


        // CREATE EMPLOYEE
        public async Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto dto)
        {
            var departmentExists = await _departmentRepository.IsDepartmentExistsAsync(dto.DepartmentId);

            if (!departmentExists)
            {
                _logger.LogWarning($"Employee creation failed. Department {dto.DepartmentId} not found.");
                throw new KeyNotFoundException("Department not found.");
            }

            var emailExists = await _employeeRepository.IsEmployeeEmailExistsAsync(dto.Email);

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

            _employeeRepository.CreateEmployee(employee);

            await _employeeRepository.SaveChangesAsync();
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
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            var departmentExists = await _departmentRepository.IsDepartmentExistsAsync(dto.DepartmentId);

            if (!departmentExists)
            {
                _logger.LogWarning($"Employee creation failed. Department {dto.DepartmentId} not found.");
                throw new KeyNotFoundException("Department not found.");
            }

            var emailExists = await _employeeRepository.IsEmployeeEmailExistsAsync(dto.Email);

            if (emailExists && employee.Email != dto.Email)
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

            _employeeRepository.UpdateEmployee(employee);

            await _employeeRepository.SaveChangesAsync();
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
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return false;
            }

            _employeeRepository.DeleteEmployee(employee);

            await _employeeRepository.SaveChangesAsync();
            _logger.LogInformation($"Employee {employee.Id} deleted successfully.");

            return true;
        }
    }
}