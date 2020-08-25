using System;
namespace BackEndAD.TempService
{
    public class RequisitionTrend
    {
        public String Category { get; set; }
        public String DepartmentName { get; set; }
        public String DateOfAuthorizing { get; set; }
        public int ReqQty { get; set; }

        public RequisitionTrend()
        {
        }

        override
        public String ToString()
        {
            return Category + " " + DepartmentName + " " + DateOfAuthorizing + " " + ReqQty;
        }
    }
}
