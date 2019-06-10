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
    public class RoomTypeViewModel 
    {
        public string ID { get; set; }
        [Display(Name = "نوع اتاق")]
        [Required(ErrorMessage ="مقدار {0} الزامی است")]
        public string Type { get; set; }
    }
    public static class RoomTypeMapper
    {
        public static RoomType Map(RoomTypeViewModel roomTypeViewModel)
        {
            return new RoomType()
            {
                ID = roomTypeViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(roomTypeViewModel.ID),
                Type = roomTypeViewModel.Type
            };
        }
    }
}
