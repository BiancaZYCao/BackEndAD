﻿using System;
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

        [HttpGet]
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
                    DateOfAuthorizing = int.Parse(dr["monthBefore"].ToString()),
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
                //Docs says that Ok(...) will AUTO TRANSFER result into JSON Type
                return Ok(result);
            else
                //this help to return a NOTfOUND result, u can customerize the string.
                //There are 3 Department alr seeded in DB, so this line should nvr appears. 
                //I put here Just for u to understand the style. :) -Bianca  
                return NotFound("QUERY FAILED.");
        }
    }
}
