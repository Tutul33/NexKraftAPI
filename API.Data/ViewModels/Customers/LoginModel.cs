using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ViewModels.Customers
{
    public class LoginModel: LoginCredential
    {
        public int LoginID { get; set; } = 0;
        public int CustomerID { get; set; } = 0;
        public string? Email { get; set; } = "";
        public string RemoteIpAddress { get; set; } = "";
        public string MachineName { get; set; } = "";
    }
    public class LoginCredential
    {
        public string? UserName { get; set; } = "";
        public string? Password { get; set; } = "";
    }
}
