using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class USER_GROUP
    {
        public USER_GROUP()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Title { get; set; }
        public bool DeleteStatus { get; set; }

        public List<USER_ACCESS_ROLE> Roles { get; set; } = new List<USER_ACCESS_ROLE>();
        public List<USER> Users { get; set; } = new List<USER>();
    }
}
