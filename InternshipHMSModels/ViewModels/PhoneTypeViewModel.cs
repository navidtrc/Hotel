using InternshipHMSModels.Core.Entity.Base;
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
    public class PhoneTypeViewModel : ObjectModel
    {

        [Display(Name = "عنوان")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(InternshipHMSLibrary.ResourcesFiles.Resources))]
        public string Title { get; set; }
    }
}
