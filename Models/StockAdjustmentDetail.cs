using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class StockAdjustmentDetail
    {
        public int id { get; set; }
        public int StockAdjustmentId { get; set; }
        public int StationeryId { get; set; }
        public int discpQty { get; set; }
        public virtual StockAdjustment StockAdjustment { get; set; }
        public virtual Stationery Stationery { get; set; }
    }
}
