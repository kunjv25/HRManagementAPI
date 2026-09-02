using HRManagementAPI.Models;

namespace HRManagementAPI.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();                              // Get employees query

        Task<Employee?> GetByIdAsync(int id);                       // Get employee by ID

        Task AddAsync(Employee employee);                           // Add employee

        void Update(Employee employee);                             // Update employee

        void Delete(Employee employee);                             // Delete employee

        Task<bool> ExistsAsync(int id);                             // Check employee exists

        Task<bool> EmailExistsAsync(string email);                  // Check employee email exists

        Task SaveChangesAsync();                                    // Save database changes
    }
}