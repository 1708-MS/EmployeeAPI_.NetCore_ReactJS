using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register New User having UserName and UserEmail as same column
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new ApplicationUser { UserName = request.EmployeeName, Email = request.RegisterEmail };
            var result = await _userManager.CreateAsync(user, request.RegisterPassword);
            if (result.Succeeded)
            {
                // Assign the role to the user
                await _userManager.AddToRoleAsync(user, SD.Role_Employee);

                // Return the response
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.LoginEmail, request.LoginPassword, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded) return Ok(); //// User was successfully logged in
            return BadRequest("Invalid login attempt.");
        }
    }
}
