using API.Data.MySQL;
using API.Settings;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Data.PostGreSQL
{
    public class GenericFactoryPostgreSql<T> : IGenericFactoryPostgreSql<T> where T : class, new()
    {
        private PostGreSqlDbConnection db;
        public GenericFactoryPostgreSql(PostGreSqlDbConnection dbCon)
        {
            db = dbCon;
        }

        public Task<Hashtable> ExecuteCommand(string spQuery, Hashtable inParam, Hashtable? outParam = null)
        {
            return Task.Run(() =>
            {
                Hashtable result = new Hashtable();
                try
                {
                    NpgsqlCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = spQuery;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DictionaryEntry obj in inParam)
                    {
                        NpgsqlParameter parameter = new NpgsqlParameter("@" + Convert.ToString(obj.Key), obj.Value);
                        cmd.Parameters.Add(parameter);
                    }
                    if (outParam != null)
                    {
                        foreach (DictionaryEntry obj in outParam)
                        {
                            NpgsqlParameter parameter = new NpgsqlParameter("@" + Convert.ToString(obj.Key), obj.Value)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(parameter);
                        }
                    }


                    IDataReader dr = cmd.ExecuteReader();
                    foreach (var item in cmd.Parameters.Where(x => x.Direction == ParameterDirection.Output))
                    {
                        result.Add(item.ParameterName, item.Value);
                    }
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                return result;
            });
        }

        public Task<T?> ExecuteQuerySingleString(string spQuery)
        {
            return Task.Run(() =>
            {
                T? Results = null;
                try
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "select * from " + spQuery;
                    cmd.CommandType = CommandType.Text;

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        Results = DataReaderMapSingleData<T>(reader).FirstOrDefault();
                    }
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {

                }
                return Results;
            });
        }

        public Task<List<T?>?> ExecuteQueryList(string spQuery)
        {
            return Task.Run(() =>
            {
                List<T?>? Results = null;
                try
                {
                    NpgsqlCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "select * from " + spQuery;
                    cmd.CommandType = CommandType.Text;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        Results = DataReaderMapToList<T?>(reader).ToList();
                    }
                }
                catch (Exception ex)
                {

                }

                return Results;
            });
        }

        public List<T?> DataReaderMapSingleData<Tentity>(IDataReader reader)
        {
            var results = new List<T?>();

            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if ((typeof(T).GetProperty(property.Name).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                        {
                            continue;
                        }
                        else
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                catch (Exception ex)
                {

                }
            }
            return results;
        }
        public List<T?> DataReaderMapToList<Tentity>(IDataReader reader)
        {
            var results = new List<T?>();

            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                        }
                    }
                    results.Add(item);
                }
                catch (Exception ex)
                {

                }
            }
            return results;
        }


    }
}
