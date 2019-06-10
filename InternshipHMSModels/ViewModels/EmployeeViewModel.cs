using CustomMVCPackage.CustomAnnotation;
using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.ViewModels
{
    public enum EditState
    {
        UpdateEmployeeAndUser,
        UpdateEmployeeCreateUser,
        UpdateEmployee
    }
    [NotMapped]
    public class EmployeeViewModel
    {

        public string ID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage ="مقدار {0} الزامی است")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage ="مقدار {0} الزامی است")]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage ="مقدار {0} الزامی است")]
        public string NationalCode { get; set; }
        [Display(Name = "شماره پرسنلی")]
        [Required(ErrorMessage ="مقدار {0} الزامی است")]
        public string EmployeeCode { get; set; }
        public string EditModeStr { get; set; }

        public EditState EditMode
        {
            get
            {
                switch (EditModeStr)
                {
                    case "1":
                        return EditState.UpdateEmployeeAndUser;
                    case "3":
                        return EditState.UpdateEmployeeCreateUser;
                    case "4":
                        return EditState.UpdateEmployee;
                    default:
                        return EditState.UpdateEmployee;
                }
            }
        }

        public string SubmitUser { get; set; }
        public bool CreateUser
        {
            get { return bool.Parse(SubmitUser); }
        }
        [CompareRequired("SubmitUser",ErrorMessage ="مقدار {0} الزامی میباشد")]
        [EmailAddress(ErrorMessage ="فرمت ایمیل وارد شده اشتباه میباشد.")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [CompareRequired("SubmitUser", ErrorMessage = "مقدار {0} الزامی میباشد")]
        [StringLength(100, ErrorMessage = "مقدار {0} حداقل باید {2} باشد", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار آن برابر نیستند")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string RoleID
        {
            get { return Translator.RoleTranslateToID(Role); }
        }

        public string LockoutEnabledGet { get; set; }
        public bool LockoutEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(LockoutEnabledGet))
                    return false;
                else
                    return bool.Parse(LockoutEnabledGet);
            }
        }


    }
    public static class EmployeeMapper
    {
        public static Employee Map(EmployeeViewModel employeeViewModel)
        {
            DateTime? _lockoutEndDateUtc = null;
            if (employeeViewModel.LockoutEnabled)
                _lockoutEndDateUtc = DateTime.Parse("2400-05-05");

            

            return new Employee()
            {
                ID = employeeViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(employeeViewModel.ID),
                FirstName = employeeViewModel.FirstName,
                LastName = employeeViewModel.LastName,
                NationalCode = employeeViewModel.NationalCode,
                EmployeeCode = employeeViewModel.EmployeeCode,
                RoleHelper = employeeViewModel.RoleID,
                Person_ApplicationUser = new ApplicationUser()
                {
                    Email = employeeViewModel.Email,
                    UserName = employeeViewModel.Email,
                    LockoutEnabled = employeeViewModel.LockoutEnabled,
                    LockoutEndDateUtc = _lockoutEndDateUtc
                }
            };
        }
    }
}
