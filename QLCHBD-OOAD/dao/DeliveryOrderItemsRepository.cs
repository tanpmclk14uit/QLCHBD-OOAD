using QLCHBD_OOAD.model.delivery;
using System;
using System.Collections.Generic;
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

        public bool DeliOrderItemsIDisNotNULL(long id)
        {
            string command = "SELECT * FROM import_form_item WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                return true;
            }
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
            string command = "INSERT INTO import_form_item (`id`, `import_form_id`, `quantity`, `disk_id`, `disk_name`, `disk_price`, `id_by_provider`, `create_time`, `update_time`) VALUES ('" +
                items.id + "', '" +
                items.deliID + "', '" +
                items.Amount + "', '" +
                items.diskID + "', '" +
                items.diskName + "', '" +
                items.imPrice + "'," +
                items.providerID + "'," +
                "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void insertItems(string id, string deliID, string Amount, string diskID, string diskName, string imPrice, string providerID)
        {
            string command = "INSERT INTO import_form_item (`id`, `import_form_id`, `quantity`, `disk_id`, `disk_name`, `disk_price`, `id_by_provider`, `create_time`, `update_time`) VALUES ('" +
                id + "', '" +
                deliID + "', '" +
                Amount + "', '" +
                diskID + "', '" +
                diskName + "', '" +
                imPrice + "'," +
                providerID + "'," +
                "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
            database.executeCommand(command);
            database.closeConnection();
        }
    }
}
