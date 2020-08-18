using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class DisbursementDetail
    {
        public int id { get; set; }
        public int DisbursementListId { get; set; }
        public int RequisitionDetailId { get; set; }
        public int qty { get; set; }
        public virtual DisbursementList DisbursementList { get; set; }
        public virtual RequisitionDetail RequisitionDetail { get; set; }
    }
}
