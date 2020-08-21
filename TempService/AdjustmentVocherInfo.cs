using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class AdjustmentVocherInfo
    {
        public int stockAdustmentDetailId { get; set; }
        public int stockAdustmentId { get; set; }
        public String reason { get; set; }
        public String empName { get; set; }
        public int itemCode { get; set; }
        public int quantity { get; set; }
        public float amount { get; set; }

    }
}
