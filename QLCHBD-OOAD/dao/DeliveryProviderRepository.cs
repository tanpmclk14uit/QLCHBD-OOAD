using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCHBD_OOAD.model.delivery;
namespace QLCHBD_OOAD.dao
{
    class DeliveryProviderRepository
    {
        private Db database;
        private static DeliveryProviderRepository instance;

        public static DeliveryProviderRepository getIntance()
        {
            if (instance == null)
            {
                instance = new DeliveryProviderRepository();
            }
            return instance;
        }

        private DeliveryProviderRepository()
        {
            database = Db.getInstace();
        }

        public bool isProviderNull(string id)
        {
            string command = "SELECT * FROM provider WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                return true;
            }
            return false;

        }

        //public void insertProvider(DeliProviders providers)
        //{
        //    string command = "INSERT INTO provider (id, name, number, mail, address, create_time, update_time, create_by, update_by) VALUES (" +
        //        providers.id.ToString() + ", " 
        //        + providers.providerName + ", " 
        //        + providers.providerNumber.ToString() + ", " 
        //        + providers.providerMail + ", " 
        //        + providers.providerAddres + ", " 
        //        + DateTime.Now.ToString() + ", " 
        //        + DateTime.Now.ToString() + ", " 
        //        + providers.createID.ToString() + ", " 
        //        + providers.updateID.ToString() + " );";
        //    database.executeCommand(command);
        //}
        //public void insertProvider(string ID, string name, string number, string mail, string address, string id)
        //{
        //    string command = "INSERT INTO provider (id, name, number, mail, address, create_time, update_time, create_by, update_by) VALUES (" +
        //        ID + ", "
        //        + name + ", "
        //        + number.ToString() + ", "
        //        + mail + ", "
        //        + address + ", "
        //        + DateTime.Now.ToString() + ", "
        //        + DateTime.Now.ToString() + ", "
        //        + id.ToString() + ", "
        //        + id.ToString() + " );";
        //    database.executeCommand(command);
        //}
        public void insertProvider(string ID, string name, string number, string mail, string address)
        {
            string command = "INSERT INTO provider (`id`, `name`, `number`, `mail`, `address`, `create_time`, `update_time`, `create_by`, `update_by`) VALUES (" +
                ID + ", '"
                + name + "', '"
                + number + "', '"
                + mail + "', '"
                + address + "', "
                + "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, "
                + 1 + ", "
                + 1 + ");";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void insertProvider(string ID, string name, string number, string mail, string address, string id)
        {
            string command = "INSERT INTO provider (`id`, `name`, `number`, `mail`, `address`, `create_time`, `update_time`, `create_by`, `update_by`) VALUES (" +
                ID + ", '"
                + name + "', '"
                + number + "', '"
                + mail + "', '"
                + address + "', "
                + "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, "
                + id + ", "
                + id + ");";
            database.executeCommand(command);
            database.closeConnection();
        }

        public ObservableCollection<DeliProviders> providerList()
        {
            ObservableCollection<DeliProviders> providersLists = new ObservableCollection<DeliProviders>();
            string command = "SELECT * FROM provider";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                DeliProviders provider = new DeliProviders((long)reader[0], (string)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], -100);
                providersLists.Add(provider);
            }
            database.closeConnection();
            return providersLists;
        }
        public ObservableCollection<DeliProviders> filterDeliProviders()
        {
            ObservableCollection<DeliProviders> deliProviders = new ObservableCollection<DeliProviders>();
            string command = "SELECT * FROM provider";
            var reader = database.executeCommand(command);

            while (reader.Read())
            {
                DeliProviders providers = new DeliProviders((long)reader[0], (string)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (DateTime)reader[2], (DateTime)reader[3], (long)reader[4], (long)reader[5]);
                deliProviders.Add(providers);
            }
            database.closeConnection();
            return deliProviders;
        }
    }
}









