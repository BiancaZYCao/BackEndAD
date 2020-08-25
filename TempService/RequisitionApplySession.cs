using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class RequisitionApplySession
    {
        public List<RequisitionDetailsApply> requisition { get; set; }
        public int session { get; set; }
    }
}
