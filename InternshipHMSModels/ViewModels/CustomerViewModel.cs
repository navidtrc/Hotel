using CustomMVCPackage.CustomAnnotation;
using InternshipHMSLibrary;
using InternshipHMSModels.Core.Entity.Base;
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
    public enum CustomerEditState
    {
        UpdateCustomerAndUser,
        UpdateCustomerCreateUser,
        UpdateCustomer
    }
    [NotMapped]
    public class CustomerViewModel
    {

        public string ID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string NationalCode { get; set; }
        //[Display(Name = "شماره مشتری")]
        //[Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(InternshipHMSLibrary.ResourcesFiles.Resources))]
        //public string CustomerCode { get; set; }
        [Display(Name = "تولد")]
        public string Birthdate { get; set; }
        [Display(Name = "ملیت")]
        public string Nationality { get; set; }
        [Display(Name = "محل سکونت")]
        public string LivingPlace { get; set; }
        [Display(Name = "شماره پاسپورت")]
        public string PassportNumber { get; set; }
        public string ParentID { get; set; }

        [CompareRequired("SubmitUser", ErrorMessage = "مقدار {0} الزامی میباشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده اشتباه میباشد.")]
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

        public string SubmitUser { get; set; }
        public bool CheckSubmitUserCustomer
        {
            get
            {
                return string.IsNullOrEmpty(SubmitUser) ? false : bool.Parse(SubmitUser);
            }
        }

        public string CreateUser { get; set; }
        public bool CreateUserForCustomer
        {
            get
            {
                return string.IsNullOrEmpty(CreateUser) ? false : bool.Parse(CreateUser);
            }
        }

        public string EditModeStr { get; set; }

        public CustomerEditState EditMode
        {
            get
            {
                switch (EditModeStr)
                {
                    case "1":
                        return CustomerEditState.UpdateCustomerCreateUser;
                    case "3":
                        return CustomerEditState.UpdateCustomerAndUser;
                    case "4":
                        return CustomerEditState.UpdateCustomer;
                    default:
                        return CustomerEditState.UpdateCustomer;
                }
            }
        }


    }
    public static class CustomerMapper
    {
        public static Customer Map(CustomerViewModel customerViewModel)
        {

            DateTime? _lockoutEndDateUtc = null;
            if (customerViewModel.LockoutEnabled)
                _lockoutEndDateUtc = DateTime.Parse("2400-05-05");
            DateTime? _birthdate = null;
            if (!string.IsNullOrEmpty(customerViewModel.Birthdate) || !string.IsNullOrWhiteSpace(customerViewModel.Birthdate))
                _birthdate = PersianDateConvertor.ConvertToGregorian(customerViewModel.Birthdate);
            Guid? _parentId = null;
            if (!string.IsNullOrEmpty(customerViewModel.ParentID) || !string.IsNullOrWhiteSpace(customerViewModel.ParentID))
                _parentId = Guid.Parse(customerViewModel.ParentID);
            return new Customer()
            {
                ID = customerViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(customerViewModel.ID),
                FirstName = customerViewModel.FirstName,
                LastName = customerViewModel.LastName,
                NationalCode = customerViewModel.NationalCode,
                Birthdate = _birthdate,
                Nationality = customerViewModel.Nationality,
                LivingPlace = customerViewModel.LivingPlace,
                PassportNumber = customerViewModel.PassportNumber,
                ParentID = _parentId,
                Person_ApplicationUser = new ApplicationUser()
                {
                    Email = customerViewModel.Email,
                    UserName = customerViewModel.Email,
                    LockoutEnabled = customerViewModel.LockoutEnabled,
                    LockoutEndDateUtc = _lockoutEndDateUtc
                }

            };
        }
    }
}
