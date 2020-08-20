using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class SupplierItem
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int StationeryId { get; set; }
        public float price { get; set; }
        public String unit { get; set; }
    }
}
