using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class PurchaseOrderDetail
    {
        public int id { get; set; }
        public int purchaseOrderId { get; set; }
        public int stationeryId { get; set; }
        public int qty { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Stationery Stationery { get; set; }
    }
}
