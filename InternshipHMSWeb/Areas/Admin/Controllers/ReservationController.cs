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
using InternshipHMSWeb.Hubs;
using Microsoft.AspNet.SignalR;
using InternshipHMSWeb.Jobs;
using System.Threading.Tasks;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d,b7ea9bd0-f597-4abe-8238-a9b94077cba6,3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")]
    public class ReservationController : BaseController
    {
        public ReservationController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Customer_List.GetAllByDataSourceResult(request));
        }
        public ActionResult Fellows_Read([DataSourceRequest]DataSourceRequest request, string ParentID)
        {
            return Json(_unitOfWork.Customer_List.GetAllExceptIdByDataSourceResult(request, ParentID));
        }
        public ActionResult Rooms_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Room_List.GetAllByDataSourceResult(request));
        }

        public ActionResult RoomsInDate_Read([DataSourceRequest]DataSourceRequest request, string FromDate, string ToDate)
        {
            return Json(_unitOfWork.Room_List.GetRoomsForReservationInDate(request,PersianDateConvertor.ConvertToGregorian(FromDate).Date,PersianDateConvertor.ConvertToGregorian(ToDate).Date,null));
        }

        public ActionResult Reservations_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Reservation_List.GetAllByDataSourceResult(request));
        }

        public ActionResult DetailFellows_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            return Json(_unitOfWork.Customer_List.GetChildByDataSourceResult(request, id));
        }


        public ActionResult ReservationRooms_Read([DataSourceRequest]DataSourceRequest request,string roomId)
        {

            return Json(_unitOfWork.ReservationRooms_List.GetRoomsForReservationByDataSourceResult(request, roomId));
        }


        [HttpPost]
        public ActionResult Create(ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {

                Reservation reservationInstance = ReservationMapper.Map(reservation);
                var result = _unitOfWork.Reservation_List.AddWithRooms(reservationInstance, reservation.SelectedRooms);
                if (result)
                {
                    if (reservation.SelectedFellows != null)
                        _unitOfWork.Customer_List.ConvertCustomersToFellow(reservation.ParentId, reservation.SelectedFellows);
                    Ok();
                    Task.Run(()=> ReservationJobs.Start(reservationInstance.ID, DateTime.Now.AddDays(1))); // Quartz
                    return RedirectToAction("Index", "Reservation", null);
                }
            }
            Forbidden();
            return Content(GenerateError());
        }

        public JsonResult Detail(string id)
        {
            InternshipHMSModels.Models.Reservation reserve = _unitOfWork.Reservation_List.Find(Guid.Parse(id));

            return Json(new
            {
                ID = reserve.ID.ToString(),
                Number = reserve.Number.ToString(),
                CustomerID = reserve.Reservation_Customer.ID.ToString(),
                CustomerFullName = reserve.Reservation_Customer.FullName.ToString(),
                State = Translator.ReservationStateToPersian(reserve.Reservation_ReservationState.Title.ToString()),
                StateID = reserve.Reservation_ReservationState.Title,
                FromDate = PersianDateConvertor.ConvertToPersianDate(reserve.FromDate, true),
                ToDate = PersianDateConvertor.ConvertToPersianDate(reserve.ToDate, true),
                Fellows = _unitOfWork.Customer_List.GetChildByDataSourceResult(new DataSourceRequest() { Page = 1, PageSize = 0 }, reserve.Reservation_Customer.ID.ToString()),
                Rooms = _unitOfWork.ReservationRooms_List.GetRoomsForReservationByDataSourceResult(new DataSourceRequest() { Page = 1, PageSize = 0 }, id)

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                var result = _unitOfWork.Reservation_List.UpdateWithRooms(ReservationMapper.Map(reservation), reservation.SelectedRooms);

                if (result)
                {
                    if (reservation.SelectedFellows != null)
                        _unitOfWork.Customer_List.UpdateFellows(reservation.ParentId, reservation.SelectedFellows);
                    Ok();
                    Reservation reservationSignalR = _unitOfWork.Reservation_List.Find(Guid.Parse(reservation.ID));
                    var hub = new dashboardHub();
                    hub.Refresh(
                        reservationSignalR.ID.ToString(),
                        Translator.ReservationStateToPersian(reservationSignalR.Reservation_ReservationState.Title.ToString()),
                        reservationSignalR.Number.ToString(),
                        PersianDateConvertor.ConvertToPersianDate(reservationSignalR.FromDate,true),
                        PersianDateConvertor.ConvertToPersianDate(reservationSignalR.ToDate,true)
                        );
                    Response.Redirect("/Admin/Home/Index");
                    return RedirectToAction("Index", "Reservation", null);
                }
            }
            Forbidden();
            return Content(GenerateError());
        }

    }
}
