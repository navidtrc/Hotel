using InternshipHMSLibrary;
using InternshipHMSService.Core;
using InternshipHMSWeb.Controllers;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d,b7ea9bd0-f597-4abe-8238-a9b94077cba6,3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public ActionResult DetailFellows_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            return Json(_unitOfWork.Customer_List.GetChildByDataSourceResult(request, id));
        }


        public ActionResult Available_Passengers([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_unitOfWork.Passenger_List.GetAvailableCustomers(request));
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.AllEmployee = _unitOfWork.Employee_List.GetAll().Count().ToString();
            ViewBag.AllCustomer = _unitOfWork.Customer_List.GetAll().Count().ToString();
            ViewBag.AllRoom = _unitOfWork.Room_List.GetAll().Count().ToString();
            ViewBag.TodayReserve = _unitOfWork.Reservation_List.GetAll().Where(w => w.FromDate == DateTime.Now.Date).Count().ToString();
            return View();
        }

       
        public string GetPersianCurrentDayOfWeek(string gregorianFirstDay)
        {
            switch (gregorianFirstDay)
            {
                case "0":
                    return "2";
                case "1":
                    return "3";
                case "2":
                    return "4";
                case "3":
                    return "5";
                case "4":
                    return "6";
                case "5":
                    return "7";
                case "6":
                    return "1";
                default:
                    return null;
            }
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


        public JsonResult PersianCurrentWeek()
        {
            List<DataSourceResult> datesWithRooms = new List<DataSourceResult>();
            List<DataSourceResult> checkedRooms = new List<DataSourceResult>();
            List<DataSourceResult> freeRooms = new List<DataSourceResult>();
            DateTime currentDate = DateTime.Now;
            string dayOfWeek = ((int)currentDate.DayOfWeek).ToString();
            string persianDayOfWeek = GetPersianCurrentDayOfWeek(dayOfWeek);
            int persianDayOfWeekInt = int.Parse(GetPersianCurrentDayOfWeek(dayOfWeek));
            DateTime firstDayOfWeek = currentDate.AddDays(-persianDayOfWeekInt + 1);

            for (int i = 0; i < 7; i++)
            {
                datesWithRooms.Add(_unitOfWork.ReservationRooms_List.GetAllByDataSourceResultInDate(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , firstDayOfWeek.AddDays(i).Date));
                checkedRooms.Add(_unitOfWork.Checking_List.GetAllByDataSourceResultInDate(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , firstDayOfWeek.AddDays(i).Date));

                freeRooms.Add(_unitOfWork.Room_List.GetFreeRooms(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , firstDayOfWeek.AddDays(i).Date));

            }
            
            return Json(new
            {
                dateRoom = datesWithRooms,
                freeRoom = freeRooms,
                checkedRoom = checkedRooms
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PersianNextWeek(string firstDay)
        {
            List<DataSourceResult> datesWithRooms = new List<DataSourceResult>();
            List<DataSourceResult> checkedRooms = new List<DataSourceResult>();
            List<DataSourceResult> freeRooms = new List<DataSourceResult>();

            DateTime firstDayOfWeek = PersianDateConvertor.ConvertToGregorian(firstDay).AddDays(7);

            for (int i = 0; i < 7; i++)
            {
                datesWithRooms.Add(_unitOfWork.ReservationRooms_List.GetAllByDataSourceResultInDate(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , firstDayOfWeek.AddDays(i).Date));
                checkedRooms.Add(_unitOfWork.Checking_List.GetAllByDataSourceResultInDate(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , firstDayOfWeek.AddDays(i).Date));
                freeRooms.Add(_unitOfWork.Room_List.GetFreeRooms(new DataSourceRequest() { Page = 1, PageSize = 0 }
               , firstDayOfWeek.AddDays(i).Date));
            }
            return Json(new
            {
                dateRoom = datesWithRooms,
                freeRoom = freeRooms,
                checkedRoom = checkedRooms
            }, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult PersianPreviousWeek(string firstDay)
        {
            List<DataSourceResult> datesWithRooms = new List<DataSourceResult>();
            List<DataSourceResult> checkedRooms = new List<DataSourceResult>();
            List<DataSourceResult> freeRooms = new List<DataSourceResult>();

            DateTime firstDayOfWeek = PersianDateConvertor.ConvertToGregorian(firstDay).AddDays(-7);

            for (int i = 0; i < 7; i++)
            {
                datesWithRooms.Add(_unitOfWork.ReservationRooms_List.GetAllByDataSourceResultInDate(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , firstDayOfWeek.AddDays(i).Date));
                checkedRooms.Add(_unitOfWork.Checking_List.GetAllByDataSourceResultInDate(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , firstDayOfWeek.AddDays(i).Date));
                freeRooms.Add(_unitOfWork.Room_List.GetFreeRooms(new DataSourceRequest() { Page = 1, PageSize = 0 }
               , firstDayOfWeek.AddDays(i).Date));
            }
            return Json(new
            {
                dateRoom = datesWithRooms,
                freeRoom = freeRooms,
                checkedRoom = checkedRooms
            }, JsonRequestBehavior.AllowGet);
        }
    }
}