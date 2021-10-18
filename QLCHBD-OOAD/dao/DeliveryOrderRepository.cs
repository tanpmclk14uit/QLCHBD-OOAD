using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public ObservableCollection<DeliOrder> getDeliOrderById(string id)
        {
            ObservableCollection<DeliOrder> DeliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT * FROM import_form WHERE ID = " + id;
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                DeliOrder DeliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), getAmountByID((long)reader[0]), (DateTime)reader[2], (DateTime)reader[3], (long)reader[4], (long)reader[5], stringToDeliveryOrderStatus(reader[6].ToString()), getTotalBillByID((long)reader[0]));
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
                DeliOrder deliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), getAmountByID((long)reader[0]), (DateTime)reader[2], (DateTime)reader[3], (long)reader[4], (long)reader[5], stringToDeliveryOrderStatus(reader[6].ToString()), getTotalBillByID((long)reader[0]));
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
                DeliOrder deliOrder = new DeliOrder((long)reader[0], reader[1].ToString(), getAmountByID((long)reader[0]), (DateTime)reader[2], (DateTime)reader[3], (long)reader[4], (long)reader[5], stringToDeliveryOrderStatus(reader[6].ToString()), getTotalBillByID((long)reader[0]));
                deliOrders.Add(deliOrder);
            }
            database.closeConnection();
            return deliOrders;
        }
        public long getTotalBillByID(long id)
        {
            long value = 0;
            ObservableCollection<DeliOrder> deliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT disk_price FROM import_form_item WHERE import_form_id = " + id.ToString();
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                value += (long)reader[0];
            }
            database.closeConnection();
            return value;
        }
        public long getAmountByID(long id)
        {
            long value = 0;
            ObservableCollection<DeliOrder> deliOrders = new ObservableCollection<DeliOrder>();
            string command = "SELECT COUNT(*) FROM import_form_item WHERE import_form_id = " + id.ToString();
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                value += (long)reader[0];
            }
            database.closeConnection();
            return value;
        }
    }
}
