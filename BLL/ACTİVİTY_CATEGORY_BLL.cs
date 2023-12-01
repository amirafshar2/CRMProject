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
    public class ACTİVİTY_CATEGORY_BLL
    {
        ACTİVİTY_CATEGORY_DAL dal = new ACTİVİTY_CATEGORY_DAL();
        public string Create(ACTİVİTY_CATEGORY a)
        {
            return dal.Create(a);
        }
        public List<ACTİVİTY_CATEGORY> Read_All()
        {
            return dal.Read_All();
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public ACTİVİTY_CATEGORY Readaccatagory(string s)
        {
            return dal.Readaccatagory(s);
        }

        public List<string> Readkatrgoryname()
        {
            return dal.Readkatrgoryname();
        }
    }
}
