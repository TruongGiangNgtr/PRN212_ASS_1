using System.ComponentModel.DataAnnotations;

namespace StudentDemo.Models;

public class UserEditForm
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [Display(Name = "Họ tên")]
    public string FullName { get; set; } = string.Empty;

    // Để trống = giữ mật khẩu cũ.
    [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu mới (để trống nếu không đổi)")]
    public string? NewPassword { get; set; }

    [Compare(nameof(NewPassword), ErrorMessage = "Hai mật khẩu không khớp")]
    [DataType(DataType.Password)]
    [Display(Name = "Nhập lại mật khẩu mới")]
    public string? ConfirmPassword { get; set; }
}
