using API.Data.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.Customer
{
    public interface ILoginServices
    {
        Task<object> LoginUser(LoginCredential credential);
    }
}
