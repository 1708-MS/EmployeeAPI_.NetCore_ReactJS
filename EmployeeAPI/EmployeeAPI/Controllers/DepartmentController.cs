using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Models.DTOs;
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
    [Route("api/Department")]
    [ApiController]
   // [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            //var departmentInDb = _context.Departments.ToList();
           //var departmentList = departmentInDb.Select(d => new Department() )
            var departmentInDb = from Department in _context.Departments
                                 select new GetAllDepartmentsDTO
                                 {
                                     DepartmentId = Department.DepartmentId,
                                     DepartmentName = Department.DepartmentName,
                                     DepartmentCode = Department.DepartmentCode
                                 };
            return Ok(departmentInDb);
        }
        [HttpGet("{id}")]
        public IActionResult GetDepartmentByEmployeeId(int id)
        {
            //var departmentOfEmployee = _context.Employees.Include(x => x.Designation).Where(x => x.EmployeeId == id).ToList();
            var departmentOfEmployee = _context.DepartmentEmployees.Include(dept => dept.Department).Where(emp => emp.EmployeeId == id).ToList();
            return Ok(departmentOfEmployee);
        }
    }
}
