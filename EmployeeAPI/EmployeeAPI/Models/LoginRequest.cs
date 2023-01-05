using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class LoginRequest
    {
        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }
    }
}
