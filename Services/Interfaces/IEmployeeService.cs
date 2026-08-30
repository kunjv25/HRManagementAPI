using HRManagementAPI.DTOs;
using HRManagementAPI.DTOs.Employee;

namespace HRManagementAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDto>> GetAllAsync();                          // Get all employees

        Task<EmployeeResponseDto> GetByIdAsync(int id);                         // Get employee by ID

        Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto dto);           // Create employee

        Task<EmployeeResponseDto> UpdateAsync(int id, EmployeeUpdateDto dto);   // Update employee

        Task<bool> DeleteAsync(int id);                                         // Delete employee
    }
}