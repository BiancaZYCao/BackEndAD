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
        [ForeignKey ("Employee")]
        public string HeadID { get; set; }
        [ForeignKey("Employee")]
        public string RepID { get; set; }
        [ForeignKey("Employee")]
        public string DelegateID { get; set; }
        public DateTime delgtStartDate { get; set; }
        public DateTime delgtEndDate { get; set; }
        [ForeignKey("CollectionInfo")]
        public string collectionID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual CollectionInfo CollectionInfo { get; set; }


    }
}
