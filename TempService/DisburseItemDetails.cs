using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class DisburseItemDetails
    {
        public String itemDescription { get; set; }
        public int revQuantity { get; set; }
        public int requisitionDetailId { get; set; }
        public int requisitionId { get; set; }
    }
}
