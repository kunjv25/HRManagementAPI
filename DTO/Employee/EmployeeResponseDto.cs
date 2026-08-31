namespace HRManagementAPI.DTO.Employee
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        public bool IsActive { get; set; }

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }
    }
}