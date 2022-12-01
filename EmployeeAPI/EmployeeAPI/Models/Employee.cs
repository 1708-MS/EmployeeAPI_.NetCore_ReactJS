using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public String EmployeeName { get; set; }
        [Required]
        public String EmployeeAddress { get; set; }
        [Required]
        public String EmployeeSalary { get; set; }
        public ICollection<Department> Departments { get; set; }
        public Designation Designation { get; set; }
    }
}
