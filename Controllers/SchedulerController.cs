using BackEndAD.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController: ControllerBase
    {
        private IStoreClerkService _storeClerkService;

        public SchedulerController(IStoreClerkService storeClerkService)
        {
            _storeClerkService = storeClerkService;
        }
        [HttpGet("reorder")]
        public IActionResult reorder()
        {
            return Redirect("http://127.0.0.1:5000/reorder");
        }

        [HttpGet("seeder")]
        public IActionResult seeder()
        {
            return Redirect("http://127.0.0.1:5000/seeder");
        }
    }
}
