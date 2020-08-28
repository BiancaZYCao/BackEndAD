using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class RequisitionDetailsList
    { 
        public String authorizer { get; set; }
        public DateTime? authorizedDate { get; set; }
        public int requisitionDetailsId { get; set; }
        public int requisitionId { get; set; }
        public String description { get; set; }
        public int quantity { get; set; }
        public String unit { get; set; }
        public String status { get; set; }
        public String comment { get; set; }
    }
}
