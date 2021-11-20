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
        public static DeliveryBillRepository getInstance()
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
                database.closeConnection();
                return true;
            }
            return false;
        }
        public string getImportBillStatusByImportFormID(string id)
        {
            string command = "SELECT status FROM import_bill WHERE import_form_id = " + id;
            var reader = database.executeCommand(command);
            if (reader.Read())
            {
                var billStatus = (string)reader[0];
                database.closeConnection();
                return billStatus;
            }
            database.closeConnection();
            return null;
        }

        public void addTemporaryBills(long id, string importID, string providerName, long sumAmount, long sumValue, long createID)
        {
            string command = "INSERT INTO import_bill (`id`, `import_form_id`, `provider_name`, `sum_amount`, `sum_value`, `create_by`, `update_by`) VALUES ('" +
                id + "', '" +
                importID + "', '" +
                providerName + "', '" +
                sumAmount + "', '" +
                sumValue + "', '" +
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
        public void updateTemporaryBillsWithImportFormID(string id)
        {
            string command = "UPDATE import_bill SET status = 'PAID' WHERE import_form_id = '" + id + "';";
            database.executeCommand(command);
            database.closeConnection();
        }

    }
}
