using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models.DTOs
{
    public class GetAllDepartmentsDTO
    {
        public int DepartmentId { get; set; }
        public String DepartmentName { get; set; }
        public int DepartmentCode { get; set; }
    }
}
