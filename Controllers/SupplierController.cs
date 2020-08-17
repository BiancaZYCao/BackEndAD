using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace BackEndAD.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierService _supplierService;
        private readonly ProjectContext _context;

        public SupplierController(ProjectContext context,ISupplierService supplierService)
        {
            _supplierService = supplierService;
            _context = context;
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

        [HttpPost]
        [ActionName("saveSupplier")]
        public async Task<ActionResult<Supplier>> saveSupplier([FromBody] Supplier s)
        {
            Supplier sup = new Supplier()
            {
                supplierCode = s.supplierCode,
                name = s.name,
                contactPerson = s.contactPerson,
                email= s.email,
                phoneNum = s.phoneNum,
                gstRegisNo = s.gstRegisNo,
                fax = s.fax,
                address = s.address,
                priority = s.priority,
            };

            _context.Supplier_Table.Add(sup);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllSuppliers), new { }, sup);
        }
    }
}
