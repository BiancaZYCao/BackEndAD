using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class ItemsNeedtobeOrdered
    {
        public int stationeryId { get; set; }
        public int actualreOrderQty { get; set; }
        public string category { get; set; }
        public string desc { get; set; }
        public string unit { get; set; }
        public int reOrderQty { get; set; }
        public int reOrderLevel { get; set; }
        public int inventoryQty { get; set; }
    }
}
