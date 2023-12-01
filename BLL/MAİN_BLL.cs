using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MAİN_BLL
    {

        MAİN_DAL dal = new MAİN_DAL();
        public string TotalReminders(USER u)
        {
            return dal.TotalReminders(u);
        }
        public string Totalmonthlisales(USER u)
        {
            return dal.Totalmonthlisales(u);

        }
        public string Total_monthli_Payment(USER u)
        {

            return dal.Total_monthli_Payment(u);

        }
        public string Total_custumer(USER u)
        {
            return dal.Total_custumer(u);

        }
        public string NewCustumerİnmonth(USER u)
        {
            return dal.NewCustumerİnmonth(u);
        }
        public string TotalStock()
        {
            return dal.TotalStock();
        }
        public List<REMİNDER> Getuserreminder(USER u)
        {
            return dal.Getuserreminder(u);
        }
        public REMİNDER GetReminder_bayinfoandtitle(string title, string info)
        {
            return dal.GetReminder_bayinfoandtitle(title, info);
        }
        public bool Reminder_ihtar(USER u)
        {
            return dal.Reminder_ihtar(u);
     
        }
        public string Product_sale_pices()
        {
            return dal.Product_sale_pices();
        }
    }
}
