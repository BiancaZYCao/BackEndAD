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

        public int clerkId { get; set; }
        public int SupplierId { get; set; }
        public DateTime dateOfOrder { get; set; }
        public string status { get; set; }
        public int StockAdjustmentId { get; set; } //ReceivalID
        public float subTotal { get; set; }

        [ForeignKey("Clerk")]
        public virtual Employee Employee { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual StockAdjustment StockAdjustment { get; set; }

        //Test [HttpPost("generatePO")]
        public List<PurchaseOrderDetail> DetailList { get; set; } 


    }
}
