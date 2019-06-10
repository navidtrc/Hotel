using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using InternshipHMSService.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace InternshipHMSWeb.Areas.Admin.Controllers.WebApi
{
    public class ReservationResult
    {
        public string ID { get; set; }
        public string Number { get; set; }
        public string State { get; set; }
        public string Price { get; set; }
    }
    public class ReservationApiController : ApiController
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ReservationApiController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        [HttpPost]
        public IHttpActionResult AddTempReservation(List<ReservationViewModel> instances)
        {
            List<ReservationResult> reservationResults = new List<ReservationResult>();
            foreach (ReservationViewModel item in instances)
            {
                item.State = "4c825c2b-ad88-48d5-af69-277d97351651";
                Reservation reservation = ReservationMapper.Map(item);
                var result = _unitOfWork.Reservation_List.AddWithRooms(reservation, item.SelectedRooms);
                if (result)
                {
                    if (item.SelectedFellows != null)
                        _unitOfWork.Customer_List.ConvertCustomersToFellow(item.ParentId, item.SelectedFellows);
                }
                _unitOfWork.Complete();

                int days = (reservation.ToDate - reservation.FromDate).Days + 1;
                int price = 0;
                Reservation savedRes = _unitOfWork.Reservation_List.Find(reservation.ID);
                foreach (var reservationRooms in savedRes.Reservation_ReservationRooms_List)
                {
                    var data = _unitOfWork.RoomPrice_List.GetAll().Where(w => w.RoomID == reservationRooms.RoomID);
                    if (data != null)
                        price += ((data.Where(w => w.Date == savedRes.FromDate).OrderByDescending(c => c.CreateDate).FirstOrDefault().Price) / 2);
                }
                price *= days;

                reservationResults.Add(new ReservationResult()
                {
                    ID = savedRes.ID.ToString(),
                    Number = savedRes.Number.ToString(),
                    State = Translator.ReservationStateToPersian(_unitOfWork.ReservationState_List.Find(savedRes.ReservationStateID).Title.ToString()),
                    Price = price.ToString()
                });
            }
            return Ok(reservationResults);
        }

        [HttpPost]
        public IHttpActionResult ChangeToFixReserve(string ReserveNumber)
        {
            if (string.IsNullOrEmpty(ReserveNumber))
                return BadRequest();
            _unitOfWork.Reservation_List.GetAll().FirstOrDefault(f => f.Number == int.Parse(ReserveNumber)).Reservation_ReservationState.Title = ReserveState.Fixed;
            _unitOfWork.Complete();
            return Ok();
        }


        //
        //        var instances =
        //                [
        //                    {
        //                    ID: null,
        //                    FromDate: '1398/02/15',
        //                    ToDate: '1398/02/30',
        //                    ParentId: '79a3eb2c-bb15-4d5b-8c66-e31de9463f7a',
        //                    SelectedFellows: ['c554f137-93ce-4708-bfb1-8a9bcb9ea941', 'b33616cf-4b63-4473-abc7-464b781051d4'],
        //                    SelectedRooms: ['8dd122e5-a4c4-4542-a0da-cacab5825310', '266650f9-d507-49b2-8a44-0160b5ada1f0']
        //                },
        //                    {
        //                        ID: null,
        //                        FromDate: '1398/02/15',
        //                        ToDate: '1398/02/30',
        //                        ParentId: '79a3eb2c-bb15-4d5b-8c66-e31de9463f7a',
        //                        SelectedFellows: ['c554f137-93ce-4708-bfb1-8a9bcb9ea941', 'b33616cf-4b63-4473-abc7-464b781051d4'],
        //                        SelectedRooms: ['8dd122e5-a4c4-4542-a0da-cacab5825310', '266650f9-d507-49b2-8a44-0160b5ada1f0']
        //}



        //                ];

        //            debugger;
        //            instances = JSON.stringify(instances);
        //            $.ajax({
        //    url: '/api/ReservationApi/AddReservation',
        //                type: 'POST',
        //                contentType: 'application/json; charset=utf-8',
        //                dataType: 'json',
        //                data: instances,
        //                success: function(result) {

        //        debugger;
        //    },
        //                error: function(result) {
        //        debugger;

        //    }
        //})
        //
    }
}
