using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class RequisitionDetailsApply
    {
        public int requisitionDetailId { get; set; }
        public String category { get; set; }
        public String desc { get; set; }
        public int reqQty { get; set; }
        public String unit { get; set; }
    }
}
