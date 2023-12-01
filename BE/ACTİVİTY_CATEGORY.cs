using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ACTİVİTY_CATEGORY
    {
        public ACTİVİTY_CATEGORY()
        {
            DeletStatus = false;
        }
        public int id { get; set; }
        public string CategoryName { get; set; }
        public bool DeletStatus { get; set; }
        public List<ACTİVİTY> activities { get; set; } = new List<ACTİVİTY>();
    }
}
