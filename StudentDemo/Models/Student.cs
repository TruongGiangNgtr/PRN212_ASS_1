using System.ComponentModel.DataAnnotations;

namespace StudentDemo.Models;

public class Student
{
    public int Id { get; set; }

    [Display(Name = "Họ tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Tuổi")]
    [Required(ErrorMessage = "Vui lòng nhập tuổi.")]
    [Range(16, 60, ErrorMessage = "Tuổi phải từ 16 đến 60.")]
    public int Age { get; set; }
}
