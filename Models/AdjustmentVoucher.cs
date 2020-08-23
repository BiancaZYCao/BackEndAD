using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.Models;

namespace BackEndAD.Models
{
    public class AdjustmentVoucher
    {
        public int Id { get; set; }
        public int StockAdjustmentId { get; set; }
        public DateTime date { get; set; }
        public int EmployeeId { get; set; }
        

        public virtual StockAdjustment StockAdjustment { get; set; }
        public virtual Employee Employee { get; set; }
        public List<AdjustmentVoucherDetail> AdjustmentVoucherDetails { get; set; }
    }
}
