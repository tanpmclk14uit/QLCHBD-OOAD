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
        public void insertProvider(string ID, string name, string number, string mail, string address)
        {
            string command = "INSERT INTO provider (`id`, `name`, `number`, `mail`, `image`, `address`, `create_time`, `update_time`, `create_by`, `update_by`) VALUES (" +
                ID + ", '"
                + name + "', '"
                + number + "', '"
                + mail + "', '"
                + "'/QLCHBD-OOAD;component/assets/img_noImage.png'" + "', '"
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
        public void insertProvider(DeliProviders providers)
        {
            string command = "INSERT INTO provider (`id`, `name`, `number`, `mail`, `address`, `create_time`, `update_time`, `create_by`, `update_by`) VALUES (" +
                providers.id + ", '"
                + providers.providerName + "', '"
                + providers.providerNumber + "', '"
                + providers.providerMail + "', '"
                + providers.providerAddress + "', "
                + "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, "
                + providers.createID + ", "
                + providers.updateID + ");";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void removeProviderByID(string id)
        {
            string command = "DELETE FROM provider WHERE id = " + id;
            database.executeCommand(command);
            database.closeConnection();
        }

        public DeliProviders getProviderbyID(string id)
        {
            string command = "SELECT * FROM provider WHERE id =" + id;
            var reader = database.executeCommand(command);
            DeliProviders provider;
            if (reader.Read())
            {
                provider = new DeliProviders((long)reader[0], (string)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (DateTime)reader[5], (DateTime)reader[6], (long)reader[7], (long)reader[8], (string)reader[9]);
                database.closeConnection();
                return provider;
            }
            database.closeConnection();
            return null;
        }
        public void updateByProvider(DeliProviders providers)
        {
            string command = "UPDATE provider SET " +
                "`number` = '"+ providers.providerNumber +"', " +
                "`mail` = '" + providers.providerMail + "', " +
                "`address` = '" + providers.providerAddress + "', " +
                "`update_time` = CURRENT_TIMESTAMP, " +
                "`update_by` = '" + 1 + "', " +
                "`image` = '" + providers.image + "'" +
                " WHERE id =" + providers.id;
            database.executeCommand(command);
            database.closeConnection();
        }

        public List<DeliProviders> providerList()
        {
            List<DeliProviders> providersLists = new List<DeliProviders>();
            string command = "SELECT * FROM provider";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                DeliProviders provider = new DeliProviders((long)reader[0], (string)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (DateTime)reader[5], (DateTime)reader[6], (long)reader[7], (long)reader[8], (string)reader[9]);
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
                DeliProviders providers = new DeliProviders((long)reader[0], (string)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (DateTime)reader[5], (DateTime)reader[6], (long)reader[7], (long)reader[8], (string)reader[9]);
                deliProviders.Add(providers);
            }
            database.closeConnection();
            return deliProviders;
        }
    }
}









