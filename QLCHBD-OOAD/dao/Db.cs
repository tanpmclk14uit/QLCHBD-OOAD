using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.dao
{
    class Db
    {
        public StringBuilder result = new StringBuilder();
        private Db() { }
        private static Db _instance;
        private MySqlConnection connection { get; set; }
        private const string connectionString = "server=localhost;user id=root;database=test;port=3306;password=123456";

        public static Db getInstace()
        {
            if (_instance == null)
                _instance = new Db();
            return _instance;
        }


        public MySqlDataReader executeCommand(string command)
        {
            MySqlDataReader reader = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = command;
                reader = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error");
            }
            return reader;
        }
        public void closeConnection()
        {
            if (this.connection.State == System.Data.ConnectionState.Open || connection == null)
                this.connection.Close();
        }
    }
}
