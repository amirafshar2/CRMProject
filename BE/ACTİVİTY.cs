using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ACTİVİTY
    {
        public ACTİVİTY() { DeletStatus = false; }
        public int id { get; set; }
        public string Title { get; set; }
        public string İnfo { get; set; }
        public DateTime RegDate { get; set; }
        public bool DeletStatus { get; set; }
        public CUSTOMER Customer { get; set; }

        public USER User { get; set; }
        public ACTİVİTY_CATEGORY ActivityCategory { get; set; }
    }
}
