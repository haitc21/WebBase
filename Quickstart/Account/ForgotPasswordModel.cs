using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Quickstart.Account
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Địa chỉ Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Địa chỉ Emial không hợp lệ!")]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        public string ReturnUrl { get; set; }
    }
}
