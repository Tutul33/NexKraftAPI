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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices serivce;
        public CustomerController(ICustomerServices _serivce)
        {
            serivce = _serivce;
        }
        /// <summary>
        /// This operation will get customer list
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<object?> GetCustomerList([FromQuery] CustomerData param)
        {
            object? data = null;
            try
            {
                data = await serivce.GetCustomerList(param);
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
        public async Task<object?> GetCustomerByCustomerID([FromQuery] int id)
        {
            object? data = null;
            try
            {
                data = await serivce.GetCustomerByCustomerID(id);
                if (data == null)
                {
                    data = new { message = "No data found" };
                }
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
        public async Task<object?> CreateCustomer([FromBody] CreateCustomerModel data)
        {
            object? resdata = null;
            try
            {
                resdata = await serivce.CreateCustomer(data);

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
                resdata = await serivce.UpdateCustomer(data);
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
        public async Task<object?> DeleteCustomer([FromQuery] int id)
        {
            object? resdata = null;
            try
            {
                resdata = await serivce.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }
    }
}
