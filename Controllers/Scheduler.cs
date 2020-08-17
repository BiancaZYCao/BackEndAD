using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private IStoreClerkService _clkService;    

        public SchedulerController(IStoreClerkService storeClerkService)
        {
            _clkService = storeClerkService;
        }
        [HttpGet]
        public IActionResult compile()
        {
            return Redirect("http://127.0.0.1:5000/");
        }


    }
}
