using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InternshipHMSModels.ViewModels
{
    [NotMapped]
    public class RoomImagesViewModel : ObjectModel
    {

        [Display(Name = "نام فایل")]
        public string FileName { get; set; }      
        public HttpPostedFileBase Picture { get; set; }
        public Guid RoomID { get; set; }
    }
}
