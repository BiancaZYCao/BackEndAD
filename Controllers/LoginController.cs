using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.Models;
using BackEndAD.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("post")]
        public async Task<ActionResult<Employee>> Login([FromBody] Employee employee)
        {
            Console.WriteLine("login");
            var result = await _loginService.findEmployee(employee.email);
            
            if (result != null && result.password.Equals(employee.password))
            {
                return Ok(result);
            }
            else
            {
                return NotFound("invalid");
            }
            }
    }
}
