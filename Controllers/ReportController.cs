using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.ServiceInterface;
using BackEndAD.TempService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        /*
        private IStoreClerkService _clkService;
        private IStoreManagerService _mgrService;
        private IStoreSupervisorService _supervisorService;
        private IDepartmentService _deptService;
        public ReportController(IStoreClerkService clkService,
            IStoreManagerService mgrService, IStoreSupervisorService supervisorService,
             IDepartmentService deptService)
        {
            _clkService = clkService;
            _mgrService = mgrService;
            _supervisorService = supervisorService;
            _deptService = deptService;
        }*/

        [HttpGet("reqByMonth")]
        public ActionResult<IList<RequisitionTrend>> GenerateReqGrouped()
        {
            #region  sql conn then sqlcommand
            string cnstr = "Server=tcp:team8-sa50.database.windows.net,1433;Initial Catalog=ADProj;Persist Security Info=False;User ID=Bianca;Password=!Str0ngPsword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=600;";
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();
            string sqlstr = "SELECT detCat.category,ed.deptName,datediff(mm,req.dateOfAuthorizing,getdate()) as monthBefore,SUM(detCat.reqQty) AS totalQty "
                    +"FROM(SELECT reqDet.StationeryId, reqDet.reqQty, sta.category, reqDet.RequisitionId FROM[dbo].[Stationery_Table] AS sta "
+"JOIN [dbo].[RequisitionDetail_Table] AS reqDet ON reqDet.StationeryId = sta.Id) AS detCat "
+"JOIN[dbo].[Requisition_Table] AS req ON detCat.RequisitionId = req.Id "+
"JOIN(SELECT dept.Id, dept.deptName, emp.Id AS empId FROM[dbo].[Department_Table] AS dept JOIN[dbo].[Employee_Table] AS emp ON dept.Id = emp.departmentId) AS ed " +
"ON req.EmployeeId = ed.empId Where req.[status] NOT IN('Applied','Declined') and datediff(mm, req.dateOfAuthorizing, getdate())<=3 " +
"GROUP BY category,deptName, datediff(mm, req.dateOfAuthorizing, getdate())";
            SqlCommand cmd = new SqlCommand(sqlstr, cn);

            SqlDataReader dr = cmd.ExecuteReader();
            IList<RequisitionTrend> result = new List<RequisitionTrend>();
            while (dr.Read())
            {
                RequisitionTrend reqT = new RequisitionTrend()
                {
                    Category = dr["category"].ToString(),
                    DepartmentName = dr["deptName"].ToString(),
                    DateOfAuthorizing = dr["monthBefore"].ToString(),
                    ReqQty = int.Parse(dr["totalQty"].ToString())
                };
                result.Add(reqT);
                
            }
            dr.Close();
            cn.Close();
            #endregion

            if (result != null)
                return Ok(result);
            else
                return NotFound("QUERY FAILED.");
        }

        [HttpGet("reqByDay")]
        public ActionResult<IList<RequisitionTrend>> GenerateReqGroupedByDay()
        {
            #region  sql conn then sqlcommand
            string cnstr = "Server=tcp:team8-sa50.database.windows.net,1433;Initial Catalog=ADProj;Persist Security Info=False;User ID=Bianca;Password=!Str0ngPsword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=600;";
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();
            string sqlstr = "SELECT detCat.category,ed.deptName,min(req.dateOfAuthorizing) as reqDate,SUM(detCat.reqQty) AS totalQty "
                    + "FROM(SELECT reqDet.StationeryId, reqDet.reqQty, sta.category, reqDet.RequisitionId FROM[dbo].[Stationery_Table] AS sta "
+ "JOIN [dbo].[RequisitionDetail_Table] AS reqDet ON reqDet.StationeryId = sta.Id) AS detCat "
+ "JOIN[dbo].[Requisition_Table] AS req ON detCat.RequisitionId = req.Id " +
"JOIN(SELECT dept.Id, dept.deptName, emp.Id AS empId FROM[dbo].[Department_Table] AS dept JOIN[dbo].[Employee_Table] AS emp ON dept.Id = emp.departmentId) AS ed " +
"ON req.EmployeeId = ed.empId Where req.[status] NOT IN('Applied','Declined') " +
"GROUP BY category,deptName, datediff(dd, req.dateOfAuthorizing, getdate())";
            SqlCommand cmd = new SqlCommand(sqlstr, cn);

            SqlDataReader dr = cmd.ExecuteReader();
            IList<RequisitionTrend> result = new List<RequisitionTrend>();
            while (dr.Read())
            {
                RequisitionTrend reqT = new RequisitionTrend()
                {
                    Category = dr["category"].ToString(),
                    DepartmentName = dr["deptName"].ToString(),
                    DateOfAuthorizing = dr["reqDate"].ToString(),
                    ReqQty = int.Parse(dr["totalQty"].ToString())
                };
                result.Add(reqT);

            }
            dr.Close();
            cn.Close();
            #endregion

            if (result != null)
                return Ok(result);
            else
                return NotFound("QUERY FAILED.");
        }

        [HttpGet("ReOrder")]
        public ActionResult<IList<RequisitionTrend>> GenerateReOrderGrouped()
        {
            #region  sql conn then sqlcommand
            string cnstr = "Server=tcp:team8-sa50.database.windows.net,1433;Initial Catalog=ADProj;Persist Security Info=False;User ID=Bianca;Password=!Str0ngPsword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=600;";
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();
            string sqlstr = "SELECT category,datediff(mm,ord.dateOfOrder,getdate()) as monthBefore,SUM(qty) as totalQty " +
                "FROM (SELECT StationeryId, qty, sta.category, PurchaseOrderId FROM[dbo].[Stationery_Table] as sta " +
                "JOIN[dbo].[PurchaseOrderDetail_Table] as ordDet ON  ordDet.StationeryId = sta.Id) as ordCat " +
                "JOIN[dbo].[PurchaseOrder_Table] as ord ON ordCat.PurchaseOrderId = ord.Id " +
                "Where datediff(mm, ord.dateOfOrder, getdate())<= 3" +
                "GROUP BY category,datediff(mm, ord.dateOfOrder, getdate())";

            SqlCommand cmd = new SqlCommand(sqlstr, cn);

            SqlDataReader dr = cmd.ExecuteReader();
            IList<RequisitionTrend> result = new List<RequisitionTrend>();
            while (dr.Read())
            {
                RequisitionTrend reqT = new RequisitionTrend()
                {
                    Category = dr["category"].ToString(),
                    DateOfAuthorizing = dr["monthBefore"].ToString(),
                    ReqQty = int.Parse(dr["totalQty"].ToString())
                };
                result.Add(reqT);

            }
            dr.Close();
            cn.Close();
            #endregion
            foreach (RequisitionTrend rt in result)
            {
                Console.WriteLine(rt.ToString());//output testing
            }

            if (result != null)
                return Ok(result);
            else
                return NotFound("QUERY FAILED.");
        }

        [HttpGet("ReOrderByDay")]
        public ActionResult<IList<RequisitionTrend>> GenerateReOrderGroupedDay()
        {
            #region  sql conn then sqlcommand
            string cnstr = "Server=tcp:team8-sa50.database.windows.net,1433;Initial Catalog=ADProj;Persist Security Info=False;User ID=Bianca;Password=!Str0ngPsword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=600;";
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();
            string sqlstr = "SELECT category,min(ord.dateOfOrder) as orderDate,SUM(qty) as totalQty " +
                "FROM (SELECT StationeryId, qty, sta.category, PurchaseOrderId FROM[dbo].[Stationery_Table] as sta " +
                "JOIN[dbo].[PurchaseOrderDetail_Table] as ordDet ON  ordDet.StationeryId = sta.Id) as ordCat " +
                "JOIN[dbo].[PurchaseOrder_Table] as ord ON ordCat.PurchaseOrderId = ord.Id " +
                "GROUP BY category,datediff(dd, ord.dateOfOrder, getdate())";

            SqlCommand cmd = new SqlCommand(sqlstr, cn);

            SqlDataReader dr = cmd.ExecuteReader();
            IList<RequisitionTrend> result = new List<RequisitionTrend>();
            while (dr.Read())
            {
                RequisitionTrend reqT = new RequisitionTrend()
                {
                    Category = dr["category"].ToString(),
                    DateOfAuthorizing = dr["orderDate"].ToString(),
                    ReqQty = int.Parse(dr["totalQty"].ToString())
                };
                result.Add(reqT);

            }
            dr.Close();
            cn.Close();
            #endregion
            foreach (RequisitionTrend rt in result)
            {
                Console.WriteLine(rt.ToString());//output testing
            }

            if (result != null)
                return Ok(result);
            else
                return NotFound("QUERY FAILED.");
        }

        [HttpGet("ItemNeedtobeOrdered")]
        public ActionResult<IList<ItemsNeedtobeOrdered>> GenerateItemNeedtobeOrdered()
        {
            #region  sql conn then sqlcommand
        string cnstr = "Server=tcp:team8-sa50.database.windows.net,1433;Initial Catalog=ADProj;Persist Security Info=False;User ID=Bianca;Password=!Str0ngPsword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=600;";
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();

            string sqlstr = "select ((((sum(reqDetail.reqQty) - sum(rcvQty))-sum(Stationery.inventoryQty))-sum(PoDetail.qty)) + sum(Stationery.reOrderLevel)) as difQty,Stationery.Id,Stationery.category,Stationery.[desc],Stationery.unit,Stationery.reOrderQty,Stationery.reOrderLevel,Stationery.inventoryQty " +
                "from RequisitionDetail_Table as reqDetail, Stationery_Table as Stationery, PurchaseOrderDetail_Table as PODetail, PurchaseOrder_Table as PO " +
                "where reqDetail.status != 'Delivered' and Stationery.Id = reqDetail.StationeryId and PO.status = 'ordered' and PODetail.PurchaseOrderId = PO.id and PODetail.StationeryId = Stationery.Id " +
                "group by Stationery.Id,Stationery.category,Stationery.[desc],Stationery.unit,Stationery.reOrderQty,Stationery.reOrderLevel,Stationery.inventoryQty";

            SqlCommand cmd = new SqlCommand(sqlstr, cn);

            SqlDataReader dr = cmd.ExecuteReader();
            IList<ItemsNeedtobeOrdered> result = new List<ItemsNeedtobeOrdered>();
            while (dr.Read())
            {
                if (int.Parse(dr["difQty"].ToString()) > 0)
                {
                    ItemsNeedtobeOrdered items = new ItemsNeedtobeOrdered()
                    {
                        stationeryId = int.Parse(dr["Id"].ToString()),
                        actualreOrderQty = int.Parse(dr["difQty"].ToString()),
                        category = dr["category"].ToString(),
                        desc = dr["desc"].ToString(),
                        unit = dr["unit"].ToString(),
                        reOrderQty = int.Parse(dr["reOrderQty"].ToString()),
                        reOrderLevel = int.Parse(dr["reOrderLevel"].ToString()),
                        inventoryQty = int.Parse(dr["inventoryQty"].ToString()),  
                    };
                    result.Add(items);
                }
            }
            dr.Close();
            cn.Close();
            #endregion
            foreach (ItemsNeedtobeOrdered rt in result)
            {
                Console.WriteLine(rt.ToString());//output testing
            }

            if (result != null)
                return Ok(result);
            else
                return NotFound("QUERY FAILED.");
        }
    }
}
