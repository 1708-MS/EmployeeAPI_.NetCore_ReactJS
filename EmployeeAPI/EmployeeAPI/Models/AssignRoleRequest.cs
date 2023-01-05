using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
