namespace HRManagementAPI.DTO.Employee
{
    public class EmployeeCreateResponseDto
    {
        public EmployeeResponseDto Employee { get; set; } = null!;
        public string TemporaryPassword { get; set; } = string.Empty;
    }
}