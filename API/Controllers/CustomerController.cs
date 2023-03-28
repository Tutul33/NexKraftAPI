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
       
        [HttpPost("[action]"), Authorizations]
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
