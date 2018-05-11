using MySql.Data.MySqlClient;
using System.Configuration;
using MySql.Data.Entity;
using System;

namespace Garcom.Infra.DBLeitura
{
    public class ContextoLeitura
    {
        private MySqlConnection _mySqlConnection;
        public MySqlConnection MySqlConnection
        {
            get
            {
                if (_mySqlConnection == null)
                    return _mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbgarcom"].ConnectionString);
                return _mySqlConnection;
            }
            private set { }
        }
    }
}
