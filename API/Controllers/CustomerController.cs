using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Customer;
using API.Data.ViewModels.Common;
using API.Data.ViewModels.Customers;
using API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase, ICustomerServices
    {
        private readonly ICustomerServices? mgt = null;
        public CustomerController(ICustomerServices newMgt)
        {
            mgt = newMgt;
        }
        /// <summary>
        /// This operation will get customer list
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<object?> GetCustomerList([FromQuery] string param)
        {
            object? data = null;
            try
            {
                data = await mgt.GetCustomerList(param);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                data
            };
        }
        /// <summary>
        /// This operation will get customer by customerId from database
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<vmCustomer?> GetCustomerByCustomerID([FromQuery] string param)
        {
            vmCustomer? data = null;
            try
            {
                data = await mgt.GetCustomerByCustomerID(param);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }
        /// <summary>
        /// This operation will create customer data to database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<object?> CreateCustomer([FromBody] vmCustomer data)
        {
            object? resdata = null;
            try
            {
                resdata = await mgt.CreateCustomer(data);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;

        }
        /// <summary>
        /// This operation will update customer data to database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<object?> UpdateCustomer([FromBody] vmCustomerUpdate data)
        {
            object? resdata = null;
            try
            {
                resdata = await mgt.UpdateCustomer(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }
        /// <summary>
        /// This operation will delete customer data from database
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<object?> DeleteCustomer([FromQuery] string param)
        {
            object? resdata = null;
            try
            {
                resdata = await mgt.DeleteCustomer(param);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }
    }
}
