using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var data = (from employee in _context.Employees
                        join EmployeeDepartment in _context.DepartmentEmployees
                        on employee.EmployeeId equals EmployeeDepartment.EmployeeId
                        select new
                        {
                            //  EmployeeName = employee.EmployeeName,
                            EmployeeAddress = employee.EmployeeAddress,
                            EmployeeSalary = employee.EmployeeSalary,
                            EmployeeDesignation = employee.Designation.DesignationName,
                            EmployeeDepartment = EmployeeDepartment.Department.DepartmentName
                        });
            return Ok(data);
        }
        [HttpPost]
        public IActionResult SaveEmployees([FromBody] EmployeeDTO employeeDTO)
        {
            //Add/Save New Employees
            if (!ModelState.IsValid) return BadRequest();
            Employee employee = new Employee()
            {
                EmployeeId = employeeDTO.EmployeeId,
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeAddress = employeeDTO.EmployeeAddress,
                EmployeeSalary = employeeDTO.EmployeeSalary,
                DesignationId = employeeDTO.DesignationId,
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();

            // Departments allotment to Saved Employees 
            List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
            foreach (var empDep in employeeDTO.DepartmentIds)
            {
                DepartmentEmployee departmentEmployee = new DepartmentEmployee()
                {
                    EmployeeId = employee.EmployeeId,
                    DepartmentId = empDep
                };
                departmentEmployees.Add(departmentEmployee);

            }

            //Saving the alloted department in the database
            _context.DepartmentEmployees.AddRange(departmentEmployees);
            _context.SaveChanges();
            return Ok();
        }
    }
}
