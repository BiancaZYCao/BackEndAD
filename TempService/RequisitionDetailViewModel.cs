using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class RequisitionDetailViewModel
    {
        public int id { get; set; }
        public int requisitionId { get; set; }
        public  int stationeryId { get; set; }
        public int reqQty { get; set; }
        public int rcvQty { get; set; }
        public String status { get; set; }
        public String requisition { get; set; }
        public String stationery { get; set; }
    }
}
