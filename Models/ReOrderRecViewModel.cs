using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class ReOrderRecViewModel
    {
        public int id { get; set; }
        public Stationery stationery { get; set; }
        //all suppliers that supply item by priority
        public ICollection<Supplier> suppliers { get; set; }
    }
}
