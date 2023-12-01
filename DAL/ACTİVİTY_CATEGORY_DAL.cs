using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ACTİVİTY_CATEGORY_DAL
    {
        DB DB = new DB();
        public string Create(ACTİVİTY_CATEGORY a)
        {
            try
            {

                DB.ActivityCategories.Add(a);
                DB.SaveChanges();
                return "Kayıt Başarılı Bir şekilde gerçekleştı";
            }
            catch (Exception e)
            {
                return "Kayıt Sırasında Bir Sorun Oluştu \n  " + e.Message;
            }
        }
        public List< ACTİVİTY_CATEGORY> Read_All()
        {
           return DB.ActivityCategories.Where(i=> i.DeletStatus== false).ToList();
        }
        public string Delete(int id)
        {
            try
            {
                var q = DB.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.DeletStatus = true;
                }
                DB.SaveChanges();
                return "Silme Başarılı Bir şekilde gerçekleştı";
            }
            catch (Exception e)
            {
                return "Silme Sırasında Bir Sorun Oluştu \n  " + e.Message;
            }
        }
        public ACTİVİTY_CATEGORY Readaccatagory(string s)
        {
            return DB.ActivityCategories.Where(i => i.CategoryName == s).SingleOrDefault();
        }

        public List<string> Readkatrgoryname()
        {
            return DB.ActivityCategories.Where(i => i.DeletStatus == false).Select(i => i.CategoryName).ToList();
        }
    }
}
