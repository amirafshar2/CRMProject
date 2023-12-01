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
    public class ACTİVİTY_DAL
    {
        DB DB = new DB();

        public string Create(ACTİVİTY a, USER u, CUSTOMER c, ACTİVİTY_CATEGORY ac)
        {
            try
            {
                a.ActivityCategory = DB.ActivityCategories.Find(ac.id);
                a.User = DB.users.Find(u.id);
                a.Customer = DB.Customers.Find(c.id);
                DB.Activitys.Add(a);
                DB.SaveChanges();
                return "Kayıt Başarılı Bir şekilde gerçekleştı";
            }
            catch (Exception e)
            {
                return "Kayıt Sırasında Bir Sorun Oluştu \n  " + e.Message;
            }
        }



        public DataTable Read_All()
        {
            string cmd = "SELECT        dbo.ACTİVİTY.id, dbo.ACTİVİTY.Title AS Konu, dbo.ACTİVİTY.İnfo AS Açıklama, dbo.ACTİVİTY.RegDate AS [Kayıt tarihi], dbo.ACTİVİTY_CATEGORY.CategoryName AS [konnu Başlığı],                          dbo.ACTİVİTY_CATEGORY.DeletStatus AS Expr1, dbo.USERs.UserName AS Temsilci FROM            dbo.ACTİVİTY INNER JOIN          dbo.USERs ON dbo.ACTİVİTY.User_id = dbo.USERs.id INNER JOIN            dbo.ACTİVİTY_CATEGORY ON dbo.ACTİVİTY.ActivityCategory_id = dbo.ACTİVİTY_CATEGORY.id  WHERE        (dbo.ACTİVİTY.DeletStatus = 0) AND (dbo.ACTİVİTY_CATEGORY.DeletStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=DBCRM;Integrated Security=true");
            var adaptor = new SqlDataAdapter(cmd, con);
            var bulider = new SqlCommandBuilder(adaptor);
            var ds = new DataSet();
            adaptor.Fill(ds);
            return ds.Tables[0];
        }

        public string Delete(int id)
        {
            try
            {
                var q = DB.Activitys.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.DeletStatus = true;
                    DB.SaveChanges();
                    return "Silme başarılı bir şekilde gerçekleştı";
                }
                return "Aktivity Bulunamadı";
            }
            catch (Exception e)
            {
                return "Silme Sırasında Bir Sorun Oluştu \n  " + e.Message;
            }
        }

        public ACTİVİTY Read_byid(int id)
        {
            var q = DB.Activitys.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                return q;
            }
            return q;
        }
        public string Activitypics()
        {
            return DB.Activitys.Where(i => i.DeletStatus == false).Count().ToString();
        }

        public DataTable Search(string s)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=DBCRM;Integrated Security=true");
            SqlCommand com = new SqlCommand("dbo.SearchActivity");
            com.Parameters.AddWithValue("@Search", s);
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            var adaptor = new SqlDataAdapter();
            adaptor.SelectCommand = com;
            var bulider = new SqlCommandBuilder(adaptor);
            var ds = new DataSet();
            adaptor.Fill(ds);
            return ds.Tables[0];
        }
    }
}
