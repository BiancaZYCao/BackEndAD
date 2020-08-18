using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class PurchaseOrder
    {
        public int id { get; set; }
       [ForeignKey("Employee")]
       public int clerkId { get; set; }
       public int supplierId { get; set; }
       public DateTime dateOfOrder { get; set; }
       public string status { get; set; }
       public int stockAdjustmentId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual StockAdjustment StockAdjustment { get; set; }

    }
}
