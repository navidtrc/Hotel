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
using System.Web.Mvc;

namespace InternshipHMSWeb.Controllers
{

    public abstract class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        public virtual void Ok()
        {
            _unitOfWork.Complete();
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        public virtual void Forbidden()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
        }
        protected virtual string GenerateError()
        {
            string data = "";
            foreach (ModelState item in ModelState.Values)
            {
                foreach (ModelError error in item.Errors)
                {
                    data = error.ErrorMessage;
                }
                if (data != "")
                    return data;
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return data;
        }

        [HttpPost]
        public virtual ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public virtual ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }



        public virtual ActionResult ContactInformations_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            return Json(_unitOfWork.Employee_List.GetContactInformation(request, Guid.Parse(id)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContactInformations_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ContactInformationViewModel> contactinformations, string personId)
        {
            if (!ModelState.IsValid)
            {
                Forbidden();
                return View();
            }
            var entities = new List<ContactInformation>();
            foreach (ContactInformationViewModel item in contactinformations)
            {
                item.PersonID = personId;
                _unitOfWork.ContactInformation_List.Add(ContactInfotmationMapper.Map(item));
                entities.Add(ContactInfotmationMapper.Map(item));
            }
            Ok();
            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult ContactInformations_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ContactInformationViewModel> contactinformations, string personId)
        {
            if (!ModelState.IsValid)
            {
                Forbidden();
                return View();
            }
            var entities = new List<ContactInformation>();
            foreach (ContactInformationViewModel item in contactinformations)
            {
                item.PersonID = personId;
                _unitOfWork.ContactInformation_List.Update(ContactInfotmationMapper.Map(item));
                entities.Add(ContactInfotmationMapper.Map(item));
            }
            Ok();
            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult ContactInformations_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ContactInformationViewModel> contactinformations, string personId)
        {
            if (!ModelState.IsValid)
            {
                Forbidden();
                return View();
            }
            var entities = new List<ContactInformation>();
            foreach (ContactInformationViewModel item in contactinformations)
            {
                item.PersonID = personId;
                _unitOfWork.ContactInformation_List.Delete(Guid.Parse(item.ID));
                entities.Add(ContactInfotmationMapper.Map(item));
            }
            Ok();
            return Json(entities.ToDataSourceResult(request, ModelState));
        }


        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
       
    }
}