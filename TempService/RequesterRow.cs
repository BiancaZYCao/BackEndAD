using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class RequesterRow
    {
        public DateTime date { get; set; }
        public int disbursementListId { get; set; }
        public int departmentId { get; set; }
        public String departmentName { get; set; }
        public String representativeName { get; set; }
        public int itemCount { get; set; }
        public String collectionPoint { get; set; }
        public String status { get; set; }
    }
}
