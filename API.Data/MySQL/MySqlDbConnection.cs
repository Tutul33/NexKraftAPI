using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.MySQL
{
    public class MySqlDbConnection : IDisposable
    {
        public MySqlConnection Connection { get; }

        public MySqlDbConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
