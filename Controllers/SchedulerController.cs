using BackEndAD.Models;
using BackEndAD.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackEndAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController: ControllerBase
    {
        private IDepartmentService _deptService;
        private IStoreClerkService _clerkService;
        private IEmailService _emailService;

        //CONSTRUCTOR: make sure u build ur service interface in.
        public SchedulerController(IEmailService emailService, IDepartmentService deptService, IStoreClerkService clerkService)
        {
            _deptService = deptService;
            _clerkService = clerkService;
            _emailService = emailService;
        }




        [HttpGet("reorder")]
        public IActionResult reorder()
        {
            bool success = true;
            //Console.WriteLine("Compiling order");
            string seederurl = String.Format("http://127.0.0.1:5000/reorder");
            WebRequest webRequest = WebRequest.Create(seederurl);
            webRequest.Method = "GET";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                success = false;

            }

            if (success)
            {
                //Console.WriteLine("done compiling");
                return Ok("done compiling");
            }
            else
            {
                //Console.WriteLine("compiling failed");
                return Ok("failed compiling");

            }

        }

        [HttpGet("seeder")]
        public IActionResult seeder()
        {
            bool success = true;
            Console.WriteLine("started seeding");
            string seederurl = String.Format("http://127.0.0.1:5000/seeder");
            WebRequest webRequest = WebRequest.Create(seederurl);
            webRequest.Method = "GET";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }
             
            if (success)
            {
                //Console.WriteLine("done seeding");
                return Ok("done seeding");
            }
            else
            {
                //Console.WriteLine("seeding failed");
                return Ok("failed seeding");
            }

        }

        [HttpGet("autoRevokeDelegate")]
        public async Task<ActionResult<Department>> autoRevokeDelegate()
        {
            var allDept = await _clerkService.findAllDepartmentAsync();
            var allEmp = await _clerkService.findEmployeesAsync();
            var allDelegate = allEmp.Where(x => x.role.Equals("DELEGATE"));
            Boolean revoked = false;
            foreach (Department dp in allDept)
            {
                if (DateTime.Compare(dp.delgtEndDate, DateTime.Now) < 0)
                {
                    Console.WriteLine("revoking " + dp.deptName + " delegate");
                    revoked = true;
                    dp.delgtEndDate = Convert.ToDateTime(null);
                    dp.delgtStartDate = Convert.ToDateTime(null);
                    _deptService.updateDeptDelegate(dp);

                    Employee currDelegate = allDelegate.Where(x => x.departmentId == dp.Id).FirstOrDefault();
                    if (currDelegate != null)
                    {
                        _deptService.updateDeptEmpRevoke(allDelegate.Where(x => x.departmentId == dp.Id).FirstOrDefault().Id, "STAFF");
                    }
                }
            }
            if (revoked)
            {
                return Ok("done");
            }
            else
            {
                return Ok("nothing to revoke");
            }

        }

        [HttpGet("disbursementreminder")]
        public async Task<ActionResult<Department>> disbursementreminder()
        {
            var allDisbursement = await _clerkService.findAllDisbursementListAsync();
            var futureDisbursement = allDisbursement.Where(x => DateTime.Compare(x.date, DateTime.Today) > 0);
            var tmrDisbursement = futureDisbursement.Where(x => DateTime.Compare(x.date, DateTime.Today.AddDays(2)) < 0);
            var allEmp = await _clerkService.findEmployeesAsync();
            var allRep = allEmp.Where(x => x.role.Equals("REPRESENTATIVE"));

            

            foreach (DisbursementList dl in tmrDisbursement)
            {
                String str = await _emailService.SendMail(allRep.Where(x=>x.departmentId==dl.DepartmentId).FirstOrDefault().email, "Upcoming Disbursement", "You have an upcoming disbursement on the "+dl.date.ToString().Substring(0,10));

            }


            return Ok("done");

        }
    }

    
}
