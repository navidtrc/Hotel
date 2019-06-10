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
    public class RoomPriceViewModel
    {
        public string ID { get; set; }
        public string RoomID { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "مقدار {0} الزامی است")]
        public string Price { get; set; }
        [Display(Name = "تاریخ شروع قیمت")]
        public string FromDate { get; set; }
        [Display(Name = "تاریخ پایان قیمت")]
        public string ToDate { get; set; }
        public string Date { get; set; }
    }
    public static class RoomPriceMapper
    {
        public static RoomPrice Map(RoomPriceViewModel roomPriceViewModel,DateTime date)
        {
            return new RoomPrice()
            {
                ID = roomPriceViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(roomPriceViewModel.ID),
                RoomID = Guid.Parse(roomPriceViewModel.RoomID),
                Price = int.Parse(roomPriceViewModel.Price),
                Date = date
            };
        }
    }
}
