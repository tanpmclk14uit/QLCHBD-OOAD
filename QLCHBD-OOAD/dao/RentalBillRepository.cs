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
            if(instance == null)
            {
                instance = new RentalBillRepository();
            }
            return instance;
        }

        private RentalBillRepository()
        {
            database = Db.getInstace();
        }

        private RentalBillStatus stringToRentalBillStatus(String status)
        {
            if(status == RentalBillStatus.OVERDUE.ToString())
            {
                return RentalBillStatus.OVERDUE;
            }
            else
            {
                if(status == RentalBillStatus.RECEIVEDALL.ToString())
                {
                    return RentalBillStatus.RECEIVEDALL;
                }
                else
                {
                    return RentalBillStatus.WAITING;
                }
            }
        }
        public ObservableCollection<RentalBill> getRentalBillsById(string id)
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = "SELECT * FROM `rental_bill` WHERE ID = "+ id;
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long)reader[1]), (DateTime)reader[2], (int)reader[4], stringToRentalBillStatus(reader[5].ToString()));
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
        }

        public ObservableCollection<ImageRentalInformation> getWaitingRentalBillsByDiskId(string id)
        {
            ObservableCollection<ImageRentalInformation> rentalBills = new ObservableCollection<ImageRentalInformation>();
            string command = "SELECT rental_bill_item.rental_id, disk.name, rental_bill_item.quantity, rental_bill_item.rental_price, rental_bill_item.due_date FROM `rental_bill` inner join `disk` inner join `rental_bill_item` WHERE disk.id = 1 and disk.id = rental_bill.id and rental_bill.status = \"WAITING\" and disk.id = rental_bill_item.disk_id";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                MessageBox.Show(((DateTime)reader[4]).ToString());
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
            while (reader.Read())
            {
                name = reader[0].ToString();
            }
            database.closeConnection();
            return name;
        }
        public ObservableCollection<RentalBill> getRentalBillsByFilterStatus(String status)
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = "SELECT * FROM `rental_bill` WHERE STATUS = '" + status+"'";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long) reader[1]), (DateTime)reader[2], (int)reader[4], stringToRentalBillStatus(reader[5].ToString()));
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
        }
        public ObservableCollection<RentalBill> getAllRentalBill()
        {
            ObservableCollection<RentalBill> rentalBills = new ObservableCollection<RentalBill>();
            string command = "SELECT * FROM `rental_bill`";
            var reader = database.executeCommand(command);
            while (reader.Read())
            {               
                
                RentalBill rentalBill = new RentalBill((long)reader[0], (long)reader[1], getNameById((long)reader[1]), (DateTime)reader[2], (int)reader[4], stringToRentalBillStatus(reader[5].ToString()));
                rentalBills.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBills;
        }
    }
}
