using HRManagementAPI.Models;

namespace HRManagementAPI.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IQueryable<Employee>> GetAllAsync();                   // Get all employees

        Task<Employee?> GetByIdAsync(int id);                       // Get employee by ID 

        Task AddAsync(Employee employee);                           // Create employee

        void Update(Employee employee);                             // Update employee

        void Delete(Employee employee);                             // Delete employee

        Task<bool> ExistsAsync(int id);

        Task<bool> EmailExistsAsync(string email);                  
    }
}