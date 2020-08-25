using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string deptName { get; set; }
        public string deptCode { get; set; }
        
        
        //public int headId { get; set; }
        
        //public int repId { get; set; }
        
        //public int? delegaterId { get; set; }
        public DateTime delgtStartDate { get; set; }
        public DateTime delgtEndDate { get; set; }

        //[ForeignKey("CollectionId")]
        public int CollectionId { get; set; }

        /*[ForeignKey("headId")]
        public Employee head { get; set; }
        //[ForeignKey("repId")]
        public Employee rep { get; set; }
        //[ForeignKey("delegaterId")]
        public Employee delegater { get; set; }*/
        //[ForeignKey("collectionId")]
        public CollectionInfo Collection { get; set; }
        //This 2 line cannot be removed will got error on mapping! -Bianca
        //[InverseProperty("department")]
        public List<Employee> employees { get; set; }


    }
}
