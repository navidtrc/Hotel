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
using InternshipHMSLibrary;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    public class CheckingController : BaseController
    {
        public CheckingController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checking_List_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Checking_List.GetAllByDataSourceResult(request));
        }

        public ActionResult Rooms_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Room_List.GetAllByDataSourceResult(request));
        }

        public ActionResult RoomsInDate_Read([DataSourceRequest]DataSourceRequest request, string FromDate, string ToDate,string ReservationId)
        {
            return Json(_unitOfWork.Room_List.GetRoomsForReservationInDate(request, PersianDateConvertor.ConvertToGregorian(FromDate).Date, PersianDateConvertor.ConvertToGregorian(ToDate).Date,ReservationId));
        }


        public ActionResult Reservation_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Reservation_List.GetAllByDataSourceResult(request));
        }
        public ActionResult Passengers_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Customer_List.GetAllByDataSourceResult(request));
        }


        public JsonResult Detail(string id)
        {
            InternshipHMSModels.Models.Checking checking = _unitOfWork.Checking_List.Find(Guid.Parse(id));

            return Json(new
            {
                ID = checking.ID.ToString(),
                FromDate = PersianDateConvertor.ConvertToPersianDate(checking.FromDate, true),
                ToDate = PersianDateConvertor.ConvertToPersianDate(checking.ToDate, true),
                RoomID = checking.Checking_Room.ID.ToString(),
                RoomNumber =  checking.Checking_Room.Number.ToString(),
                ReservationID = checking.Checking_Reservation == null ? "" : checking.Checking_Reservation.ID.ToString(),
                ReservationNumber = checking.Checking_Reservation == null ? "" : checking.Checking_Reservation.Number.ToString(),
                Passengers = _unitOfWork.Passenger_List.GetRoomPassengersByDataSourceResult(new DataSourceRequest() { Page = 1, PageSize = 0 }, checking.ID.ToString()),

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(CheckingViewModel checking)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Checking_List.AddWithPassengers(CheckingMapper.Map(checking), checking.Passengers);
                Ok();
                return RedirectToAction("Index", "checking", null);
            }
            Forbidden();
            return Content(GenerateError());
        }

        [HttpPost]
        public ActionResult Edit(CheckingViewModel checking)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Checking_List.EditWithPassengers(CheckingMapper.Map(checking), checking.Passengers);
                Ok();
                return RedirectToAction("Index", "checking", null);
            }
            Forbidden();
            return Content(GenerateError());
        }
    }
}
