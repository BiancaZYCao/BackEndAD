using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class DisbursementDetailByDept
    {
        public DateTime date { get; set; }
        public int disbursementListId { get; set; }
        public int departmentId { get; set; }
        public String departmentName { get; set; }
        public String representativeName { get; set; }
        public String itemDescription { get; set; }
        public int revQuantity { get; set; }
       
        public String status { get; set; }
    }
}
