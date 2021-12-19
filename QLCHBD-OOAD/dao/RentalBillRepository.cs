using MySqlConnector;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.dao
{
    class RentalBillRepository
    {
        private Db database;

        private static RentalBillRepository instance;

        public static RentalBillRepository getIntance()
        {
            if (instance == null)
            {
                instance = new RentalBillRepository();
            }
            return instance;
        }

        private RentalBillRepository()
        {
            database = Db.getInstace();
        }

        
        public ObservableCollection<RentalBill> getRentalBillsThatNotReturnAllById(string id)
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = $"SELECT * FROM `rental_bill` WHERE ID = {id} and returned_all = 0";
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long)reader[1]), (DateTime)reader[2], (int)reader[4]);
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
        }
        public ObservableCollection<RentalBill> getAllRentalBills()
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = $"SELECT * FROM `rental_bill`";
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long)reader[1]), (DateTime)reader[2], (int)reader[4], getStaffNameById((long)reader[3]), (bool)reader[5]);
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
        }
        public ObservableCollection<RentalBill> getAllRentalBillsById(string id)
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = $"SELECT * FROM `rental_bill` where id = {id}";
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long)reader[1]), (DateTime)reader[2], (int)reader[4], getStaffNameById((long)reader[3]), (bool)reader[5]);
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
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
        public ObservableCollection<ImageRentalInformation> getWaitingRentalBillsByDiskId(string id)
        {
            ObservableCollection<ImageRentalInformation> rentalBills = new ObservableCollection<ImageRentalInformation>();
            string command = "SELECT rental_bill_item.rental_id, disk.name, rental_bill_item.quantity, rental_bill_item.rental_price, rental_bill_item.due_date FROM `rental_bill` inner join `disk` inner join `rental_bill_item` WHERE disk.id = " + id + " and rental_bill_item.rental_id = rental_bill.id and rental_bill.status = \"WAITING\" and disk.id = rental_bill_item.disk_id";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                ImageRentalInformation rentalBill = new ImageRentalInformation((long)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (DateTime)reader[4]);
                rentalBills.Add(rentalBill);

            }
            database.closeConnection();
            return rentalBills;
        }
        private String getNameById(long id)
        {
            String name = "";
            string command = "SELECT name FROM `guest` WHERE ID = " + id.ToString();
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                name = reader[0].ToString();
            }
            database.closeConnection();
            return name;
        }

        public long getNumberOfBillById(long id)
        {
            long number = 0;
            string command = "SELECT count(*) FROM `rental_bill` inner join `rental_bill_item` WHERE rental_bill.id = rental_bill_item.rental_id and rental_bill_item.disk_id = " + id.ToString() + " and rental_bill_item.quantity != rental_bill_item.receive_quantity";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                number = Convert.ToInt64(reader[0]);
            }
            database.closeConnection();
            return number;
        }

        public long getNumberInRentalById(long id)
        {
            long number = 0;
            string command = "SELECT sum(rental_bill_item.receive_quantity) FROM `rental_bill` inner join `rental_bill_item` WHERE rental_bill.id = rental_bill_item.rental_id and rental_bill_item.disk_id = " + id.ToString() + " and rental_bill_item.quantity != rental_bill_item.receive_quantity";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                if (!Convert.IsDBNull(reader[0]))
                {
                    number = Convert.ToInt64(reader[0]);
                }
            }
            database.closeConnection();
            return number;
        }

        public ObservableCollection<RentalBill> getRentalBillsByFilterStatus(String status)
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = "SELECT * FROM `rental_bill` WHERE STATUS = '" + status + "'";
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long)reader[1]), (DateTime)reader[2], (int)reader[4]);
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
        }
        public ObservableCollection<RentalBill> getAllRentalBillThatReturnedAll()
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = "SELECT * FROM `rental_bill` where returned_all = 0";
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long)reader[1]), (DateTime)reader[2], (int)reader[4]);
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
        }
        private MySqlConnection connection { get; set; }
        private const string connectionString = "server=localhost;user id=root;database=ooad_qlchbd;port=3306;password=123456";
        public void createNewRentalBill(RentalBill rental, long staffID, ObservableCollection<RentalBillItem> rentalBillItems)
        {
            long lastInsertId = -1;
            string command = $"INSERT INTO `rental_bill`(`guess_id`, `create_by`, `total_price`) VALUES ('{rental.guestId}','{staffID}','{rental.totalPriceSave}')";
           
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                lastInsertId = long.Parse(cmd.LastInsertedId.ToString());               
                connection.Close();
                if (lastInsertId != -1)
                {
                    foreach (RentalBillItem rentalBill in rentalBillItems)
                    {
                        createNewRentalBillItem(rentalBill, lastInsertId);
                        updateDiskRented(rentalBill);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                database.closeConnection();
            }
        }
        private void createNewRentalBillItem(RentalBillItem rentalBillItem, long rentalId)
        {   
            string format = "yyyy-MM-dd HH:mm:ss";
            string command = $"INSERT INTO `rental_bill_item`(`rental_id`, `quantity`, `rental_price`, `disk_name`, `disk_image`, `disk_id`, `due_date`, `receive_quantity` ) VALUES ('{rentalId}','{rentalBillItem.amount}','{rentalBillItem.rentalPrice}','{rentalBillItem.diskName}','{rentalBillItem.image}','{rentalBillItem.diskId}','{rentalBillItem.getDueDate().ToString(format)}','{rentalBillItem.returned}')";
            var reader = database.executeCommand(command);
            database.closeConnection();
        }
        private void updateDiskRented(RentalBillItem rentalBillItem)
        {
            string command = $"UPDATE `disk` SET rented=rented+{rentalBillItem.amount} WHERE id = {rentalBillItem.diskId};";
            var reader = database.executeCommand(command);
            database.closeConnection();
        }
        public int getAmoutOfDiskByDiskId(long id)
        {
            int count = 0;
            string command = $"Select quantity, rented from `disk` where id = {id}";
            var reader = database.executeCommand(command);
            while(reader != null && reader.Read())
            {
                count = (int)reader[0] - (int)reader[1];
            }
            return count;
        }

    }
}
