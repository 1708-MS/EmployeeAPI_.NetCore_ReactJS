using EmployeeAPI.Models;
using EmployeeAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.ServiceContract
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel);
    }
}
