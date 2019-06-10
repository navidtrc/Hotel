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
    public class FacilitiesViewModel
    {
        public string ID { get; set; }
        public string RoomID { get; set; }
        [Display(Name = "تخت یک نفره")]
        public string SingleBed { get; set; }
        [Display(Name = "تخت دو نفره")]
        public string DoubleBed { get; set; }
        [Display(Name = "متراژ")]
        public string Space { get; set; }
        [Display(Name = "ظرفیت")]
        public string Capacity { get; set; }
        [Display(Name = "آشپزخونه")]
        public string Kitchen { get; set; }
        [Display(Name = "تراس")]
        public string Terrace { get; set; }
        [Display(Name = "تلویزیون")]
        public string TV { get; set; }
        [Display(Name = "اینترنت")]
        public string WiFi { get; set; }
        [Display(Name = "گاو صندوق")]
        public string SafeBox { get; set; }
        [Display(Name = "کنسول بازی")]
        public string GamingConsole { get; set; }
    }
    public static class FacilitiesMapper
    {
        public static Facilities Map(FacilitiesViewModel facilitiesViewModel)
        {
            return new Facilities()
            {
                ID = facilitiesViewModel.ID == null ? Guid.NewGuid() : Guid.Parse(facilitiesViewModel.ID),
                SingleBed = int.Parse(facilitiesViewModel.SingleBed),
                DoubleBed = int.Parse(facilitiesViewModel.DoubleBed),
                Space = int.Parse(facilitiesViewModel.Space),
                Capacity = int.Parse(facilitiesViewModel.Capacity),
                Kitchen = bool.Parse(facilitiesViewModel.Kitchen),
                Terrace = bool.Parse(facilitiesViewModel.Terrace),
                TV = bool.Parse(facilitiesViewModel.TV),
                WiFi = bool.Parse(facilitiesViewModel.WiFi),
                SafeBox = bool.Parse(facilitiesViewModel.SafeBox),
                GamingConsole = bool.Parse(facilitiesViewModel.GamingConsole)
            };

        }
    }
}
