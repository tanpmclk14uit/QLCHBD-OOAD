using QLCHBD_OOAD.model.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.dao
{
    class GuestReponsitory
    {
        private Db db;
        private static GuestReponsitory instance;

        public static GuestReponsitory getInstance()
        {
            if (instance == null)
            {
                instance = new GuestReponsitory();
            }           
            return instance;
        }

        private GuestReponsitory()
        {
            db = Db.getInstace();
        }

        public Guest findRentalGuestById(string id)
        {
            Guest guest = null;
            string command = $"SELECT `id`, `cmnd_cccd`, `address`, `name`, birth_date FROM `guest` WHERE id = {id}";
            var reader = db.executeCommand(command);
            if (reader != null && reader.Read())
            {
                guest = new Guest((long)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (DateTime)reader[4]);
            }
            db.closeConnection();
            return guest;
        }

    }
}
