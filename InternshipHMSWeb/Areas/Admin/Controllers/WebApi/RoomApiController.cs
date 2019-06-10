using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using InternshipHMSService.Core;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InternshipHMSWeb.Areas.Admin.Controllers.WebApi
{
    public class RoomApiController : ApiController
    {
        protected readonly IUnitOfWork _unitOfWork;
        public RoomApiController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }


        [HttpGet]
        public IHttpActionResult GetFreeRooms()
        {
            var result = _unitOfWork.Room_List.GetAll();
            return Ok(result);
        }
        public IHttpActionResult GetFreeRooms(string FromDate, string ToDate)
        {
            return Json(_unitOfWork.Room_List.GetRoomsForReservationInDate(new DataSourceRequest() { Page = 0, PageSize = 0 }, PersianDateConvertor.ConvertToGregorian(FromDate).Date, PersianDateConvertor.ConvertToGregorian(ToDate).Date, null));
        }

    }

}