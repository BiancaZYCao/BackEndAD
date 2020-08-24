using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class AdjustmentVoucherDetail
    {
        public int Id { get; set; }
        public int adjustmentVoucherId { get; set; }
        public int StockAdjustmentDetailId { get; set; }
        public double price { get; set; }
        public string reason { get; set; }

        public AdjustmentVoucher adjustmentVoucher { get; set; }
        public virtual StockAdjustmentDetail StockAdjustmentDetail { get; set; }
    }
}
