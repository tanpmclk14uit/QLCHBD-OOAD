using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.delivery;

namespace QLCHBD_OOAD.dao
{
    class DeliveryOrderRepository
    {
        private Db database;
        private static DeliveryOrderRepository instance;

        public static DeliveryOrderRepository getIntance()
        {
            if (instance == null)
            {
                instance = new DeliveryOrderRepository();
            }
            return instance;
        }

        private DeliveryOrderRepository()
        {
            database = Db.getInstace();
        }

        private DeliveryOrderStatus stringToDeliveryOrderStatus(String status)
        {
            if (status == DeliveryOrderStatus.DELIVERED.ToString())
            {
                return DeliveryOrderStatus.DELIVERED;
            }
            else
            {
                if (status == DeliveryOrderStatus.WATING.ToString())
                    return DeliveryOrderStatus.WATING;
                else
                    return DeliveryOrderStatus.ERROR;
            }
        }

        public bool DeliOrderIDisNotNULL(long id)
        {
            string command = "SELECT * FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                return true;
            }
            return false;
        }
        public void addTemporaryImportForm(long id, string providerName, long createID)
        {
            string command = "INSERT INTO import_form (`id`, `provider_name`, `create_by`, `update_by`) VALUES ('" +
                id + "', '" +
                providerName + "', '" +
                createID + "', '" +
                createID + "');";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void updateTemporaryImportForm(long id, string provider)
        {
            string command = "UPDATE import_form SET provider_name = '" + provider + "' WHERE id = '" + id + "';";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void DeleteImportFormByID(string id)
        {
            string command = "DELETE FROM import_form WHERE id = " + id;
            database.executeCommand(command);
            database.closeConnection();
        }
        public void createNewImportForm(string formID, string provider, int totalAmount, long totalPrice, long id)
        {
            string command = "INSERT INTO import_form (`id`, `provider_name`, `sum_amount`, `sum_value`, `create_by`, `update_by`) VALUES ('" +
                 formID + "', '" +
                 provider + "', '" +
                 totalAmount + "', '" +
                 totalPrice + "', '" +
                 id + "', '" +
                 id + "');";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void deleteFormWithID(string id)
        {
            string command = "DELETE FROM import_form WHERE id = '"+id+"';";
            database.executeCommand(command);
            database.closeConnection();
        }

        public ObservableCollection<DeliOrder> getDeliOrderById(string id)
        {
            ObservableCollection<DeliOrder> DeliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT * FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                DeliOrder DeliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4], (DateTime)reader[5], (long)reader[6], (long)reader[7], stringToDeliveryOrderStatus(reader[8].ToString()));
                DeliOrders.Add(DeliOrder);
            }
            database.closeConnection();
            return DeliOrders;
        }

        public ObservableCollection<DeliOrder> getDeliOrderByFilterStatus(String status)
        {
            ObservableCollection<DeliOrder> deliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT * FROM import_form WHERE STATUS = '" + status + "'";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                DeliOrder deliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4], (DateTime)reader[5], (long)reader[6], (long)reader[7], stringToDeliveryOrderStatus(reader[8].ToString()));
                deliOrders.Add(deliOrder);
            }
            database.closeConnection();
            return deliOrders;
        }
        public ObservableCollection<DeliOrder> getAllDeliOrder()
        {
            ObservableCollection<DeliOrder> deliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT * FROM import_form";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                DeliOrder deliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4], (DateTime)reader[5], (long)reader[6], (long)reader[7], stringToDeliveryOrderStatus(reader[8].ToString()));
                deliOrders.Add(deliOrder);
            }
            database.closeConnection();
            return deliOrders;
        }
        public int getTotalBillByID(string id)
        {
            int value = 0;
            string command = "SELECT sum_value FROM import_form WHERE id = " + id;
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                value += (int)reader[0];
            }
            database.closeConnection();
            return value;
        }
        public long getAmountByID(string id)
        {
            long value = 0;
            string command = "SELECT sum_amount FROM import_form WHERE id = " + id;
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                value += (int)reader[0];
            }
            database.closeConnection();
            return value;
        }

        public long findCurrentMaxId()
        {
            long max = 0;
            string command = "SELECT MAX(id) FROM import_form LIMIT 1";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                if (Convert.ToString(reader[0]) != "")
                {
                    max = (long)reader[0];
                }
            }
            database.closeConnection();
            return max;
        }

        public bool pushNewOrder(DeliOrder deliOrder)
        {
            bool result = false;
            string command = $"INSERT INTO import_form (`id`, `provider_name`,`sum_amount`,`sum_value`,`create_time`,`update_time`, `create_by`, `update_by`, `status`) VALUES ('{deliOrder.id}', '{deliOrder.provider}', '{deliOrder.amount}', '{deliOrder.totalBills}', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, '1', '1', '{DeliveryOrderStatus.WATING}');";
            var reader = database.executeCommand(command);
            if (reader != null)
            {
                result = true;
            }
            database.closeConnection();
            return result;
        }
    }
}
