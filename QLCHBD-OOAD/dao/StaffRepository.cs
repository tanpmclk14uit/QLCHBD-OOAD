using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.dao
{
    class StaffRepository
    {
        private Db db;
        private static StaffRepository instance;

        public static StaffRepository getInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            instance = new StaffRepository();
            return instance;
        }

        public StaffRepository()
        {
            db = Db.getInstace();
        }

        public List<Staff> getAllStaff()
        {
            List<Staff> staffs = new List<Staff>();
            string command = "SELECT id, name, user_name, password, cmnd_cccd, is_manager, is_loged_in, status, birth_date, image FROM `staff`";
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                Staff staff = new Staff((long)reader[0], (string)reader[1], (string)reader[2], PasswordHash.Decrypt((string)reader[3]), (string)reader[4],(bool)reader[5], (bool)reader[6], stringToStaffStatus((string)reader[7]), (DateTime)reader[8], (string)reader[9]);
                staffs.Add(staff);
            }
            db.closeConnection();
            return staffs;
        }

        public int isHaveUserName(String username)
        {
            int i = 0;
            string command = $"SELECT count(*) FROM `staff` WHERE user_name = '{username}' and status = 'WORKING'";
            var reader = db.executeCommand(command);
            while(reader.Read())
            {
                i = Convert.ToInt32((long)reader[0]);
            }
            db.closeConnection();
            return i;
        }

        public void changePassword(String newPassword, long id)
        {
            string command = $"UPDATE staff SET password = {PasswordHash.Encrypt(newPassword)} WHERE id = {id}";
            var reader = db.executeCommand(command);
            db.closeConnection();
        }

        //username already exist
        public bool isRightPassword(String username, String password)
        {
            string command = $"SELECT staff.password FROM staff WHERE staff.user_name = '{username}'";
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                if (PasswordHash.Decrypt((string)reader[0]) == password)
                {
                    return true;
                }    
            }
            return false;
        }

        public Staff getStaffWithUsername(String username)
        {
            Staff result = new Staff();
            string command = $"SELECT id, name, user_name, password, cmnd_cccd, is_manager, is_loged_in, status, birth_date, image FROM `staff` WHERE staff.user_name = '{username}'";
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                Staff staff = new Staff((long)reader[0], (string)reader[1], (string)reader[2], PasswordHash.Decrypt((string)reader[3]), (string)reader[4], (bool)reader[5], (bool)reader[6], stringToStaffStatus((string)reader[7]), (DateTime)reader[8], (string)reader[9]);
                Replication.CopyPropertiesTo(staff, result);
            }
            db.closeConnection();
            return result;
        }

        public void deleteStaff(long id)
        {
            string command = $"DELETE FROM staff WHERE id = {id}";
            var reader = db.executeCommand(command);
            db.closeConnection();
        }

        public void addStaff(string name, string residentId, DateTime birthday, string username, string password)
        {
            string format = "yyyy-MM-dd";
            string command = $"INSERT INTO `staff` (`name`, `birth_date`, `image`, `user_name`, `password`, `is_manager`, `cmnd_cccd`, `status`, `is_loged_in`, `create_time`, `update_time`, `create_by`, `update_by`, `salary_coefficient`) VALUES ('{name}', '{birthday.ToString(format)}', 'none', '{username}', '{PasswordHash.Encrypt(password)}', '0', '{residentId}', 'WORKING', '0', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, '1', '1', '20000')";
            var reader = db.executeCommand(command);
            db.closeConnection();
        }

        private StaffStatus stringToStaffStatus(string status)
        {
            if (StaffStatus.FIRED.ToString() == status)
            {
                return StaffStatus.FIRED;
            }
            return StaffStatus.WORKING;
        }
    }
}
