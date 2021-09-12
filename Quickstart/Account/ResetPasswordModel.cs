using System.ComponentModel.DataAnnotations;

namespace WebBase.Quickstart.Account
{
    public class ResetPasswordModel
    {
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Password phải giống nhau.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public string ReturnUrl { get; set; }
    }
}