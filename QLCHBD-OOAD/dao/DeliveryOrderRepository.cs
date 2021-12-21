using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private FileStream file;
        private static DeliveryOrderRepository instance;

        public static DeliveryOrderRepository getInstance()
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
        public void updateStatusDELIVERED(string id)
        {
            string command = "UPDATE import_form SET status = 'DELIVERED' WHERE id = '" + id + "'";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void updateStatusERROR(string id)
        {
            string command = "UPDATE import_form SET status = 'ERROR' WHERE id = '" + id + "'";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void updateStatusWAITING(string id)
        {
            string command = "UPDATE import_form SET status = 'WATING' WHERE id = '" + id + "'";
            database.executeCommand(command);
            database.closeConnection();
        }

        public bool DeliOrderIDisNotNULL(long id)
        {
            string command = "SELECT * FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                database.closeConnection();
                return true;
            }
            database.closeConnection();
            return false;
        }
        public void DeleteImportFormByID(long id)
        {
            string command = "DELETE FROM import_form WHERE id = " + id;
            database.executeCommand(command);
            database.closeConnection();
        }
        public void createNewImportForm(string formID, string provider, int totalAmount, long totalPrice, long id, string image)
        {
            string command = "INSERT INTO import_form (`id`, `provider_name`, `sum_amount`, `sum_value`, `image`, `create_by`, `update_by`) VALUES ('" +
                 formID + "', '" +
                 provider + "', '" +
                 totalAmount + "', '" +
                 totalPrice + "', '" +
                 image + "', '" +
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
        public string getImportFormStatusWithID(string id)
        {
            string command = "SELECT status FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                return reader[0].ToString();
                database.closeConnection();
            }
            database.closeConnection();
            return null;
        }
        public bool ImportFormWithStatusByID(string id, string status)
        {
            string command = "SELECT status FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                string strStatus = reader[0].ToString();
                database.closeConnection();
                if (strStatus.Equals(status)) return true;
                else return false;
                
            }
            database.closeConnection();
            return false;
        }

        public DeliOrder getDeliOrderById(string id)
        {
            DeliOrder DeliOrders;
            string command = "SELECT * FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                DeliOrders = new DeliOrder((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4], (DateTime)reader[5], (long)reader[6], (long)reader[7], stringToDeliveryOrderStatus(reader[8].ToString()), (string)reader[9]);
                DeliOrders.setImage = checkImageExists(DeliOrders.id);
                database.closeConnection();
                return DeliOrders;
            }
            database.closeConnection();
            return null;
        }
        public ObservableCollection<DeliOrder> filterDeliOrderByID(string id)
        {
            ObservableCollection<DeliOrder> deliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT * FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                DeliOrder deliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4], (DateTime)reader[5], (long)reader[6], (long)reader[7], stringToDeliveryOrderStatus(reader[8].ToString()), (string)reader[9]);
                deliOrder.setImage = checkImageExists(deliOrder.id);
                deliOrders.Add(deliOrder);
            }
            database.closeConnection();
            return deliOrders;
        }
        public ObservableCollection<DeliOrder> getDeliOrderByFilterStatus(String status)
        {
            ObservableCollection<DeliOrder> deliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT * FROM import_form WHERE STATUS = '" + status + "'";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                DeliOrder deliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4], (DateTime)reader[5], (long)reader[6], (long)reader[7], stringToDeliveryOrderStatus(reader[8].ToString()), (string)reader[9]);
                checkImageExists(deliOrder.id);
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
                DeliOrder deliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4], (DateTime)reader[5], (long)reader[6], (long)reader[7], stringToDeliveryOrderStatus(reader[8].ToString()), (string)reader[9]);
                deliOrder.setImage = checkImageExists(deliOrder.id);
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






        private string checkImageExists(long id)
        {
            string imagePath = getProviderImagebyImportFormID(id.ToString());
            if (File.Exists(imagePath))
            {
                file = File.OpenRead(imagePath);
                file.Close();
            }
            else if (imagePath.Contains(@"\Assets") || imagePath.Contains("QLCHBD"))
            {
                imagePath = "/QLCHBD-OOAD;component/assets/img_noImage.png";
            }

            return imagePath;
        }

        public string getProviderImagebyImportFormID(string id)
        {
            string command = "SELECT provider.image FROM import_form LEFT JOIN provider ON provider.name = import_form.provider_name AND import_form.id = " + id + " IS NOT NULL;";
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                string image = (string)reader[0];
                return image;
            }
            database.closeConnection();
            return "/QLCHBD-OOAD;component/assets/img_noImage.png";
        }
    }
}
