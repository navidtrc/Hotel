using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using InternshipHMSService.Core;
using InternshipHMSService.Persistance;
using InternshipHMSWeb.Jobs;
using InternshipHMSWeb.JsonWebTokenConfig;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternshipHMSWeb.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        //[HttpPost]
        //public void Upload()
        //{

        //    for (int i = 0; i < Request.Files.Count; i++)
        //    {
        //        var file = Request.Files[i];
        //        var fileName = Path.GetFileName(file.FileName);

        //        byte[] fileData = null;
        //        using (var binaryReader = new BinaryReader(Request.Files[i].InputStream))
        //        {
        //            fileData = binaryReader.ReadBytes(Request.Files[i].ContentLength);
        //        }
        //        // Save fileName as File Name
        //        // Save fileData as File Data

        //    }

        //}


        //[HttpPost]
        //private HttpCookie CreateTokenCookie()
        //{
        //    HttpCookie StudentCookies = new HttpCookie("Token");
        //    StudentCookies.Value = "";
        //    StudentCookies.Expires = DateTime.Now.AddDays(1);
        //    return StudentCookies;
        //}

        //public ActionResult CheckAuthentication()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        Ok();
        //        return Content("Successful");
        //    }
        //    else
        //        Forbidden();
        //    return Content("Login Required");
        //}
        //public ActionResult Fellows_Read([DataSourceRequest]DataSourceRequest request, string ParentID)
        //{
        //    return Json(_unitOfWork.Customer_List.GetAllExceptIdByDataSourceResult(request, ParentID));
        //}

        //[HttpPost]
        //[JwtAuthorize]
        //public ActionResult Create(ReservationViewModel reservation)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        reservation.ParentId = _unitOfWork.Customer_List.GetAll().Where(w => w.Person_ApplicationUser != null)
        //            .FirstOrDefault(w => w.Person_ApplicationUser.Id == reservation.ParentId).ID.ToString();
        //        Reservation rsv = ReservationMapper.Map(reservation);
        //        var result = _unitOfWork.Reservation_List.AddWithRooms(rsv, reservation.SelectedRooms);
        //        if (result)
        //        {
        //            if (reservation.SelectedFellows != null)
        //                _unitOfWork.Customer_List.ConvertCustomersToFellow(reservation.ParentId, reservation.SelectedFellows);
        //            Ok();
        //            Task.Run(() => ReservationJobs.Start(rsv.ID, DateTime.Now.AddDays(1)));
        //            string Number = _unitOfWork.Reservation_List.Find(rsv.ID).Number.ToString();


        //            int days = (rsv.ToDate - rsv.FromDate).Days + 1;
        //            int Price = 0;
        //            Reservation savedRes = _unitOfWork.Reservation_List.Find(rsv.ID);
        //            foreach (var reservationRooms in savedRes.Reservation_ReservationRooms_List)
        //            {
        //                var data = _unitOfWork.RoomPrice_List.GetAll().Where(w => w.RoomID == reservationRooms.RoomID);
        //                if (data != null)
        //                    Price += ((data.Where(w => w.Date == savedRes.FromDate).OrderByDescending(c => c.CreateDate).FirstOrDefault().Price) / 2);
        //            }
        //            Price *= days;
        //            return Json(new { number = Number, price = Price });
        //        }
        //    }
        //    Forbidden();
        //    return Content(GenerateError());
        //}


        //[HttpPost]
        //[JwtAuthorize]
        //public ActionResult CreateFellow(CustomerViewModel customer)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        if (!customer.CheckSubmitUserCustomer)
        //        {
        //            var result = _unitOfWork.Customer_List.Add(CustomerMapper.Map(customer));
        //            if (result.Succeeded)
        //            {
        //                Ok();
        //                return RedirectToAction("Index", "Customer", null);
        //            }
        //        }
        //        if (customer.CheckSubmitUserCustomer)
        //        {
        //            var result = _unitOfWork.Customer_List.AddWithUser(CustomerMapper.Map(customer), customer.Password);
        //            if (result.Succeeded)
        //            {
        //                Ok();
        //                return RedirectToAction("Index", "Customer", null);
        //            }
        //        }
        //    }
        //    Forbidden();
        //    return Content(GenerateError());
        //}
        public ActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
            {
                if (Request.Cookies["token"] != null)
                {
                    var c = new HttpCookie("token");
                    c.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c);
                }
            }
            return View();
        }

        //public ActionResult RoomsInDate_Read([DataSourceRequest]DataSourceRequest request, string FromDate, string ToDate)
        //{
        //    return Json(_unitOfWork.Room_List.GetRoomsForReservationInDate(request, PersianDateConvertor.ConvertToGregorian(FromDate).Date, PersianDateConvertor.ConvertToGregorian(ToDate).Date, null));
        //}
        //public JsonResult Detail(string id)
        //{

        //    Room room = _unitOfWork.Room_List.Find(Guid.Parse(id));
        //    return Json(new
        //    {
        //        ID = room.ID,
        //        Number = room.Number,
        //        Location = room.Location,
        //        Type = room.Room_RoomType.Type,
        //        Hotel = room.Room_HotelData.Name,
        //        FacilitiesID = room.FacilitiesID.ToString(),


        //        SingleBed = room.Room_Facilities.SingleBed.ToString(),
        //        DoubleBed = room.Room_Facilities.DoubleBed.ToString(),
        //        Space = room.Room_Facilities.Space.ToString(),
        //        Capacity = room.Room_Facilities.Capacity.ToString(),
        //        Kitchen = room.Room_Facilities.Kitchen == false ? "0" : "1",
        //        Terrace = room.Room_Facilities.Terrace == false ? "0" : "1",
        //        TV = room.Room_Facilities.TV == false ? "0" : "1",
        //        WiFi = room.Room_Facilities.WiFi == false ? "0" : "1",
        //        SafeBox = room.Room_Facilities.SafeBox == false ? "0" : "1",
        //        GamingConsole = room.Room_Facilities.GamingConsole == false ? "0" : "1"


        //    }, JsonRequestBehavior.AllowGet);
        //}

    }
}