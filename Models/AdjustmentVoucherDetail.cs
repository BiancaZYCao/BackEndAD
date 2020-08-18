using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class AdjustmentVoucherDetail
    {
        public int id { get; set; }
        public int adjustmentVoucherId { get; set; }
        public int stockAdjustmentDetailId { get; set; }
        public double price { get; set; }
        public virtual AdjustmentVoucher AdjustmentVoucher { get; set; }
        public virtual StockAdjustmentDetail StockAdjustmentDetail { get; set; }
    }
}
