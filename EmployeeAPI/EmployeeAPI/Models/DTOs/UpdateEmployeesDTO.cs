using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models.DTOs
{
    public class UpdateEmployeesDTO
    {
        public int EmployeeId { get; set; }
        public String EmployeeName { get; set; }
        public String EmployeeAddress { get; set; }
        public string EmployeeSalary { get; set; }
        public int DesignationId { get; set; }
        public List<int> DepartmentIds { get; set; }

    }
}
