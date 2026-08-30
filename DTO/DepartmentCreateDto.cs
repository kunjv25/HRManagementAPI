using System.ComponentModel.DataAnnotations;

namespace HRManagementAPI.DTO
{
    public class DepartmentCreateDto
    {
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
    }
}