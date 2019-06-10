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
    public class HotelDataViewModel
    {
        public string ID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string Name { get; set; }
        [Display(Name = "رتبه")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string Rate { get; set; }
    }
    public static class HotelDataMapper
    {
        public static HotelData Map(HotelDataViewModel hotelDataViewModel)
        {
            return new HotelData()
            {
                ID = hotelDataViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(hotelDataViewModel.ID),
                Name = hotelDataViewModel.Name,
                Rate = int.Parse(hotelDataViewModel.Rate)
            };
        }
    }
}
