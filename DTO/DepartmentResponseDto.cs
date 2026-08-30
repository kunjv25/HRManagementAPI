namespace HRManagementAPI.DTO
{
    public class DepartmentResponseDto
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}