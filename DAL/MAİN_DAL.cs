using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MAİN_DAL
    {
        DB DB = new DB();

        REMİNDER reminder = new REMİNDER();





        public string TotalReminders(USER u)
        {
            return DB.reminders.Where(i => i.Users.id == u.id && i.DeletStatus == false && i.İsDone == false).Count().ToString();
        }
        public string Totalmonthlisales(USER u)
        {
            DateTime today = DateTime.Today;
            DateTime FirstDayOfTheThisMonth;
            DateTime LastDayOfTheThisMonth;
            if (today.Day <= 10 && today.Day >= 1)
            {
                FirstDayOfTheThisMonth = new DateTime(today.Year, ((today.Month) - 1), 10, 00, 00, 00, 0000);
                LastDayOfTheThisMonth = FirstDayOfTheThisMonth.AddMonths(1);
            }
            else
            {
                FirstDayOfTheThisMonth = new DateTime(today.Year, today.Month, 10, 00, 00, 00, 0000);
                LastDayOfTheThisMonth = FirstDayOfTheThisMonth.AddMonths(1);
            }

            var q = DB.invoices.Where(i => i.User.id == u.id && i.Deletestatus == false && i.RegDate >= FirstDayOfTheThisMonth && i.RegDate < LastDayOfTheThisMonth).Sum(i => (double?)i.TotalPrice);
            if (q.HasValue)
            {
                return q.ToString();
            }
            return "0,00";

        }
        public string Total_monthli_Payment(USER u)
        {
            DateTime today = DateTime.Today;
            DateTime FirstDayOfTheThisMonth;
            DateTime LastDayOfTheThisMonth;
            if (today.Day <= 10 && today.Day >= 1)
            {
                FirstDayOfTheThisMonth = new DateTime(today.Year, ((today.Month) - 1), 10, 00, 00, 00, 0000);
                LastDayOfTheThisMonth = FirstDayOfTheThisMonth.AddMonths(1);
            }
            else
            {
                FirstDayOfTheThisMonth = new DateTime(today.Year, today.Month, 10, 00, 00, 00, 0000);
                LastDayOfTheThisMonth = FirstDayOfTheThisMonth.AddMonths(1);
            }

            var q = DB.invoices.Where(i => i.User.id == u.id && i.Deletestatus == false && i.RegDate >= FirstDayOfTheThisMonth && i.RegDate < LastDayOfTheThisMonth && i.İsCheckedOut == true).Sum(i => (double?)i.TotalPrice);
            if (q.HasValue)
            {
                return q.ToString();
            }
            return "0,00";

        }
        public string Total_custumer(USER u)
        {
            return DB.Customers.Where(i => i.User.id == u.id && i.DeletStatus == false).Count().ToString();

        }
        public string NewCustumerİnmonth(USER u)
        {
            DateTime today = DateTime.Today;
            DateTime FirstDayOfTheThisMonth;
            DateTime LastDayOfTheThisMonth;
            if (today.Day <= 10 && today.Day >= 1)
            {
                FirstDayOfTheThisMonth = new DateTime(today.Year, ((today.Month) - 1), 10, 00, 00, 00, 0000);
                LastDayOfTheThisMonth = FirstDayOfTheThisMonth.AddMonths(1);
            }
            else
            {
                FirstDayOfTheThisMonth = new DateTime(today.Year, today.Month, 10, 00, 00, 00, 0000);
                LastDayOfTheThisMonth = FirstDayOfTheThisMonth.AddMonths(1);
            }

            var q = DB.Customers.Where(i => i.User.id == u.id && i.DeletStatus == false && i.Regdate >= FirstDayOfTheThisMonth && i.Regdate < LastDayOfTheThisMonth).Count().ToString();
            if (q != null)
            {
                return q;
            }
            return "0";
        }

        public string TotalStock()
        {
            return DB.products.Where(i => i.DeletStatus == false).Sum(i => (double?)i.Stock).ToString();
        }
        public List<REMİNDER> Getuserreminder(USER u)
        {
            List<REMİNDER> re = new List<REMİNDER>();
           List<REMİNDER> r = DB.reminders.Where(i => i.Users.id == u.id && i.İsDone == false && i.DeletStatus == false ).ToList();
            foreach (var item in r)
            {
                if (item.ReminDate == DateTime.Now.Date)
                {
                    re .Add(item);
                } 
            }
            return re;
        }
        public REMİNDER GetReminder_bayinfoandtitle(string title, string info)
        {
            return DB.reminders.Where(i => i.Title == title && i.Reminderİnfo == info && i.DeletStatus == false).FirstOrDefault();
        }
        public bool Reminder_ihtar( USER u)
        {
            List<REMİNDER> r = DB.reminders.Where(i => i.Users.id == u.id && i.İsDone == false && i.DeletStatus == false).ToList();
            foreach (var item in r)
            {
                if (item.ReminDate < DateTime.Now.Date)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public string Product_sale_pices()
        {
            var q = DB.products.Where(i => i.id > 0).FirstOrDefault();
            if (q != null)
            {
                //var sum = DB.products.Where(i => i.SaledPices != 0).Select(i => i.SaledPices).Sum();
                //return sum.ToString();
            }
            return "";
        }
    }
}
