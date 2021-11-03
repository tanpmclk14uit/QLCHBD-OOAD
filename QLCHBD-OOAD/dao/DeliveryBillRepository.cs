using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.dao
{
    class DeliveryBillRepository
    {
        private Db database;
        private static DeliveryBillRepository instance;

        private DeliveryBillRepository()
        {
            database = Db.getInstace();
        }
        public static DeliveryBillRepository getIntance()
        {
            if (instance == null)
                instance = new DeliveryBillRepository();
            return instance;
        }
        public bool DeliBillsIDisNotNULL(long id)
        {
            string command = "SELECT * FROM import_bill WHERE ID = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                return true;
            }
            return false;
        }

        public void addTemporaryBills(long id, long createID)
        {
            string command = "INSERT INTO import_bill (`id`, `provider_name`, `create_by`, `update_by`) VALUES ('" +
                id + "', '" +
                "JustInTemporary', '" +
                createID + "', '" +
                createID +"');";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void DeleteTemporaryBills(string id)
        {
            string command = "DELETE FROM import_bill WHERE (id = '" + id + "');";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void updateTemporaryBills(long id, string provider)
        {
            string command = "UPDATE import_bill SET provider_name = '" + provider + "' WHERE id = '" + id + "';";
            database.executeCommand(command);
            database.closeConnection();
        }

    }
}
