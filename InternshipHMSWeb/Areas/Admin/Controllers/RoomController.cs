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
using System.Globalization;
using InternshipHMSLibrary;

namespace InternshipHMSWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "066e5a40-54ca-4c02-b7e3-ac3f6163e25d,b7ea9bd0-f597-4abe-8238-a9b94077cba6,3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")]
    public class RoomController : BaseController
    {
        public RoomController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }


        public string GetPersianCurrentDayOfWeek(string gregorianFirstDay)
        {
            switch (gregorianFirstDay)
            {
                case "0":
                    return "یکشنبه";
                case "1":
                    return "دوشنبه";
                case "2":
                    return "سه شنبه";
                case "3":
                    return "چهارشنبه";
                case "4":
                    return "پنج شنبه";
                case "5":
                    return "جمعه";
                case "6":
                    return "شنبه";
                default:
                    return null;
            }
        }
        public string GetPersianMonthName(string monthNumber)
        {
            switch (monthNumber)
            {

                case "1":
                    return "فروردین";
                case "2":
                    return "اردیبهشت";
                case "3":
                    return "خرداد";
                case "4":
                    return "تیر";
                case "5":
                    return "مرداد";
                case "6":
                    return "شهریور";
                case "7":
                    return "مهر";
                case "8":
                    return "آبان";
                case "9":
                    return "آذر";
                case "10":
                    return "دی";
                case "11":
                    return "بهمن";
                case "12":
                    return "اسفند";
                default:
                    return null;
            }
        }

        public JsonResult DateTimeNow()
        {
            return Json(new { CurrentDate = DateTime.Now.Date.ToShortDateString() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RoomPriceCalendar(string roomID, string requestDate)
        {

            DateTime currentDate = DateTime.Now;
            if (requestDate != null)
                currentDate = DateTime.Parse(requestDate);
            PersianCalendar pc = new PersianCalendar();
            int persianDay = pc.GetDayOfMonth(currentDate);
            DateTime firstDayOfMonth = currentDate.AddDays(-persianDay + 1);

            #region KabiseList
            int nearkabise = 1399;
            List<int> kabise = new List<int>();
            for (int i = 0; i < 201; i++)
            {
                kabise.Add(nearkabise + (i * 4));
            }
            for (int i = 1; i <= 201; i++)
            {
                kabise.Add(nearkabise - (i * 4));
            }

            #endregion

            #region PersianMonthRange
            int MonthRange;
            if (pc.GetMonth(currentDate) == 12)
            {
                if (kabise.Contains(pc.GetYear(currentDate)))
                    MonthRange = 30;
                else
                    MonthRange = 29;
            }
            else if (pc.GetMonth(currentDate) < 7)
                MonthRange = 31;
            else
                MonthRange = 30;
            #endregion

            #region HeaderDetail            
            List<string> headerWeek = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                headerWeek.Add(GetPersianCurrentDayOfWeek(((int)firstDayOfMonth.AddDays(i).DayOfWeek).ToString()));
            }
            string MonthYearTitle = $"{GetPersianMonthName(pc.GetMonth(currentDate).ToString())} {pc.GetYear(currentDate)}";
            MonthYearTitle = MonthYearTitle.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
            #endregion

            #region CalendarDetail

            List<DataSourceResult> roomPrices = new List<DataSourceResult>();
            for (int i = 0; i < MonthRange; i++)
            {
                roomPrices.Add(_unitOfWork.RoomPrice_List.GetForRoomWithDateByDataSourceResult(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , Guid.Parse(roomID)
                , firstDayOfMonth.AddDays(i).Date));
            }
            return Json(new
            {
                HeaderTitle = MonthYearTitle,
                HeaderWeek = headerWeek,
                CalendarPrices = roomPrices,
                CurrentDay = currentDate.Date.ToShortDateString()
            }, JsonRequestBehavior.AllowGet);
            #endregion

        }
        public JsonResult RoomPriceCalendarNextMonth(string roomID, string requestDate)
        {


            DateTime currentDate = DateTime.Parse(requestDate).AddMonths(1);
            PersianCalendar pc = new PersianCalendar();
            int persianDay = pc.GetDayOfMonth(currentDate);
            DateTime firstDayOfMonth = currentDate.AddDays(-persianDay + 1);

            #region KabiseList
            int nearkabise = 1399;
            List<int> kabise = new List<int>();
            for (int i = 0; i < 201; i++)
            {
                kabise.Add(nearkabise + (i * 4));
            }
            for (int i = 1; i <= 201; i++)
            {
                kabise.Add(nearkabise - (i * 4));
            }

            #endregion

            #region PersianMonthRange
            int MonthRange;
            if (pc.GetMonth(currentDate) == 12)
            {
                if (kabise.Contains(pc.GetYear(currentDate)))
                    MonthRange = 30;
                else
                    MonthRange = 29;
            }
            else if (pc.GetMonth(currentDate) < 7)
                MonthRange = 31;
            else
                MonthRange = 30;
            #endregion

            #region HeaderDetail            
            List<string> headerWeek = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                headerWeek.Add(GetPersianCurrentDayOfWeek(((int)firstDayOfMonth.AddDays(i).DayOfWeek).ToString()));
            }
            string MonthYearTitle = $"{GetPersianMonthName(pc.GetMonth(currentDate).ToString())} {pc.GetYear(currentDate)}";
            MonthYearTitle = MonthYearTitle.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
            #endregion


            List<DataSourceResult> roomPrices = new List<DataSourceResult>();


            for (int i = 0; i < MonthRange; i++)
            {
                roomPrices.Add(_unitOfWork.RoomPrice_List.GetForRoomWithDateByDataSourceResult(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , Guid.Parse(roomID)
                , firstDayOfMonth.AddDays(i).Date));
            }


            return Json(new
            {
                HeaderTitle = MonthYearTitle,
                HeaderWeek = headerWeek,
                CalendarPrices = roomPrices,
                CurrentDay = currentDate.Date.ToShortDateString()
            }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult RoomPriceCalendarPrevoiusMonth(string roomID, string requestDate)
        {


            DateTime currentDate = DateTime.Parse(requestDate).AddMonths(-1);
            PersianCalendar pc = new PersianCalendar();
            int persianDay = pc.GetDayOfMonth(currentDate);
            DateTime firstDayOfMonth = currentDate.AddDays(-persianDay + 1);

            #region KabiseList
            int nearkabise = 1399;
            List<int> kabise = new List<int>();
            for (int i = 0; i < 201; i++)
            {
                kabise.Add(nearkabise + (i * 4));
            }
            for (int i = 1; i <= 201; i++)
            {
                kabise.Add(nearkabise - (i * 4));
            }

            #endregion

            #region PersianMonthRange
            int MonthRange;
            if (pc.GetMonth(currentDate) == 12)
            {
                if (kabise.Contains(pc.GetYear(currentDate)))
                    MonthRange = 30;
                else
                    MonthRange = 29;
            }
            else if (pc.GetMonth(currentDate) < 7)
                MonthRange = 31;
            else
                MonthRange = 30;
            #endregion

            #region HeaderDetail            
            List<string> headerWeek = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                headerWeek.Add(GetPersianCurrentDayOfWeek(((int)firstDayOfMonth.AddDays(i).DayOfWeek).ToString()));
            }
            string MonthYearTitle = $"{GetPersianMonthName(pc.GetMonth(currentDate).ToString())} {pc.GetYear(currentDate)}";
            MonthYearTitle = MonthYearTitle.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
            #endregion


            List<DataSourceResult> roomPrices = new List<DataSourceResult>();


            for (int i = 0; i < MonthRange; i++)
            {
                roomPrices.Add(_unitOfWork.RoomPrice_List.GetForRoomWithDateByDataSourceResult(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , Guid.Parse(roomID)
                , firstDayOfMonth.AddDays(i).Date));
            }


            return Json(new
            {
                HeaderTitle = MonthYearTitle,
                HeaderWeek = headerWeek,
                CalendarPrices = roomPrices,
                CurrentDay = currentDate.Date.ToShortDateString()
            }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult RoomPriceCalendarSearch(string roomID, string requestDate)
        {


            DateTime currentDate = PersianDateConvertor.ConvertToGregorian(requestDate);
            PersianCalendar pc = new PersianCalendar();
            DateTime firstDayOfMonth = currentDate;

            #region KabiseList
            int nearkabise = 1399;
            List<int> kabise = new List<int>();
            for (int i = 0; i < 201; i++)
            {
                kabise.Add(nearkabise + (i * 4));
            }
            for (int i = 1; i <= 201; i++)
            {
                kabise.Add(nearkabise - (i * 4));
            }

            #endregion

            #region PersianMonthRange
            int MonthRange;
            if (pc.GetMonth(currentDate) == 12)
            {
                if (kabise.Contains(pc.GetYear(currentDate)))
                    MonthRange = 30;
                else
                    MonthRange = 29;
            }
            else if (pc.GetMonth(currentDate) < 7)
                MonthRange = 31;
            else
                MonthRange = 30;
            #endregion

            #region HeaderDetail            
            List<string> headerWeek = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                headerWeek.Add(GetPersianCurrentDayOfWeek(((int)firstDayOfMonth.AddDays(i).DayOfWeek).ToString()));
            }
            string MonthYearTitle = $"{GetPersianMonthName(pc.GetMonth(currentDate).ToString())} {pc.GetYear(currentDate)}";
            MonthYearTitle = MonthYearTitle.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
            #endregion


            List<DataSourceResult> roomPrices = new List<DataSourceResult>();


            for (int i = 0; i < MonthRange; i++)
            {
                roomPrices.Add(_unitOfWork.RoomPrice_List.GetForRoomWithDateByDataSourceResult(new DataSourceRequest() { Page = 1, PageSize = 0 }
                , Guid.Parse(roomID)
                , firstDayOfMonth.AddDays(i).Date));
            }


            return Json(new
            {
                HeaderTitle = MonthYearTitle,
                HeaderWeek = headerWeek,
                CalendarPrices = roomPrices,
                CurrentDay = currentDate.Date.ToShortDateString()
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HotelData_List_Read([DataSourceRequest]DataSourceRequest request)
        {

            return Json(_unitOfWork.HotelData_List.GetAllByDataSourceResult(request));
        }
        public ActionResult RoomType_List_Read([DataSourceRequest]DataSourceRequest request)
        {

            return Json(_unitOfWork.RoomType_List.GetAllByDataSourceResult(request));
        }
        public ActionResult Room_List_Read([DataSourceRequest]DataSourceRequest request)
        {

            return Json(_unitOfWork.Room_List.GetAllByDataSourceResult(request));
        }
        [HttpPost]
        public ActionResult Create(RoomViewModel room)
        {

            if (ModelState.IsValid)
            {
                Room instance = RoomMapper.Map(room,
                    _unitOfWork.RoomType_List.GetAll().FirstOrDefault(f => f.Type == room.Type).ID.ToString(),
                    _unitOfWork.HotelData_List.GetAll().FirstOrDefault(f => f.Name == room.Hotel).ID.ToString());
                _unitOfWork.Room_List.Add(instance);
                Ok();
                return Json(new { RoomID = instance.ID.ToString() });
            }
            Forbidden();
            return Content(GenerateError());
        }


        [HttpPost]
        public ActionResult FacilitiesCreate(FacilitiesViewModel facilities)
        {

            if (ModelState.IsValid)
            {
                Facilities instance = FacilitiesMapper.Map(facilities);
                _unitOfWork.Facilities_List.Add(instance);
                _unitOfWork.Room_List.Find(Guid.Parse(facilities.RoomID)).FacilitiesID = instance.ID;
                Ok();
                return RedirectToAction("Index", "Room", null);
            }
            Forbidden();
            return Content(GenerateError());
        }

        [HttpPost]
        public ActionResult FacilitiesEdit(FacilitiesViewModel facilities)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Facilities_List.Update(FacilitiesMapper.Map(facilities));
                Ok();
                return RedirectToAction("Index", "Room", null);
            }
            Forbidden();
            return Content(GenerateError());
        }


        [HttpPost]
        public ActionResult PriceCreate(RoomPriceViewModel room)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(room.Date))
                {
                    DateTime seed = PersianDateConvertor.ConvertToGregorian(room.FromDate).Date;
                    DateTime finalDay = PersianDateConvertor.ConvertToGregorian(room.ToDate).AddDays(1).Date;
                    do
                    {
                        _unitOfWork.RoomPrice_List.Add(RoomPriceMapper.Map(room, seed));
                        seed = seed.AddDays(1);
                    } while (seed != finalDay);
                }
                else
                {
                    _unitOfWork.RoomPrice_List.Add(RoomPriceMapper.Map(room, DateTime.Parse(room.Date)));
                }
                Ok();
                return RedirectToAction("Index", "Room", null);
            }
            return Content(GenerateError());
        }

        public JsonResult Detail(string id)
        {

            Room room = _unitOfWork.Room_List.Find(Guid.Parse(id));
            return Json(new
            {
                ID = room.ID,
                Number = room.Number,
                Location = room.Location,
                Type = room.Room_RoomType.Type,
                Hotel = room.Room_HotelData.Name,
                FacilitiesID = room.FacilitiesID.ToString(),


                SingleBed =  room.Room_Facilities.SingleBed.ToString(),
                DoubleBed= room.Room_Facilities.DoubleBed.ToString(),
                Space = room.Room_Facilities.Space.ToString(),
                Capacity = room.Room_Facilities.Capacity.ToString(),
                Kitchen = room.Room_Facilities.Kitchen == false ? "0" : "1",
                Terrace = room.Room_Facilities.Terrace == false ? "0" : "1",
                TV = room.Room_Facilities.TV == false ? "0" : "1",
                WiFi = room.Room_Facilities.WiFi == false ? "0" : "1",
                SafeBox = room.Room_Facilities.SafeBox == false ? "0" : "1",
                GamingConsole = room.Room_Facilities.GamingConsole == false ? "0" : "1"


            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Room_List.Update(RoomMapper.Map(room,
                   _unitOfWork.RoomType_List.GetAll().FirstOrDefault(f => f.Type == room.Type).ID.ToString(),
                   _unitOfWork.HotelData_List.GetAll().FirstOrDefault(f => f.Name == room.Hotel).ID.ToString()));
                Ok();
                return RedirectToAction("Index", "Room", null);
            }

            Forbidden();
            return Content(GenerateError());

        }



        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Room_List.Delete(Guid.Parse(id));
                Ok();
                return RedirectToAction("Index", "Room", null);
            }

            Forbidden();
            return Content(GenerateError());

        }

    }
}
