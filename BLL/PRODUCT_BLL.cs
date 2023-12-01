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
    public class PRODUCT_BLL
    {
        PRODUCT_DAL dal = new PRODUCT_DAL();

        public string create(PRODUCT p)
        {
            //if (dal.CHeack(p))
            //{
            return dal.Create(p);
            //}
            //return "Ürün zatem mevcut";

        }
        public DataTable ReadAll()
        {

            return dal.ReadAll();

        }
        public string Update(PRODUCT p, int id)
        {
            //if (dal.CHeack(p))
            //{
            return dal.update(p, id);
            //}
            //return "Ürün Kayıtta Mevcut";            
        }
        public PRODUCT Readbyid(int id)
        {
            return dal.Readbyid(id);
        }


        public string Delet(int id)
        {
            return dal.Delet(id);
        }
        public DataTable Search(string s)
        {
            return dal.Search(s);
        }
        public PRODUCT Chenge_price(int id, double price)
        {
            return dal.Chenge_price(id, price);
        }

        public string Productpics()
        {
            return dal.Productpics();
        }
        public List<PRODUCT> Readall()
        {
            return dal.Readall();
        }
        public string check_pic(string c, string n, string k)
        {
            return dal.check_pic(c, n, k);
        }
    }
}
