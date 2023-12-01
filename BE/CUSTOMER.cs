using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CUSTOMER
    {
        public CUSTOMER()
        {
            DeletStatus = false;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Regdate { get; set; }
        public bool DeletStatus { get; set; }
        public double Alacak { get; set; }
        public double Bakiye { get; set; }
        public USER User { get; set; }
        public string Adress { get; set; }
        public  string vergidairesi_bilgileri { get; set; }

        public List<ACTİVİTY> activityes { get; set; } = new List<ACTİVİTY> { };
        public List<İNVOİCE> invoices { get; set; } = new List<İNVOİCE> { };
    }
}
