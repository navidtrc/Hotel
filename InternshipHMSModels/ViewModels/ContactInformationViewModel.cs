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
    [NotMapped]
    public class ContactInformationViewModel
    {
        public string ID { get; set; }
        public string Type { get; set; }

        [Display(Name = "نوع تلفن")]
        public string PhoneTypeID { get; set; }
        [Display(Name = "شماره تماس")]
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "کشور")]
        public string Country { get; set; }
        [Display(Name = "شهر")]
        public string City { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        public string PersonID { get; set; }
    }
    public static class ContactInfotmationMapper
    {
        public static ContactInformation Map(ContactInformationViewModel contactInformationViewModel)
        {

            return new ContactInformation()
            {
                ID = contactInformationViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(contactInformationViewModel.ID),
                Type = contactInformationViewModel.Type,
                PhoneNumber = contactInformationViewModel.PhoneNumber,
                Country = contactInformationViewModel.Country,
                City = contactInformationViewModel.City,
                Address = contactInformationViewModel.Address,
                PersonID = Guid.Parse(contactInformationViewModel.PersonID)
            };
        }
    }
}
