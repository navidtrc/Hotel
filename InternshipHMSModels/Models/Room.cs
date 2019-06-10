using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class Room : ObjectModel
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public Guid HotelDataID { get; set; }
        [ForeignKey("HotelDataID")]
        public virtual HotelData Room_HotelData { get; set; }
        public Guid RoomTypeID { get; set; }
        [ForeignKey("RoomTypeID")]
        public virtual RoomType Room_RoomType { get; set; }
        public Guid? FacilitiesID { get; set; }
        [ForeignKey("FacilitiesID")]
        public virtual Facilities Room_Facilities { get; set; }
        public virtual ICollection<ReservationRooms> Room_ReservationRooms_List { get; set; }
        public virtual ICollection<Checking> Room_Checking_List { get; set; }
        public virtual ICollection<RoomPrice> Room_RoomPrice_List { get; set; }
        public virtual ICollection<RoomImages> Room_RoomImages_List { get; set; }

    }
   
}
