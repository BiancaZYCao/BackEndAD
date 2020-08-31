using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class RequisitionViewModel
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public String dateOfRequest { get; set; }
        public String dateOfAuthorizing { get; set; }
        public int authorizerId { get; set; }
        public String status { get; set; }
        public String comment { get; set; }
        public String employee { get; set; }
        public String authorizer { get; set; }
    }
}
