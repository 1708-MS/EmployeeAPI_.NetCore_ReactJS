using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class RegisterRequest : ApplicationUser
    {
        public string EmployeeName { get; set; }
        //public string EmployeeAddress { get; set; }
        //public string EmployeeSalary { get; set; }
        //public string RegisterEmail { get; set; }
        //public string RegisterPassword { get; set; }
    }
}
