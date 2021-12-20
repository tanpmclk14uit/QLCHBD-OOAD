using QLCHBD_OOAD.model.receipt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.dao
{
    class ReceiptRepository
    {
        private Db database;
        private static ReceiptRepository intance;
        public static ReceiptRepository getIntance()
        {
            if(intance == null)
            {
                intance = new ReceiptRepository();
            }
            return intance;
        }
        private ReceiptRepository()
        {
            database = Db.getInstace();
        }
        public long createNewReceipt(Receipt receipt)
        {
            long id;
            string command = $"INSERT INTO `receipt`(`id`, `guess_id`, `create_by`, `additional_fee`) VALUES ('{receipt.id}','{receipt.guestId}','{receipt.createBy}','{receipt.additionalFee}')";
            id = database.excuteInsertCommand(command);
            database.closeConnection();
            return id;
        }
        public void createNewReceiptItem(ReceiptItem receiptItem)
        {
            string command = $"INSERT INTO `receipt_item`( `receipt`, `returned_quantity`, `disk_id`, `disk_name`, `delay_date`, `lost_quantity`) VALUES ('{receiptItem.receipt}','{receiptItem.returnedQuantity}','{receiptItem.diskId}','{receiptItem.diskName}','{receiptItem.delayDays}','{receiptItem.lostQuantity}')";
            database.executeCommand(command);
            database.closeConnection();
        }
        public ObservableCollection<Receipt> getAllReceipts()
        {
            ObservableCollection<Receipt> receipts = new ObservableCollection<Receipt>();
            string command = $"SELECT * FROM `receipt`";
            var reader = database.executeCommand(command);
            while(reader!=null && reader.Read())
            {
                Receipt receipt = new Receipt((long)reader[0], getNameById((long)reader[1]), (DateTime)reader[2], getStaffNameById((long)reader[3]), (int)reader[4]);
                receipts.Add(receipt);
            }
            return receipts;
        }
        private string getNameById(long id)
        {
            String name = "";
            string command = $"SELECT name FROM `guest` WHERE ID = {id}";
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                name = reader[0].ToString();
            }
            database.closeConnection();
            return name;
        }
        private string getStaffNameById(long id)
        {
            String name = "";
            string command = $"SELECT name FROM `staff` WHERE ID = {id}";
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                name = reader[0].ToString();
            }
            database.closeConnection();
            return name;
        }
    }

}
