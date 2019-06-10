using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSLibrary
{
    public static class Translator
    {
        public static string ReservationStateToPersian(string state)
        {
            switch (state)
            {
                case "Temporary":
                    return "رزرو موقت";
                case "Fixed":
                    return "رزرو قطعی";
                case "PayUp":
                    return "پرداخت شده";
                case "Canceled":
                    return "لغو";
                default:
                    return "";
            }
        }
        public static string RoleTranslateToID(string inputRole)
        {
            switch (inputRole)
            {
                case "مدیر ارشد":
                    return "066e5a40-54ca-4c02-b7e3-ac3f6163e25d";
                case "مدیر":
                    return "b7ea9bd0-f597-4abe-8238-a9b94077cba6";
                case "کارمند":
                    return "3091bb05-4af6-4c41-b9e0-2b1fb9607a7e";
                case "مشتری":
                    return "95a75089-f400-4e40-ab94-def3626f5374";
                default:
                    return "d5b194e0-b0e7-44a0-bc11-dbbab46ddd2a";
            }
        }
        public static string RoleTranslateToName(string inputRole)
        {
            switch (inputRole)
            {
                case "066e5a40-54ca-4c02-b7e3-ac3f6163e25d":
                    return "مدیر ارشد";
                case "b7ea9bd0-f597-4abe-8238-a9b94077cba6":
                    return "مدیر";
                case "3091bb05-4af6-4c41-b9e0-2b1fb9607a7e":
                    return "کارمند";
                case "95a75089-f400-4e40-ab94-def3626f5374":
                    return "مشتری";
                default:
                    return "یوزر";
            }
        }
        public static bool RoleCheckChangingAccess(string inRole, string toRole)
        {
            if (inRole == "066e5a40-54ca-4c02-b7e3-ac3f6163e25d")
                return true;
            if (inRole == "b7ea9bd0-f597-4abe-8238-a9b94077cba6")
                if (toRole == "066e5a40-54ca-4c02-b7e3-ac3f6163e25d")
                    return true;
            if (inRole == "3091bb05-4af6-4c41-b9e0-2b1fb9607a7e")
                if (toRole == "b7ea9bd0-f597-4abe-8238-a9b94077cba6" || toRole == "066e5a40-54ca-4c02-b7e3-ac3f6163e25d")
                    return false;
            if (inRole == "95a75089-f400-4e40-ab94-def3626f5374")
                return false;
            if (inRole == "d5b194e0-b0e7-44a0-bc11-dbbab46ddd2a")
                return false;
            return true;
        }

    }
}
