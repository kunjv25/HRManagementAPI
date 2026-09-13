using HRManagementAPI.Models;

namespace HRManagementAPI.Services.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByUserIdAsync(string userId);                             // Get my-profile

        IQueryable<Employee> GetAllEmployees();                                      // Get employees query

        Task<Employee?> GetEmployeeByIdAsync(int id);                               // Get employee by ID

        Task CreateEmployee(Employee employee);                             // Add employee

        void UpdateEmployee(Employee employee);                             // Update employee

        void DeleteEmployee(Employee employee);                             // Delete employee

        Task<bool> IsEmployeeExistsAsync(int id);                           // Check employee exists

        Task<bool> IsEmployeeEmailExistsAsync(string email);                // Check employee email exists

        Task SaveChangesAsync();                                            // Save database changes
    }
}