using System.Web.Mvc;

namespace InternshipHMSWeb.Areas.Customer
{
    public class CustomerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Customer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Customer_default",
                "Customer/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional },
                new[] { "InternshipHMSWeb.Areas.Customer.Controllers" }
            );
        }
    }
}