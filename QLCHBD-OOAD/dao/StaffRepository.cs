using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
