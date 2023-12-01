using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Dynamic;

namespace DAL
{
    public class İNVOİCE_DAL
    {
        DB DB = new DB();
        Random rnd = new Random();

        public string Create(İNVOİCE i, CUSTOMER c, List<PRODUCT> p, USER u)
        {
            try
            {

                i.User = DB.users.Find(u.id);
                i.Customer = DB.Customers.Find(c.id);
                //i.Customer.Bakiye =  i.Customer.Bakiye + i.TotalPrice;
                string s = rnd.Next(1000000).ToString();
                var q = DB.invoices.Where(f => f.invoiceNumber == s);

                while (q.Count() > 0)
                {
                    s = rnd.Next(1000000).ToString();
                }
                i.invoiceNumber = s;
                foreach (var item in p)
                {
                    item.Stock = 0;
                    i.products.Add(item);
                }
               


                DB.invoices.Add(i);
                DB.SaveChanges();
                return "Sipariş oluşturuldu";
            }
            catch (Exception e)
            {
                return "Kayıt sırasında bir sorun oluştu   \n" + e.Message;
            }
        }
        public DataTable readall()
        {
            string cmd = "SELECT        dbo.İNVOİCE.id, dbo.İNVOİCE.invoiceNumber AS [Fiş numarası], dbo.İNVOİCE.RegDate AS [Kayit tarihi],  dbo.CUSTOMERs.Name AS [Müşteri Adı],                           dbo.CUSTOMERs.Company AS Firma, dbo.USERs.Name AS [Satiş Temsilcisi]  FROM            dbo.İNVOİCE INNER JOIN                          dbo.CUSTOMERs ON dbo.İNVOİCE.Customer_id = dbo.CUSTOMERs.id INNER JOIN                           dbo.USERs ON dbo.İNVOİCE.User_id = dbo.USERs.id AND dbo.CUSTOMERs.User_id = dbo.USERs.id  WHERE        (dbo.İNVOİCE.Deletestatus = 0) ";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=DBCRM;Integrated Security=true");
            var adptor = new SqlDataAdapter(cmd, con);
            var bulid = new SqlCommandBuilder(adptor);
            var ds = new DataSet();
            adptor.Fill(ds);
            return ds.Tables[0];
        }
        public İNVOİCE read_by_id(int id)
        {
            var q = DB.invoices.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                return q;
            }
            return null;
        }
        public string update(İNVOİCE i, CUSTOMER c, List<PRODUCT> p, USER u, int id)
        {
            try
            {
                var q = DB.invoices.Where(e => e.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.TotalPrice = i.TotalPrice;
                    q.İsCheckedOut = i.İsCheckedOut;
                    if (i.CeackOutDate != null)
                    {
                        q.CeackOutDate = i.CeackOutDate;
                    }
                    q.Customer = DB.Customers.Find(c.id);
                    q.User = DB.users.Find(u.id);
                    foreach (var item in p)
                    {
                        q.products.Add(item);
                    }
                    DB.SaveChanges();
                    return "Düzenleme başarılı bir şekilde gerçekleştı";
                }
                return "Fiş bulunmadı";
            }
            catch (Exception e)
            {
                return "Düzenleme sırasında bir sorun oluştu   \n" + e.Message; ;

            }
        }
        public string Delete(int id)
        {
            try
            {
                var q = DB.invoices.Where(e => e.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.Deletestatus = true;
                    DB.SaveChanges();
                    return "Silme başarılı bir şekilde gerçekleştı";
                }
                return "Fiş bulunmadı";
            }
            catch (Exception e)
            {
                return "Silme sırasında bir sorun oluştu   \n" + e.Message; ;

            }
        }
        public string Checkouute_Payment(int id)
        {
            var q = DB.invoices.Where(i => i.id == id).FirstOrDefault();
            if (q != null && q.İsCheckedOut != true)
            {
                q.İsCheckedOut = true;
                q.CeackOutDate = DateTime.Now.Date;
                DB.SaveChanges();
                return "Ödeme alındı olarak işaretlendı";
            }
            return "Sipariş Bulunamadı ";

        }
        public DataTable SearchCustomer(string s)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=DBCRM;Integrated Security=true");
            SqlCommand com = new SqlCommand("dbo.Searchinvoice");
            com.Parameters.AddWithValue("@Search", s);
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;

            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = com;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
        public string Readinvnum()
        {
            var q = DB.invoices.OrderByDescending(i => i.id).FirstOrDefault();
            return q.invoiceNumber;
        }
        public string Payed(int id)
        {
            try
            {
                var q = DB.invoices.Where(e => e.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.İsCheckedOut = true;
                    q.CeackOutDate = DateTime.Now.Date;

                    DB.SaveChanges();
                    return "Ödeme Bilgisi başarılı bir şekilde Kayıt edildi";
                }
                return "Fiş bulunmadı";
            }
            catch (Exception e)
            {
                return "Düzenleme sırasında bir sorun oluştu   \n" + e.Message; ;

            }
        }
        public string invoicepics()
        {
            return DB.invoices.Where(i => i.Deletestatus == false).Count().ToString();
        }
        public string İnvoicenum()
        {
           var q = DB.invoices.OrderByDescending ( i => i.id).FirstOrDefault();
            return q.invoiceNumber;

        }
        //public string update_sale_pics( List <PRODUCT> p , int s) 
        //{
        //  foreach
        //}
        public CUSTOMER Get_customer_Bay_invoice_İd(int id)
        {
            return DB.invoices.Include("Customers").Where(i=>i.id == id).Select(i=>i.Customer).FirstOrDefault();
        }
        public DataTable Get_İnvoice_Product(int id)
        {

            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=DBCRM;Integrated Security=true");
            SqlCommand com = new SqlCommand("dbo.GetİnvoiceProduct");
            com.Parameters.AddWithValue("@Search", id);
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;

            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = com;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
    }
}
