using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class Supplier
    {
        [Required]
        public int id { get; set; }

        [Required]
       public string supplierCode { get; set; }

        public ICollection<SupplierItem> supplierItems { get; set;}

        [Required]
        public string name { get; set; }

        [Required]
        public string contactPerson { get; set; }

        public string email { get; set; }

        [Required]
        public string phoneNum { get; set; }

        public string gstRegisNo { get; set; }

        [Required]
        public string fax { get; set; }

        [Required]
        public string address { get; set; }

        public int priority { get; set; }

        public ICollection<SupplierItem> supplierItems { get; set; }
        
    }
}
