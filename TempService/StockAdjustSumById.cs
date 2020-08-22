using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class StockAdjustSumById
    {
        public int stockAdustmentId { get; set; }
        public int empId { get; set; }
        public String empName { get; set; }
        public double amount { get; set; }
    }
}
