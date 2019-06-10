using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipHMSModels.ViewModels
{
    [NotMapped]
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    [NotMapped]
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    [NotMapped]
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    [NotMapped]
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    [NotMapped]
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    [NotMapped]
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد")]
        public string Password { get; set; }

        [Display(Name = "من را به خاطر بسپار")]
        public bool RememberMe { get; set; }
        public string token { get; set; }
    }

    [NotMapped]
    public class RegisterViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "مقدار {0} الزامی میباشد")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "مقدار {0} الزامی میباشد")]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "مقدار {0} الزامی میباشد")]
        public string NationalCode { get; set; }
        [Required(ErrorMessage = "مقدار {0} الزامی میباشد")]
        [EmailAddress(ErrorMessage = "فرمت {0} وارد شده صحیح نمیباشد")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی میباشد")]
        [StringLength(100, ErrorMessage = "مقدار {0} حداقل باید {2} باشد", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور با مقدار آن مطابقت ندارد")]
        public string ConfirmPassword { get; set; }
    }
    public static class RegisterMapper
    {
        public static Customer MapToCustomer(RegisterViewModel registerViewModel)
        {
            return new Customer()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                NationalCode = registerViewModel.NationalCode,
                Person_ApplicationUser = new ApplicationUser()
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    SerialNumber = Guid.NewGuid().ToString()
                }
            };
        }

    }
    [NotMapped]
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    [NotMapped]
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
