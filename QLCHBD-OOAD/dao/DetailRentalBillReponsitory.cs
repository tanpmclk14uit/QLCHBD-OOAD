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
            string command = "SELECT id, cmnd_cccd, address, name, membership  FROM guest WHERE ID = " + id;
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                guest = new Guest((long)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (bool)reader[4]);
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
            string command = $"SELECT name FROM staff JOIN rental_bill ON staff.id = rental_bill.create_by WHERE rental_bill.id = {id}";
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
            string command = "SELECT disk_id, disk_name, quantity, receive_quantity, due_date, rental_price, id, lost_quantity FROM `rental_bill_item` where rental_id =" + id;
            var reader = database.executeCommand(command);
            while (reader != null && reader.Read())
            {
                RentalBillItem rentalBill = new RentalBillItem((long) reader[6],(long)reader[0], (string)reader[1],(int)reader[2],(int)reader[3], (DateTime)reader[4],(int)reader[5], (int) reader[7]);
                if(rentalBill.returned + rentalBill.lost  < rentalBill.amount)
                {
                    if(rentalBill.getDueDate() < DateTime.Now)
                    {
                        rentalBill.setRentalBIllStatus(appUtil.RentalBillStatus.OVERDUE);
                    }
                    else
                    {
                        rentalBill.setRentalBIllStatus(appUtil.RentalBillStatus.WAITING);
                    }
                    
                }
                else
                {
                    rentalBill.setRentalBIllStatus(appUtil.RentalBillStatus.RETURNED);
                }
                rentalBillItems.Add(rentalBill);
            }
            database.closeConnection();
            return rentalBillItems;
        }
        public void updateReturnById(long id, int returned)
        {
            string command = $"UPDATE `rental_bill_item` SET receive_quantity = receive_quantity + {returned} where id = {id}";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void updateRentedById(long id, int returned)
        {
            string command = $"UPDATE `disk` SET rented = rented - {returned} where id = {id}";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void updateLostQuantityById(long id, int lost)
        {
            string command = $"UPDATE `rental_bill_item` SET lost_quantity = lost_quantity + {lost} where id = {id}";
            database.executeCommand(command);
            database.closeConnection();
        }
        public void updateTotalDiskWhenLost(long id, int lost)
        {
            string command = $"UPDATE `disk` SET quantity = quantity - {lost} where id = {id}";
            database.executeCommand(command);
            database.closeConnection();
        }
        public bool isRentalReturnedAll(long id)
        {
            string command = $"SELECT COUNT(id) FROM rental_bill_item WHERE rental_id = {id} and quantity <> receive_quantity + lost_quantity;";
            var reader = database.executeCommand(command);
            if (reader != null && reader.Read())
            {
                
                long count = (long)reader[0];
                database.closeConnection();
                return count == 0;
            }
            else
            {
                database.closeConnection();
                return false;
            }
        }
        public void updateReturnAll(long id)
        {   
            string command = $"UPDATE `rental_bill` SET returned_all = 1 WHERE id ={id} ";
            database.executeCommand(command);
            database.closeConnection();
        }

       
    }
} 
