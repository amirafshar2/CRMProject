using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class USER_ACCESS_ROLE
    {
        public int id { get; set; }
        public string Section { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CamDelete { get; set; }

        public USER_GROUP UserGroup { get; set; }
    }
}
