using InternshipHMSDataAccess;
using InternshipHMSModels.Models;
using InternshipHMSService;
using InternshipHMSWeb.App_Start.IoCConfig;
using InternshipHMSWeb.JsonWebTokenConfig;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Linq;
using System.ServiceModel.Security.Tokens;
using System.Timers;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(InternshipHMSWeb.Startup))]
namespace InternshipHMSWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //test

            ConfigureAuth(app);
            app.UseOAuthAuthorizationServer(SmObjectFactory.Container.GetInstance<AppOAuthOptions>());
            app.UseJwtBearerAuthentication(SmObjectFactory.Container.GetInstance<AppJwtOptions>());



            DefaultSetting.CreateRoleUserDefaults();
            CreateLookUpTables();
           

        }

      
        private void CreateLookUpTables()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Guid Temporary = Guid.Parse("4c825c2b-ad88-48d5-af69-277d97351651");
                Guid Fixed = Guid.Parse("4c825c2b-ad88-48d5-af69-296d97351441");
                Guid PayUp = Guid.Parse("4c825c2b-ad88-48d5-af69-277d97396441");
                Guid Canceled = Guid.Parse("4c825c2b-ad46-48d5-af69-277d97396441");

                if (db.ReservationState_List.FirstOrDefault(f => f.ID == Temporary) == null)
                {
                    db.ReservationState_List.Add(new ReservationState()
                    {
                        ID = Temporary,
                        Title = ReserveState.Temporary
                    });
                }
                if (db.ReservationState_List.FirstOrDefault(f => f.ID == Fixed) == null)
                {
                    db.ReservationState_List.Add(new ReservationState()
                    {
                        ID = Fixed,
                        Title = ReserveState.Fixed
                    });
                }
                if (db.ReservationState_List.FirstOrDefault(f => f.ID == PayUp) == null)
                {
                    db.ReservationState_List.Add(new ReservationState()
                    {
                        ID = PayUp,
                        Title = ReserveState.PayUp
                    });
                }
                if (db.ReservationState_List.FirstOrDefault(f => f.ID == Canceled) == null)
                {
                    db.ReservationState_List.Add(new ReservationState()
                    {
                        ID = Canceled,
                        Title = ReserveState.Canceled
                    });
                }
                db.SaveChanges();
            }
        }
    }
}
