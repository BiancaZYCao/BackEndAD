using BackEndAD.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
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
        
        [HttpGet("reorder")]
        public IActionResult reorder()
        {
            bool success = true;
            Console.WriteLine("Compiling order");
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
                Console.WriteLine("done compiling");
            }
            else
            {
                Console.WriteLine("compiling failed");
            }
            
            return Ok("done");
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
                Console.WriteLine("done seeding");
            }
            else
            {
                Console.WriteLine("seeding failed");
            }

            return Ok("done");
        }
    }
}
