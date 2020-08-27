using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class fakeDisbursementDetail
    {
        public int id { get; set; }
        public int DisbursementListId { get; set; }
        public int RequisitionDetailId { get; set; }
        public int qty { get; set; }
        public String DisbursementList { get; set; }
        public String RequisitionDetail { get; set; }
    }
}
