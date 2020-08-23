using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.TempService
{
    public class RequesterRow
    {
        public DateTime date { get; set; }
        public int requestorId { get; set; }
        public String requestorName { get; set; }
        public int itemCount { get; set; }
       
        public String status { get; set; }
    }
}
