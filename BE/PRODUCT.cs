using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PRODUCT
    {
        public PRODUCT()
        {
            DeletStatus = false;
        }
        public int id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Cap { get; set; }
        public string Boy { get; set; }
        public string Packing { get; set; }
        public string Quality { get; set; }
        public string Feature { get; set; }      
        public int Stock { get; set; }
        public int SaledPices { get; set; }
        public double Price { get; set; }
        public string Kaplama { get; set; }
     
        public string DINnumber { get; set; }
        public string BrandName { get; set; }
        public string Product_cod { get; set; }
        public string picture { get; set; }
        public bool DeletStatus { get; set; }
       

        public List<İNVOİCE> invoices { get; set; } = new List<İNVOİCE> { };
    }
}
