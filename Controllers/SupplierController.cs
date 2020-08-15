using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.Models;
using BackEndAD.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackEndAD.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // CONTROLLER METHODS handling each HTTP get/put/post/request 
        // GET: api/Supplier
        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAllSuppliers()
        {
            var result = await _supplierService.findAllSuppliersAsync();
            if (result != null)
                //Docs says that Ok(...) will AUTO TRANSFER result into JSON Type
                return Ok(result);
            else
                //this help to return a NOTfOUND result, u can customerize the string.
                return NotFound("Suppliers not found");
        }
    }
}
