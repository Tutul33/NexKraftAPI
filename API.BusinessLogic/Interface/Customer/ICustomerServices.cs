using API.Data.ORM.MsSQLDataModels;
using API.Data.ViewModels.Common;
using API.Data.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.Customer
{
    public interface ICustomerServices 
    {
        Task<object?> DeleteCustomer(int id);
        Task<object?> GetCustomerByCustomerID(int id);
        Task<object?> GetCustomerList(CustomerData param);
        Task<object?> CreateCustomer(CreateCustomerModel data);
        Task<object?> UpdateCustomer(vmCustomerUpdate data);
    }
}
