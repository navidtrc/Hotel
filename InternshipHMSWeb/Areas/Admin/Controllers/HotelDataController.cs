using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using InternshipHMSModels.Models;
using InternshipHMSWeb.Controllers;
using InternshipHMSService.Core;
using InternshipHMSModels.ViewModels;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d,b7ea9bd0-f597-4abe-8238-a9b94077cba6,3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")]
    public class HotelDataController : BaseController
    {
        public HotelDataController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HotelData_List_Read([DataSourceRequest]DataSourceRequest request)
        {

            return Json(_unitOfWork.HotelData_List.GetAllByDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HotelData_List_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<HotelDataViewModel> hoteldata_list)
        {
            if (hoteldata_list != null && ModelState.IsValid)
            {
                foreach (HotelDataViewModel hotelData in hoteldata_list)
                {
                    _unitOfWork.HotelData_List.Add(HotelDataMapper.Map(hotelData));
                }
                Ok();
                return RedirectToAction("Index", "HotelData", null);
            }
            Forbidden();
            return Content(GenerateError());
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HotelData_List_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<HotelDataViewModel> hoteldata_list)
        {
            if (hoteldata_list != null && ModelState.IsValid)
            {
                foreach (HotelDataViewModel hotelData in hoteldata_list)
                {
                    _unitOfWork.HotelData_List.Update(HotelDataMapper.Map(hotelData));
                }
                Ok();
                return RedirectToAction("Index", "HotelData", null);
            }
            Forbidden();
            return Content(GenerateError());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HotelData_List_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<HotelDataViewModel> hoteldata_list)
        {

            if (ModelState.IsValid)
            {
                foreach (HotelDataViewModel hotelData in hoteldata_list)
                {
                    _unitOfWork.HotelData_List.Delete(Guid.Parse(hotelData.ID));
                }
                Ok();
                return RedirectToAction("Index", "HotelData", null);
            }
            Forbidden();
            return Content(GenerateError());
        }


    }
}
