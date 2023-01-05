using EmployeeAPI.ServiceContract;
using EmployeeAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        // Create Roles
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var role = new IdentityRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) return Ok();  // Role was created successfully
            return BadRequest(result.Errors); // An error occurred while creating the role
        }

        // Token Authenticate 
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _userService.Authenticate(loginViewModel);
            if (user == null) return BadRequest(new { message = "Wrong User/Password" });
            return Ok(user);
        }
    }
}
