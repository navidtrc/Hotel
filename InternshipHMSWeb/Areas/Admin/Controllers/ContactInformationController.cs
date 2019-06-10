using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using InternshipHMSService.Core;
using InternshipHMSWeb.Controllers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d,b7ea9bd0-f597-4abe-8238-a9b94077cba6,3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")]
    public class ContactInformationController : BaseController
    {
        public ContactInformationController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        // GET: Admin/ContactInformation
        public ActionResult Index()
        {
            return View();
        }


    }
}