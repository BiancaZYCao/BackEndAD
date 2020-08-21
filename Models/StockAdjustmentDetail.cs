using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class StockAdjustmentDetail
    {
        public int Id { get; set; }
        public int stockAdjustmentId { get; set; }
        public int StationeryId { get; set; }
        public int discpQty { get; set; }
        public string comment { get; set; }
        public virtual StockAdjustment stockAdjustment { get; set; }
        public virtual Stationery Stationery { get; set; }
    }
}
