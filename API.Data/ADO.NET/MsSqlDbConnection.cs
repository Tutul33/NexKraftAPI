using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ADO.NET
{
    public class MsSqlDbConnection:IDisposable
    {
        public SqlConnection Connection { get; }

        public MsSqlDbConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
