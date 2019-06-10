using InternshipHMSDataAccess;
using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using InternshipHMSService.Core.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class RoomPriceRepository : Repository<RoomPrice>, IRoomPriceRepository
    {
        public RoomPriceRepository(ApplicationDbContext db) : base(db)
        {
        }

        public DataSourceResult GetForRoomWithDateByDataSourceResult(DataSourceRequest request, Guid roomId, DateTime requestDate)
        {
            PersianCalendar pc = new PersianCalendar();
            if (GetAll().Where(w => w.Date == requestDate && w.RoomID == roomId).Count() != 0)
            {
                RoomPrice test = GetAll().Where(w => w.RoomID == roomId && w.Date == requestDate).OrderByDescending(d => d.CreateDate).FirstOrDefault();
                return GetAll().Where(w => w.RoomID == roomId && w.Date == requestDate).OrderByDescending(d => d.CreateDate).ToDataSourceResult(request, roomPrice => new
                {
                    ID = roomPrice.ID,
                    Date = roomPrice.Date.Date.ToShortDateString(),
                    PersianDay = pc.GetDayOfMonth(requestDate).ToString().Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹"),
                    Price = roomPrice.Price.ToString("#.#"),
                    RoomID = roomPrice.RoomPrice_Room.ID.ToString(),
                    RoomNumber = roomPrice.RoomPrice_Room.Number,
                });
            }
            else
            {


                List<RoomPrice> roomPrices = new List<RoomPrice>();
                roomPrices.Add(new RoomPrice()
                {
                    Price = 0,
                    Date = requestDate
                });
                return roomPrices.ToDataSourceResult(request, Prices => new
                {
                    Date = Prices.Date.Date.ToShortDateString(),
                    PersianDay = pc.GetDayOfMonth(requestDate).ToString().Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹"),
                    Price = Prices.Price,
                });


            }
        }

        public long RoomFinalPriceByDataSourceResult(DataSourceRequest request, Guid roomId, Guid reservationId)
        {
            return 1;
        }
    }
}
