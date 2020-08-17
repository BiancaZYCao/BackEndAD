using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class RequisitionDetail
    {
        public int Id { get; set; }
       
        
        public int reqQty { get; set; }
        public string status { get; set; }

        public virtual Requisition Requisition { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
