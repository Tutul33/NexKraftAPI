using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.PostGreSQL
{
    public interface IGenericFactoryPostgreSql<T> where T : class
    {
        /// <summary>
        /// Execute store procedure with parameter, return out parameter
        /// </summary>
        /// <param name="spQuery"></param>
        /// <param name="inParam"></param>
        /// <param name="outParam"></param>
        /// <returns></returns>
        Task<Hashtable> ExecuteCommand(string spQuery, Hashtable inParam,Hashtable outParam);
        /// <summary>
        /// Execute function and retrieve single record
        /// </summary>
        /// <param name="spQuery"></param>
        /// <returns></returns>
        Task<T?> ExecuteQuerySingleString(string spQuery);
        /// <summary>
        /// Execute function and retrieve list of records
        /// </summary>
        /// <param name="spQuery"></param>
        /// <returns></returns>
        Task<List<T?>?> ExecuteQueryList(string spQuery);
        /// <summary>
        /// Mapping data reader
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        List<T?> DataReaderMapToList<Tentity>(IDataReader reader);
        List<T?> DataReaderMapSingleData<Tentity>(IDataReader reader);
    }
}
