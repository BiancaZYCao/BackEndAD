using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class DisbursementList
    {
        public int id { get; set; }
        public int DepartmentId { get; set; }
        public DateTime date { get; set; }
        public string deliveryPoint { get; set; }
        public virtual Department Department { get; set; }
    }
}
