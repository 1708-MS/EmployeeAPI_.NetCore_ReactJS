using EmployeeAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/Designation")]
    [ApiController]
    [Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DesignationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet()]
        public IActionResult GetAllDesignations()
        {
            var designationInDb = _context.Designations.ToList();
            return Ok(designationInDb);
        }
        [HttpGet("{id}")]
        public IActionResult GetDesignationByEmployeeId(int id)
        {
            var designationOfEmployee = _context.Employees.Include(x=>x.Designation).Where(x=>x.EmployeeId==id).ToList();
            return Ok(designationOfEmployee);
        }
    }
}
