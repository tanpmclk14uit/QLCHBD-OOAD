using QLCHBD_OOAD.model.Guest;
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
    class DetailRentalBillReponsitory
    {
        private Db database;

        private static DetailRentalBillReponsitory instance;

        public static DetailRentalBillReponsitory getIntance()
        {
            if (instance == null)
            {
                instance = new DetailRentalBillReponsitory();
            }
            return instance;
        }

        private DetailRentalBillReponsitory()
        {
            database = Db.getInstace();
        }

        public Guest getGuestById(long id)
        {
            Guest guest = null;
            string command = "SELECT id, cmnd_cccd, address, name  FROM guest WHERE ID = " + id;
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                guest = new Guest((long)reader[0], (string)reader[1], (string)reader[2], (string)reader[3]);
            }
            database.closeConnection();
            return guest;
        }

        public string getOrderCreateDate(long id)
        {
            string createDate = null;
            string command = "SELECT create_time  FROM rental_bill WHERE ID = " + id;
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                DateTime dateTime = (DateTime) reader[0];
                createDate = dateTime.ToShortDateString();
            }
            database.closeConnection();
            return createDate;
        }

        public string getOrderCreateBy(long id)
        {
            string createBy = null;
            string command = "SELECT create_by  FROM rental_bill WHERE ID = " + id;
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                createBy = reader[0].ToString();
            }
            database.closeConnection();
            return createBy;
        }

        public ObservableCollection<RentalBillItem> getAllRentalBillItemByRentalId(long id)
        {
            ObservableCollection<RentalBillItem> rentalBillItems = new ObservableCollection<RentalBillItem>();
            string command = "SELECT disk_id, disk_name, quantity, receive_quantity, due_date, rental_price FROM `rental_bill_item` where rental_id =" + id;
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {

                RentalBillItem rentalBill = new RentalBillItem((long)reader[0], (string)reader[1],(int)reader[2],(int)reader[3], (DateTime)reader[4],(int)reader[5]);
                rentalBillItems.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBillItems;
        }
    }
}
