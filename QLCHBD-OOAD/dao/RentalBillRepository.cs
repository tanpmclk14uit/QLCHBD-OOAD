using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.retal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
