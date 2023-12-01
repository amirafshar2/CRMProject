using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class USER
    {

        public USER()
        {
            DeletStatus = false;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string TC { get; set; }
        public string E_mail { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Pic { get; set; }
        public string Status { get; set; }
        public DateTime Regtime { get; set; }
        public bool DeletStatus { get; set; }

        public List<CUSTOMER> customers { get; set; } = new List<CUSTOMER> { };
        public List<ACTİVİTY> activities { get; set; } = new List<ACTİVİTY> { };
        public List<REMİNDER> reminders { get; set; } = new List<REMİNDER> { };
        public List<İNVOİCE> invoices { get; set; } = new List< İNVOİCE> { };
        public USER_GROUP userGroup { get; set; }
    }
}
public enum can
{
    Read,
    Create,
    Update,
    Delete
}