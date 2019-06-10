using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSLibrary
{
    public static class PersianDateConvertor
    {
        public static string ConvertToPersianDate(DateTime date,bool WithPersianNumber = false)
        {
            string year = "";
            string month = "";
            string day = "";
            PersianCalendar pc = new PersianCalendar();
            year = pc.GetYear(date).ToString();
            month = pc.GetMonth(date) >= 10 ? pc.GetMonth(date).ToString() : $"0{pc.GetMonth(date).ToString()}";
            day = pc.GetDayOfMonth(date) >= 10 ? pc.GetDayOfMonth(date).ToString() : $"0{pc.GetDayOfMonth(date).ToString()}";
            if (WithPersianNumber)
            {
                string PersianYearNumber = year.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
                string PersianMonthNumber = month.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
                string PersianDayNumber = day.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
                return $"{PersianYearNumber}/{PersianMonthNumber}/{PersianDayNumber}";
            }
            return $"{year}/{month}/{day}";
        }
        public static DateTime ConvertToGregorian(string date)
        {
            if (string.IsNullOrEmpty(date))
                return DateTime.Parse("0001-01-01");
            string englishNumber = date.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
            PersianCalendar pc = new PersianCalendar();
            var PersianArray = englishNumber.Split('/');
            var select = PersianArray.Select(datePart => int.Parse(datePart)).ToArray();
            var outDate = pc.ToDateTime(select[0], select[1], select[2], 0, 0, 0, 0);
            return outDate;
        }
    }
}
