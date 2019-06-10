using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.ViewModels
{
    public class CheckingViewModel
    {
        public string ID { get; set; }
        [Display(Name = "تاریخ ورود")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string FromDate { get; set; }
        [Display(Name = "تاریخ خروج")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string ToDate { get; set; }
        [Display(Name = "اتاق")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string RoomID { get; set; }
        [Display(Name = "رزرو")]
        public string ReservationID { get; set; }
        public string[] Passengers { get; set; }
    }
    public static class CheckingMapper
    {
        public static Checking Map(CheckingViewModel checkingViewModel)
        {
            Guid? _reservationId = null;
            if (!string.IsNullOrEmpty(checkingViewModel.ReservationID))
                _reservationId = Guid.Parse(checkingViewModel.ReservationID);
            return new Checking()
            {
                ID = checkingViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(checkingViewModel.ID),
                FromDate = PersianDateConvertor.ConvertToGregorian(checkingViewModel.FromDate),
                ToDate = PersianDateConvertor.ConvertToGregorian(checkingViewModel.ToDate),
                RoomID = Guid.Parse(checkingViewModel.RoomID),
                ReservationID = _reservationId
            };
        }
    }
}
