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
    public class USER_GROUP_BLL
    {
        USER_GROUP_DAL dal = new USER_GROUP_DAL();
        public string Create(USER_GROUP u)
        {
            return dal.Create(u);
        }
        public DataTable GetAll()
        {
            return dal.GetAll();
        }
        public USER_GROUP Get(int id)
        {
            return dal.Get(id);
        }
        public USER_GROUP getug_bytitle(string s)
        {
            return dal.getug_bytitle(s);
        }
        public List<string> GetTitle()
        {
            return dal.GetTitle();
        }
        public string delete(int id)
        {

            return dal.delete(id);
        }
    }
}
