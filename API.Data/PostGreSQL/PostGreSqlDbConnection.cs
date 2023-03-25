using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.PostGreSQL
{
    public class PostGreSqlDbConnection : IDisposable
    {
        public NpgsqlConnection Connection { get; }
        public PostGreSqlDbConnection(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
