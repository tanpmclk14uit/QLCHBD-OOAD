using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.images;

namespace QLCHBD_OOAD.dao
{
    class DeliveryBilingItemsRepository
    {
        private Db database;
        private static DeliveryBilingItemsRepository instance;

        private DeliveryBilingItemsRepository()
        {
            database = Db.getInstace();
        }
        public static DeliveryBilingItemsRepository getInstance()
        {
            if (instance == null)
                instance = new DeliveryBilingItemsRepository();
            return instance;
        }

        public void insertItems(DeliBillsItems items)
        {
            string command = "INSERT INTO import_billing_item (`id`, `import_bill`, `disk_name`, `import_price`, `disk_id`, `amount`, `create_time`, `update_time`) VALUES ('" +
                items.id + "', '" +
                items.billID + "', '" +
                items.diskName + "', '" +
                items.price + "', '" +
                items.diskID + "', '" +
                items.amount + "'," +
                "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void insertItems(DeliOrderItems items, string id)
        {
            string command = "INSERT INTO import_billing_item (`import_bill`, `disk_name`, `import_price`, `disk_id`, `amount`, `create_time`, `update_time`) VALUES ('" +
                id + "', '" +
                items.diskName + "', '" +
                items.imPrice + "', '" +
                items.diskID + "', '" +
                items.Amount + "'," +
                "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void insertItems(Images items, string id)
        {
            string command = "INSERT INTO import_billing_item (`import_bill`, `disk_name`, `import_price`, `disk_id`, `amount`, `create_time`, `update_time`) VALUES ('" +
                id + "', '" +
                items.name + "', '" +
                items.lostCharges + "', '" +
                items.idByProvider + "', '" +
                items.quantity + "'," +
                "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void insertItems(string id, string billID, string diskName, string price, string diskID, string amount)
        {
            string command = "INSERT INTO import_billing_item (`id`, `import_bill`, `disk_name`, `import_price`, `disk_id`, `amount`, `create_time`, `update_time`) VALUES ('" +
                id + "', '" +
                billID + "', '" +
                diskName + "', '" +
                price + "', '" +
                diskID + "', '" +
                amount + "'," +
                "CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
            database.executeCommand(command);
            database.closeConnection();
        }
        public ObservableCollection<DeliBillsItems> getItemsbyBillsID(string id)
        {
            ObservableCollection<DeliBillsItems> deliBillsItems = new ObservableCollection<DeliBillsItems>();
            string command = "SELECT * FROM import_billing_item WHERE import_bill =" + id;
            var reader = database.executeCommand(command);

            while (reader.Read())
            {
                DeliBillsItems items = new DeliBillsItems((long)reader[0], (long)reader[1], (string)reader[2], (int)reader[3], (long)reader[4], (int)reader[5], (DateTime)reader[6], (DateTime)reader[7]);
                deliBillsItems.Add(items);
            }
            return deliBillsItems;
        }
        public void removeItemByID(string id)
        {
            string command = "DELETE FROM import_billing_item WHERE id =" + id + " ;";
            database.executeCommand(command);
            database.closeConnection();

        }
        public void removeItemByBillsID(string id)
        {
            string command = "DELETE FROM import_billing_item WHERE import_bill =" + id + " ;";
            database.executeCommand(command);
            database.closeConnection();
        }

        public ObservableCollection<DeliBillsItems> filterItems()
        {
            ObservableCollection<DeliBillsItems> deliBillsItems = new ObservableCollection<DeliBillsItems>();
            string command = "SELECT * FROM import_form_item";
            var reader = database.executeCommand(command);

            while (reader.Read())
            {
                DeliBillsItems items = new DeliBillsItems((long)reader[0], (long)reader[1], (string)reader[2], (long)reader[3], (long)reader[4], (int)reader[5], (DateTime)reader[6], (DateTime)reader[7]);
                deliBillsItems.Add(items);
            }

            return deliBillsItems;
        }
        public long getNumberDeliveringImage()
        {
            string command = "Select Sum(import_form_item.quantity) from `import_form_item`";
            var reader = database.executeCommand(command);
            long result = 0;
            while (reader.Read())
            {
                result = (long)reader[0];
            }
            database.closeConnection();
            return result;
        }


    }
}
