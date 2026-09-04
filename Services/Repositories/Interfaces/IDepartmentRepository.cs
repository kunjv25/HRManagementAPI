using HRManagementAPI.Models;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();                    // Get all departments

    Task<Department?> GetByIdAsync(int id);                  // Get department by ID

    Task<bool> IsDepartmentExistsAsync(int id);              // Check whether department exists

    Task<bool> IsDepartmentNameExistsAsync(string name);               // Check whether department name already exists

    Task<bool> IsDepartmentNameExistsAsync(string name, int id);       // Check name exists excluding current department

    Task AddAsync(Department department);                    // Add new department

    void Update(Department department);                       // Update department

    void Delete(Department department);                       // Delete department

    Task SaveChangesAsync();                                  // Save changes to database
}