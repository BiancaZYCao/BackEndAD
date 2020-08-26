using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class CollectionInfo
    {
        public int Id { get; set; }
        //[ForeignKey("Employee")]
        //public int? clerkId { get; set; }
        //public DateTime collectionDate { get; set; }
        public DateTime collectionTime { get; set; }
        public string collectionPoint { get; set; }
        public string lat { get; set; }
        public string longi {get;set;}
        public Employee clerk { get; set; }

        public List<Department> Departments { get; set; }

    }
}
