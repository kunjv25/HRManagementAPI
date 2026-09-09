using HRManagementAPI.DTO.Employee;

namespace HRManagementAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeePagedResponseDto> GetAllAsync(                                          // Get all employees
            int pageNumber, 
            int pageSize, 
            string? search, 
            int? departmentId, 
            bool? isActive, 
            string? sortBy, 
            string? sortOrder);                             

        Task<EmployeeResponseDto> GetByIdAsync(int id);                                      // Get employee by ID

        Task<EmployeeCreateResponseDto> CreateAsync(EmployeeCreateDto dto);                  // Create employee

        Task<EmployeeResponseDto> UpdateAsync(int id, EmployeeUpdateDto dto);                // Update employee

        Task<bool> DeleteAsync(int id);                                                      // Delete employee
    }
}