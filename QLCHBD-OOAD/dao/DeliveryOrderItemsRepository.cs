using QLCHBD_OOAD.model.delivery;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.dao
{
    class DeliveryOrderItemsRepository
    {
        private Db database;
        private static DeliveryOrderItemsRepository instance;

        private DeliveryOrderItemsRepository()
        {
            database = Db.getInstace();
        }
        public static DeliveryOrderItemsRepository getIntance()
        {
            if (instance == null)
                instance = new DeliveryOrderItemsRepository();
            return instance;
        }

        public bool DeliOrderItemsIDisNotNULL(string id)
        {
            string command = "SELECT * FROM import_form_item WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                database.closeConnection();
                return true;
            }
            database.closeConnection();
            return false;
        }

        public void insertItems(DeliBillsItems items, string importFormID)
        {
            string command = "INSERT INTO import_form_item (`import_form_id`, `quantity`, `disk_id`, `disk_name`, `disk_price`, `id_by_provider`) VALUES ('" +
                importFormID + "', '" +
                items.amount + "', '" +
                items.diskID + "', '" +
                items.diskName + "', '" +
                items.price + "', '" +
                items.id + "');";
            database.executeCommand(command);
            database.closeConnection();
        }

        public void insertItems(DeliOrderItems items)
        {
            string command = "INSERT INTO import_form_item (`import_form_id`, `quantity`, `disk_id`, `disk_name`, `disk_price`, `id_by_provider`, `create_time`, `update_time`) VALUES ('" +
                items.deliID + "', '" +
                items.Amount + "', '" +
                items.diskID + "', '" +
                items.diskName + "', '" +
                items.imPrice + "', '" +
                items.IDbyProvider + "'," +
                "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
            database.executeCommand(command);
            database.closeConnection();
        }        
        public void removeItemByID(string id)
        {
            string command = "DELETE FROM import_form_item WHERE id =" + id + " ;";
            database.executeCommand(command);
            database.closeConnection();

        }
        public void removeItemByImportFormsID(string id)
        {
            string command = "DELETE FROM import_form_item WHERE import_form_id =" + id + " ;";
            database.executeCommand(command);
            database.closeConnection();
        }
        public ObservableCollection<DeliOrderItems> getItemsbyImportFormsID(string id)
        {
            ObservableCollection<DeliOrderItems> Items = new ObservableCollection<DeliOrderItems>();
            string command = "SELECT * FROM import_form_item WHERE import_form_id =" + id;
            var reader = database.executeCommand(command);

            while (reader.Read())
            {
                DeliOrderItems item = new DeliOrderItems((long)reader[0], (long)reader[1], (int)reader[2], (long)reader[3], (string)reader[4], (int)reader[5], (long)reader[6]);
                Items.Add(item);
            }
            database.closeConnection();
            return Items
        }

        //Làm gì có Bug, commit chơi thôi

        //HAPPY BIRTHDAY m nhá

        //Dậy thôi :v
    }
}
