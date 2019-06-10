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
    [NotMapped]
    public class ReservationViewModel
    {
        public string ID { get; set; }
        [Display(Name = "نوع رزرو")]
        public string State { get; set; }
        public string Number { get; set; }
        [Display(Name = "از تاریخ")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string FromDate { get; set; }
        [Display(Name = "تا تاریخ")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string ToDate { get; set; }
        [Display(Name = "مسافر")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string ParentId { get; set; }
        public string[] SelectedFellows { get; set; }
        [Display(Name = "اتاق ها")]
        [Required(ErrorMessage = "اتاقی برای رزرو انتخاب نشده")]
        public string[] SelectedRooms { get; set; }

    }
    public static class ReservationMapper
    {
        public static Reservation Map(ReservationViewModel reservationViewModel)
        {
            string GetStateID(string val)
            {
                switch (val)
                {
                    case "0":
                        return "4c825c2b-ad88-48d5-af69-277d97351651";
                    case "1":
                        return "4c825c2b-ad88-48d5-af69-296d97351441";
                    case "2":
                        return "4c825c2b-ad88-48d5-af69-277d97396441";
                    case "3":
                        return "4c825c2b-ad46-48d5-af69-277d97396441";
                    default:
                        return "4c825c2b-ad88-48d5-af69-277d97351651";
                }
            }

            Guid _reservationState = Guid.Parse("4c825c2b-ad88-48d5-af69-277d97351651");
            if (!string.IsNullOrEmpty(reservationViewModel.State))
                _reservationState = Guid.Parse(GetStateID(reservationViewModel.State));

            return new Reservation()
            {


                ID = reservationViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(reservationViewModel.ID),
                ReservationStateID = _reservationState,
                CustomerID = Guid.Parse(reservationViewModel.ParentId),
                FromDate = PersianDateConvertor.ConvertToGregorian(reservationViewModel.FromDate),
                ToDate = PersianDateConvertor.ConvertToGregorian(reservationViewModel.ToDate)

            };
        }
    }
}
