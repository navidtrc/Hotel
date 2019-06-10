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
    public class RoomViewModel
    {
        public string ID { get; set; }
        public string FacilitiesID { get; set; }
        [Display(Name = "شماره")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public int Number { get; set; }
        [Display(Name = "موقعیت مکانی")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string Location { get; set; }
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        [Display(Name = "نوع اتاق")]
        public string Type { get; set; }
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        [Display(Name = "هتل")]
        public string Hotel { get; set; }

    }
    public static class RoomMapper
    {

        public static Room Map(RoomViewModel roomViewModel, string type, string hotel)
        {
            Guid? _facilitiesId = null;
            if (!string.IsNullOrEmpty(roomViewModel.FacilitiesID))
                _facilitiesId =Guid.Parse(roomViewModel.FacilitiesID);
            return new Room()
            {
                ID = roomViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(roomViewModel.ID),
                Number = roomViewModel.Number,
                Location = roomViewModel.Location,
                RoomTypeID = Guid.Parse(type),
                HotelDataID = Guid.Parse(hotel),
                FacilitiesID =_facilitiesId
            };
        }
    }
}
