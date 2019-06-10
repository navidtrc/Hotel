using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using InternshipHMSService.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace InternshipHMSWeb.Controllers
{

    public abstract class ApiBaseController : ApiController
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ApiBaseController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
       
    }
}