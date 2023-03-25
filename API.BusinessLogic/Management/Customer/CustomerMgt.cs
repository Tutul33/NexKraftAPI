using API.Data.ADO.NET;
using API.Data.ViewModels.Common;
using API.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data.ViewModels.Customers;
using API.Data.ORM.DataModels;
using API.BusinessLogic.Interface.Customer;
using Newtonsoft.Json;
using Azure;
using System.Xml;
using API.Data.MySQL;
using API.Data.PostGreSQL;

namespace API.BusinessLogic.Management.Customer
{
    public class CustomerMgt : ICustomerServices
    {
        Hashtable? ht = null;
        GenericFactory<vmCustomer>? objCustomer = null;
        GenericFactoryMySql<vmCustomer>? objCustomerMySQL = null;
        GenericFactoryPostgreSql<vmCustomer>? objCustomerPostgreSQL = null;
        public CustomerMgt() { }
        public async Task<object?> GetCustomerList(string param)
        {
            dynamic? dParam = JsonConvert.DeserializeObject(param);
            CommonData cmnParam = JsonConvert.DeserializeObject<CommonData>(dParam.ToString());

            objCustomer = new GenericFactory<vmCustomer>();
            objCustomerMySQL = new GenericFactoryMySql<vmCustomer>();
            objCustomerPostgreSQL = new GenericFactoryPostgreSql<vmCustomer>();
            List<vmCustomer?>? listCustomer = new List<vmCustomer?>();
            try
            {
                if (StaticInfos.IsMsSQL)
                {
                    ht = new Hashtable
                         {
                            { "PageIndex", cmnParam.PageNumber },
                            { "PageSize", cmnParam.PageSize}
                         };
                    listCustomer = await objCustomer.ExecuteCommandList("[dbo].[SP_GetCustomersPageWise]", ht, StaticInfos.MsSqlConnectionString);

                }
                else if (StaticInfos.IsMySQL)
                {
                    ht = new Hashtable
                        {
                           { "PageIndex", cmnParam.PageNumber },
                           { "PageSize", cmnParam.PageSize}
                        };
                    listCustomer = await objCustomerMySQL.ExecuteCommandList("SP_GetCustomersPageWise", ht, StaticInfos.MySqlConnectionString);
                }
                else if (StaticInfos.IsPostgreSQL)
                {
                    string functionName = "fnc_getcustomerlist(" + cmnParam.PageNumber + "," + cmnParam.PageSize + ",'" + cmnParam.Search + "')";
                    listCustomer = await objCustomerPostgreSQL.ExecuteQueryList(functionName, StaticInfos.PostgreSqlConnectionString);
                }

            }
            catch (Exception ex)
            {

            }
            return new
            {
                listCustomer
            };
        }
        public async Task<vmCustomer?> GetCustomerByCustomerID(string param)
        {
            objCustomer = new GenericFactory<vmCustomer>();
            objCustomerMySQL = new GenericFactoryMySql<vmCustomer>();
            objCustomerPostgreSQL = new GenericFactoryPostgreSql<vmCustomer?>();
            vmCustomer? customer = new vmCustomer();
            try
            {
                CommonData cmnParam = new CommonData();
                cmnParam.Id = Convert.ToInt32(param);
                if (StaticInfos.IsMsSQL)
                {
                    ht = new Hashtable
                          {
                             { "CustomerID", cmnParam.Id}
                          };
                    customer = await objCustomer.ExecuteCommandSingle("SP_GetCustomerByCustomerID", ht, StaticInfos.MsSqlConnectionString);
                }
                else if (StaticInfos.IsMySQL)
                {
                    ht = new Hashtable
                          {
                             { "C_CustomerID", cmnParam.Id}
                          };
                    customer = await objCustomerMySQL.ExecuteCommandSingle("SP_GetCustomerByCustomerID", ht, StaticInfos.MySqlConnectionString);
                }
                else if (StaticInfos.IsPostgreSQL)
                {
                    string functionName = "fnc_getcustomer_by_id(" + cmnParam.Id + ")";
                    customer = await objCustomerPostgreSQL.ExecuteQuerySingleString(functionName, StaticInfos.PostgreSqlConnectionString);

                }

            }
            catch (Exception ex)
            {

            }
            return customer;
        }
        public async Task<object?> DeleteCustomer(string param)
        {
            objCustomer = new GenericFactory<vmCustomer>();
            objCustomerMySQL = new GenericFactoryMySql<vmCustomer>();
            objCustomerPostgreSQL = new GenericFactoryPostgreSql<vmCustomer>();
            string message = string.Empty; bool resstate = false;
            try
            {
                CommonData cmnParam = new CommonData(); int response = 0;
                cmnParam.Id = Convert.ToInt32(param);
                if (StaticInfos.IsMsSQL)
                {
                    ht = new Hashtable
                         {
                            { "CustomerID", cmnParam.Id}
                         };
                    response = await objCustomer.ExecuteCommand("SP_DeleteCustomer", ht, StaticInfos.MsSqlConnectionString);
                }
                else if (StaticInfos.IsMySQL)
                {
                    ht = new Hashtable
                         {
                            { "C_CustomerID", cmnParam.Id}
                         };
                    response = await objCustomerMySQL.ExecuteCommand("SP_DeleteCustomer", ht, StaticInfos.MySqlConnectionString);

                }
                else if (StaticInfos.IsPostgreSQL)
                {
                    ht = new Hashtable
                         {
                            { "customerid", cmnParam.Id}
                         };
                    response = await objCustomerPostgreSQL.ExecuteCommand("sp_deletecustomer", ht, StaticInfos.PostgreSqlConnectionString);

                }
                if (response > 0)
                {
                    message = "Deleted Successfully.";
                    resstate = true;
                }
                else
                {
                    message = "Failed.";
                }
            }
            catch (Exception ex)
            {

            }
            return new { message, resstate };
        }
        public async Task<object?> CreateCustomer(vmCustomer objCtomer)
        {
            objCustomer = new GenericFactory<vmCustomer?>(); objCustomerMySQL = new GenericFactoryMySql<vmCustomer>();
            objCustomerPostgreSQL = new GenericFactoryPostgreSql<vmCustomer>();
            string message = string.Empty; bool resstate = false; int response = 0;
            try
            {
                if (StaticInfos.IsMsSQL)
                {
                    ht = new Hashtable
                     {
                      { "CustomerName", objCtomer?.CustomerName},
                      { "Country", objCtomer?.Country }
                     };
                    response = await objCustomer.ExecuteCommand("SP_CreateCustomer", ht, StaticInfos.MsSqlConnectionString);

                }
                else if (StaticInfos.IsMySQL)
                {
                    ht = new Hashtable
                     {
                      { "C_CustomerName", objCtomer?.CustomerName},
                      { "C_Country", objCtomer?.Country }
                     };
                    response = await objCustomerMySQL.ExecuteCommand("SP_CreateCustomer", ht, StaticInfos.MySqlConnectionString);
                }
                else if (StaticInfos.IsPostgreSQL)
                {
                    ht = new Hashtable
                     {
                      { "customername", objCtomer?.CustomerName},
                      { "country", objCtomer?.Country }
                     };
                    response = await objCustomerPostgreSQL.ExecuteCommand("sp_createcustomer", ht, StaticInfos.PostgreSqlConnectionString);
                }

                if (response > 0)
                {
                    message = "Created Successfully.";
                    resstate = true;
                }
                else
                {
                    message = "Failed.";
                }
            }
            catch (Exception ex)
            {

            }
            return new { message, resstate };
        }
        public async Task<object?> UpdateCustomer(vmCustomerUpdate? objCtomer)
        {
            objCustomer = new GenericFactory<vmCustomer>();
            objCustomerMySQL = new GenericFactoryMySql<vmCustomer>();
            objCustomerPostgreSQL = new GenericFactoryPostgreSql<vmCustomer>();
            string message = string.Empty; bool resstate = false;
            try
            {
                //vmCustomer? objCtomer = JsonConvert.DeserializeObject<vmCustomer?>(data.ToString());
                int response = 0;

                if (StaticInfos.IsMsSQL)
                {
                    ht = new Hashtable
                        {
                           { "CustomerID", objCtomer?.CustomerID},
                           { "CustomerName", objCtomer?.CustomerName},
                           { "Country", objCtomer?.Country }
                        };
                    response = await objCustomer.ExecuteCommand("SP_UpdateCustomer", ht, StaticInfos.MsSqlConnectionString);
                }
                else if (StaticInfos.IsMsSQL)
                {
                    ht = new Hashtable
                        {
                           { "C_CustomerID", objCtomer?.CustomerID},
                           { "C_CustomerName", objCtomer?.CustomerName},
                           { "C_Country", objCtomer?.Country }
                        };
                    response = await objCustomerMySQL.ExecuteCommand("SP_UpdateCustomer", ht, StaticInfos.MySqlConnectionString);
                }
                else if (StaticInfos.IsPostgreSQL)
                {
                    ht = new Hashtable
                        {
                           { "customerid", objCtomer?.CustomerID},
                           { "customername", objCtomer?.CustomerName},
                           { "country", objCtomer?.Country }
                        };
                    response = await objCustomerPostgreSQL.ExecuteCommand("sp_updatecustomer", ht, StaticInfos.PostgreSqlConnectionString);
                }


                if (response > 0)
                {
                    message = "Updated Successfully.";
                    resstate = true;
                }
                else
                {
                    message = "Failed.";
                }
            }
            catch (Exception ex)
            {

            }
            return new { message, resstate };
        }
    }
}
