using System.ComponentModel.DataAnnotations;

namespace HRManagementAPI.DTO
{
    public class DepartmentUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
    }
}