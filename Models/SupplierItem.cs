using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Summary description for Class1
/// </summary>
namespace BackEndAD.Models
{

    public class SupplierItem
    {

        public int id { get; set; }

        
        public int SupplierId { get; set; }

        [Required]
        public Stationery StationeryItem { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public string unit { get; set; }

        public virtual Stationery Stationery {get; set;}
        public virtual Supplier Supplier { get; set; }



    }


}
