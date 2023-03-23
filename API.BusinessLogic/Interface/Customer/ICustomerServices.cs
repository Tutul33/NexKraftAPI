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
        /// <summary>
        /// This operation will delete customer data from database
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<object?> DeleteCustomer(string param);
        /// <summary>
        /// This operation will get customer by customerId from database
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<vmCustomer?> GetCustomerByCustomerID(string param);
        /// <summary>
        /// This operation will get customer list
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<object?> GetCustomerList(string param);
        /// <summary>
        /// This operation will create customer data to database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<object?> CreateCustomer(vmCustomer data);
        /// <summary>
        /// This operation will update customer data to database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<object?> UpdateCustomer(vmCustomerUpdate data);
    }
}
