using HRManagementAPI.Models;

namespace HRManagementAPI.Services.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();                                      // Get employees query

        Task<Employee?> GetByIdAsync(int id);                               // Get employee by ID

        Task CreateEmployee(Employee employee);                             // Add employee

        void UpdateEmployee(Employee employee);                             // Update employee

        void DeleteEmployee(Employee employee);                             // Delete employee

        Task<bool> IsEmployeeExistsAsync(int id);                           // Check employee exists

        Task<bool> IsEmployeeEmailExistsAsync(string email);                // Check employee email exists

        Task SaveChangesAsync();                                            // Save database changes
    }
}