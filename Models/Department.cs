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
        /*[ForeignKey ("Employee")]
        public int headid { get; set; }
        //[ForeignKey("Employee")]
        public int repid { get; set; }
        //[ForeignKey("Employee")]
        public int? delegaterid { get; set; }*/
        public DateTime delgtStartDate { get; set; }
        public DateTime delgtEndDate { get; set; }
        //[ForeignKey("CollectionInfo")]
        //public int CollectionId { get; set; }
        
        public Employee head { get; set; }
        public Employee rep { get; set; }
        public Employee delegater { get; set; }
        public CollectionInfo Collection { get; set; }
        [InverseProperty("department")]
        public List<Employee> employees { get; set; }


    }
}
