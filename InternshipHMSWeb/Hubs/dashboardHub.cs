using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace InternshipHMSWeb.Hubs
{
    public class dashboardHub : Hub
    {

       

        public void Refresh(string reservationId,string state,string reservationNumber,string fromDate,string toDate)
        {
            GlobalHost.ConnectionManager.GetHubContext<dashboardHub>().Clients.All.Refresh(reservationId,state, reservationNumber,fromDate,toDate);

        }

    }
}