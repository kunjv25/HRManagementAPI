using HRManagementAPI.DTO;

namespace HRManagementAPI.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync();                      // Get all Departments

        Task<DepartmentResponseDto?> GetDepartmentByIdAsync(int id);                            // Get Department by ID

        Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto dto);             // Create Department

        Task<DepartmentResponseDto?> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto);    // Update Department

        Task<bool> DeleteDepartmentAsync(int id);                                               // Delete Department
    }
}