using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//This class is used to test connection with front end. -Bianca

namespace BackEndAD.Models
{
    public class Stationery
    {
        public int Id { get; set; }
        public string category { get; set; }
        public string desc { get; set; }
        public string unit { get; set; }
        public int reOrderQty { get; set; }
        public int reOrderLevel { get; set; }
        public int inventoryQty { get; set; }
    }
}
