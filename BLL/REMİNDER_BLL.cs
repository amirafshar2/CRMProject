using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class REMİNDER_BLL
    {
        REMİNDER_DAL DAL = new REMİNDER_DAL();
        public string Create(REMİNDER r, USER u)
        {
            return DAL.Create(r, u);

        }
        public DataTable Read_all()
        {
            return DAL.Read_all();


        }
        public DataTable ReminderSearch(string s)
        {
            return DAL.ReminderSearch(s);

        }

        public string Update(REMİNDER c, int id , USER u)
        {

            return DAL.Update(c, id ,u);
        }

        public string Delete(int id)
        {
            return DAL.Delete(id);
        }

        public REMİNDER Readbyid(int id)
        {
            return DAL.Readbyid(id);
        }
        public string İsDone(REMİNDER c, int id)
        {
            return DAL.İsDone(c, id);

        }
        //public UserClass readUserByReminderId(int idr)
        //{
        //    return DAL.readUserByReminderId(idr);
        //}
        public void İsDone_byreminder(REMİNDER r)
        {
            DAL.İsDone_byreminder(r);
        }
        public string TotalReminders()
        {
            return DAL.TotalReminders();
        }


    }
}
