using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class REMİNDER
    {
        public REMİNDER() { DeletStatus = false; İsDone = false; }
        public int id { get; set; }
        public string Title { get; set; }
        public string Reminderİnfo { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime ReminDate { get; set; }
        public bool İsDone { get; set; }
        public bool DeletStatus { get; set; }
        public USER Users { get; set; }

    }
}
