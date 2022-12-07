using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

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

        //This is to Display all the Saved Employee Details
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
           
            var employeeFromDb = (from employee in _context.Employees
                                  join EmployeeDepartment in _context.DepartmentEmployees
                                  on employee.EmployeeId equals EmployeeDepartment.EmployeeId
                                  select new 
                                  {
                                      EmployeeName = employee.EmployeeName,
                                      EmployeeAddress = employee.EmployeeAddress,
                                      EmployeeSalary = employee.EmployeeSalary,
                                      DesignationName = employee.Designation.DesignationName,
                                      DesignationCode = employee.Designation.DesignationCode,
                                      DepartmentName = EmployeeDepartment.Department.DepartmentName,
                                      DepartmentCode = EmployeeDepartment.Department.DepartmentCode
                                  });
           
            return Ok(employeeFromDb);
        }
        [HttpPost]
        public IActionResult SaveEmployees([FromBody] EmployeeDTO employeeDTO)
        {
            //Add/Save New Employees
            if (ModelState.IsValid && employeeDTO != null)
            {
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
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employeeInDb = _context.Employees.Find(id);
            if (employeeInDb != null)
            {
                var departmentInDb = _context.DepartmentEmployees.Where(emp => emp.EmployeeId == employeeInDb.EmployeeId).Select(dep => dep.DepartmentId);
                var designationInDb = _context.Designations.FirstOrDefault(desg=>desg.DesignationId==id);
                List<string> departmentNamesList = new List<string>();
                List<int> departmentCodesList = new List<int>();
                foreach (var departmentId in departmentInDb)
                {
                    var departmentNameList = _context.Departments.Where(dep => dep.DepartmentId == departmentId).Select(s => s.DepartmentName).FirstOrDefault();
                    var departmentCodeList = _context.Departments.Where(dep => dep.DepartmentId == departmentId).Select(s => s.DepartmentCode).FirstOrDefault();
                
                    departmentNamesList.Add(departmentNameList);
                    departmentCodesList.Add(departmentCodeList);
                }
                EmployeeDTO employeeDTO = new EmployeeDTO()
                {
                    EmployeeId = employeeInDb.EmployeeId,
                    EmployeeName = employeeInDb.EmployeeName,
                    EmployeeAddress = employeeInDb.EmployeeAddress,
                    EmployeeSalary = employeeInDb.EmployeeSalary,
                    DepartmentNames = departmentNamesList,
                    DepartmentCodes = departmentCodesList,
                    DesignationId = employeeInDb.DesignationId,
                    DesignationName = designationInDb.DesignationName,
                    DesignationCode = designationInDb.DesignationCode
                };
                return Ok(employeeDTO);
            }
            return NotFound();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid && employeeDTO!=null)
            {
                Employee employee = new Employee()
                {
                    EmployeeId = employeeDTO.EmployeeId,
                    EmployeeName = employeeDTO.EmployeeName,
                    EmployeeAddress = employeeDTO.EmployeeAddress,
                    DesignationId = employeeDTO.DesignationId,
                };
                _context.Employees.Update(employee);
                _context.SaveChanges();
                var employeeExistingDepartments = _context.DepartmentEmployees.Where(dep => dep.EmployeeId == employee.EmployeeId).ToList();
                _context.RemoveRange(employeeExistingDepartments);
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
                _context.DepartmentEmployees.AddRange(departmentEmployees);
                _context.SaveChanges();
                return Ok();

            }
            return BadRequest();

        }
        
    }
}
