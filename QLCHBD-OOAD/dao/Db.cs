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
        //private const string connectionString = "server=ooad.mysql.database.azure.com;user id=ooad_admin@ooad;database=ooad_qlchbd;port=3306;password=QLCHBD123456!";
        private const string connectionString = "server=localhost;user id=root;database=ooad_qlchbd;port=3306;password=123456";

        public static Db getInstace()
        {
            if (_instance == null)
                _instance = new Db();
            return _instance;
        }

        public long excuteInsertCommand(string command)
        {
            long lastInsertId = -1;
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                lastInsertId = long.Parse(cmd.LastInsertedId.ToString());
                connection.Close();
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                closeConnection();
            }
            return lastInsertId;
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
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
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
