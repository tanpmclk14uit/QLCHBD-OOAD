using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.dao
{
    class ReturnRepository
    {
        private Db database;

        private static ReturnRepository instance;

        public static ReturnRepository getIntance()
        {
            if (instance == null)
            {
                instance = new ReturnRepository();
            }
            return instance;
        }

        private ReturnRepository()
        {
            database = Db.getInstace();
        }



    }
}
