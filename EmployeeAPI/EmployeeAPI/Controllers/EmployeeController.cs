using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    //[Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ApplicationDbContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        //This is to Display all the Saved Employee Details

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employeeFromDb = (from employee in _context.Employees
                                  join EmployeeDepartment in _context.DepartmentEmployees
                                  on employee.EmployeeId equals EmployeeDepartment.EmployeeId
                                  select new GetAllEmployeesDTO
                                  {
                                      EmployeeId = employee.EmployeeId,
                                      EmployeeName = employee.EmployeeName,
                                      EmployeeAddress = employee.EmployeeAddress,
                                      EmployeeSalary = employee.EmployeeSalary,
                                      DesignationName = employee.Designation.DesignationName,
                                      DesignationCode = employee.Designation.DesignationCode,
                                      DepartmentName = _context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId).Select(employee => employee.Department.DepartmentName).ToList(),
                                      DepartmentCode = _context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId).Select(employee => employee.Department.DepartmentCode).ToList(),
                                      DepartmentIds = _context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId).Select(employee => employee.Department.DepartmentId).ToList(),
                                      DesignationId = employee.DesignationId
                                  });
            List<GetAllEmployeesDTO> getAllEmployeesDTOs = new List<GetAllEmployeesDTO>();
            foreach (var employee in employeeFromDb)
            {
                if (getAllEmployeesDTOs.FirstOrDefault(employeeInDTOList => employeeInDTOList.EmployeeId == employee.EmployeeId) == null)
                {
                    getAllEmployeesDTOs.Add(employee);
                }
            }
            return Ok(getAllEmployeesDTOs);
        }

        // Get/Fetch Employee details from the database by Id
        //[HttpGet("{id:int}")]
        //public IActionResult GetEmployeeById(int id)
        //{

        //    var employeeInDb = _context.Employees.Include(employee => employee.Designation).FirstOrDefault(employee => employee.EmployeeId == id);
        //    if (employeeInDb != null)
        //    {
        //        var departmentInDb = _context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employeeInDb.EmployeeId)
        //            .Select(departmentEmployee => departmentEmployee.DepartmentId);

        //        List<string> departmentNamesList = new List<string>();
        //        List<int> departmentCodesList = new List<int>();
        //        foreach (var departmentId in departmentInDb)
        //        {
        //            var departmentNameList = _context.Departments.Where(department => department.DepartmentId == departmentId)
        //                .Select(department => department.DepartmentName).FirstOrDefault();
        //            var departmentCodeList = _context.Departments.Where(department => department.DepartmentId == departmentId)
        //                .Select(department => department.DepartmentCode).FirstOrDefault();

        //            departmentNamesList.Add(departmentNameList);
        //            departmentCodesList.Add(departmentCodeList);
        //        }
        //        GetEmployeeByIdDTO getEmployeeByIdDTO = new GetEmployeeByIdDTO()
        //        {
        //            EmployeeId = employeeInDb.EmployeeId,
        //            EmployeeName = employeeInDb.EmployeeName,
        //            EmployeeAddress = employeeInDb.EmployeeAddress,
        //            EmployeeSalary = employeeInDb.EmployeeSalary,
        //            DepartmentNames = departmentNamesList,
        //            DepartmentCodes = departmentCodesList,
        //            DesignationId = employeeInDb.DesignationId,
        //            DesignationName = employeeInDb.Designation.DesignationName,
        //            DesignationCode = employeeInDb.Designation.DesignationCode
        //        };
        //        return Ok(getEmployeeByIdDTO);
        //    }
        //    return NotFound();
        //}

        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employeeInDb = _context.Employees.Include(employee => employee.Designation).FirstOrDefault(employee => employee.EmployeeId == id);
            if (employeeInDb == null)
            {
                return NotFound();
            }
            var departmentInDb = _context.DepartmentEmployees
                .Where(departmentEmployee => departmentEmployee.EmployeeId == employeeInDb.EmployeeId)
                .Join(_context.Departments, departmentEmployee => departmentEmployee.DepartmentId, department => department.DepartmentId,
                (departmentEmployee, department) => new { department.DepartmentName, department.DepartmentCode });

            var departmentNamesList = departmentInDb.Select(d => d.DepartmentName).ToList();
            var departmentCodesList = departmentInDb.Select(d => d.DepartmentCode).ToList();

            var getEmployeeByIdDTO = new GetEmployeeByIdDTO
            {
                EmployeeId = employeeInDb.EmployeeId,
                EmployeeName = employeeInDb.EmployeeName,
                EmployeeAddress = employeeInDb.EmployeeAddress,
                EmployeeSalary = employeeInDb.EmployeeSalary,
                DepartmentNames = departmentNamesList,
                DepartmentCodes = departmentCodesList,
                DesignationId = employeeInDb.DesignationId,
                DesignationName = employeeInDb.Designation.DesignationName,
                DesignationCode = employeeInDb.Designation.DesignationCode
            };
            return Ok(getEmployeeByIdDTO);
        }

        //[HttpPost]
        //public IActionResult SaveEmployees([FromBody] SaveEmployeesDTO saveEmployeesDTO)
        //{
        //    if (ModelState.IsValid && saveEmployeesDTO != null)
        //    {
        //        Employee employee = new Employee()
        //        {
        //            EmployeeName = saveEmployeesDTO.EmployeeName,
        //            EmployeeAddress = saveEmployeesDTO.EmployeeAddress,
        //            EmployeeSalary = saveEmployeesDTO.EmployeeSalary,
        //            DesignationId = saveEmployeesDTO.DesignationId,
        //        };
        //        _context.Employees.Add(employee);
        //        _context.SaveChanges();

        //        // Departments allotment to the saved Employees 
        //        List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
        //        foreach (var employeeDepartment in saveEmployeesDTO.DepartmentId)
        //        {
        //            DepartmentEmployee departmentEmployee = new DepartmentEmployee()
        //            {
        //                EmployeeId = employee.EmployeeId,
        //                DepartmentId = employeeDepartment
        //            };
        //            departmentEmployees.Add(departmentEmployee);
        //        }

        //        //Saving the alloted department in the database
        //        _context.DepartmentEmployees.AddRange(departmentEmployees);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    else
        //        return BadRequest();
        //}

        [HttpPost]
        public IActionResult SaveEmployees([FromBody] SaveEmployeesDTO saveEmployeesDTO)
        {
            if (ModelState.IsValid && saveEmployeesDTO != null)
            {
                var employee = new Employee
                {
                    EmployeeName = saveEmployeesDTO.EmployeeName,
                    EmployeeAddress = saveEmployeesDTO.EmployeeAddress,
                    EmployeeSalary = saveEmployeesDTO.EmployeeSalary,
                    DesignationId = saveEmployeesDTO.DesignationId
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                var departmentEmployees = saveEmployeesDTO.DepartmentId
                    .Select(departmentId => new DepartmentEmployee
                    {
                        EmployeeId = employee.EmployeeId,
                        DepartmentId = departmentId
                    })
                    .ToList();

                _context.DepartmentEmployees.AddRange(departmentEmployees);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeesDTO updateEmployeesDTO)
        {
            if (ModelState.IsValid && updateEmployeesDTO != null)
            {
                // Updating and Saving Employees and Designation
                var employee = await _context.Employees.FindAsync(updateEmployeesDTO.EmployeeId);
                employee.DesignationId = updateEmployeesDTO.DesignationId;

                // Updating the assigned Departments to the Employees
               
                List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
                foreach (var empDep in updateEmployeesDTO.DepartmentIds)
                {
                    if (!_context.DepartmentEmployees.Any(de => de.EmployeeId == employee.EmployeeId && de.DepartmentId == empDep))
                    {
                        DepartmentEmployee departmentEmployee = new DepartmentEmployee()
                        {
                            EmployeeId = employee.EmployeeId,
                            DepartmentId = empDep
                        };
                        departmentEmployees.Add(departmentEmployee);
                    }
                }
                _context.DepartmentEmployees.AddRange(departmentEmployees);
                _context.RemoveRange(_context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId && !updateEmployeesDTO.DepartmentIds.Contains(departmentEmployee.DepartmentId)));

                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
        
        //public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeesDTO updateEmployeesDTO)
        //{
        //    if (ModelState.IsValid && updateEmployeesDTO != null)
        //    {
        //        // Updating and Saving Employees and Designation
        //        var employee = await _context.Employees.FindAsync(updateEmployeesDTO.EmployeeId);
        //        employee.DesignationId = updateEmployeesDTO.DesignationId;

                //        // Updating the assigned Departments to the Employees
                //        _context.RemoveRange(_context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId && !updateEmployeesDTO.DepartmentIds.Contains(departmentEmployee.DepartmentId)));
                //        List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
                //        foreach (var empDep in updateEmployeesDTO.DepartmentIds)
                //        {
                //            DepartmentEmployee departmentEmployee = new DepartmentEmployee()
                //            {
                //                EmployeeId = employee.EmployeeId,
                //                DepartmentId = empDep
                //            };
                //            departmentEmployees.Add(departmentEmployee);
                //        }
                //        _context.DepartmentEmployees.AddRange(departmentEmployees);

                //        await _context.SaveChangesAsync();
                //        return Ok();
                //    }
                //    return BadRequest();
                //}
                //[HttpPut]
                //public IActionResult UpdateEmployee([FromBody] UpdateEmployeesDTO updateEmployeesDTO)
                //{
                //    if (ModelState.IsValid && updateEmployeesDTO != null)
                //    {
                //        // Updating and Saving Employees and Designation
                //        Employee employee = new Employee()
                //        {
                //            EmployeeId = updateEmployeesDTO.EmployeeId,
                //            EmployeeName = updateEmployeesDTO.EmployeeName,
                //            EmployeeAddress = updateEmployeesDTO.EmployeeAddress,
                //            EmployeeSalary = updateEmployeesDTO.EmployeeSalary,
                //            DesignationId = updateEmployeesDTO.DesignationId,
                //        };
                //        _context.Employees.Update(employee);
                //        _context.SaveChanges();

                //        // Updating the assigned Departments to the Employees
                //        var employeeExistingDepartments = _context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId).ToList();
                //        _context.RemoveRange(employeeExistingDepartments);
                //        List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
                //        foreach (var empDep in updateEmployeesDTO.DepartmentIds)
                //        {
                //            DepartmentEmployee departmentEmployee = new DepartmentEmployee()
                //            {
                //                EmployeeId = employee.EmployeeId,
                //                DepartmentId = empDep
                //            };
                //            departmentEmployees.Add(departmentEmployee);
                //        }
                //        _context.DepartmentEmployees.AddRange(departmentEmployees);
                //        _context.SaveChanges();
                //        return Ok();
                //    }
                //    return BadRequest();
                //}

                // Directly delete full details of the Employees from Database
                // This deletes the Designation and Departments assigned to the Employees
       
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployees(int id)
        {
            var employeeInDb = _context.Employees.Find(id);
            if (employeeInDb != null)
            {
                _context.Remove(employeeInDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}