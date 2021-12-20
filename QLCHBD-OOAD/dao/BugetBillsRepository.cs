using QLCHBD_OOAD.model.buget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.dao
{
    class BugetBillsRepository
    {
        private Db db;
        public static BugetBillsRepository instance;

        public static BugetBillsRepository getInstance()
        {
            if (instance == null)
            {
                instance = new BugetBillsRepository();
                return instance;
            }
            return instance;
        }

        public BugetBillsRepository()
        {
            db = Db.getInstace();
        }

        public List<BugetBills> getBugetBillsInRange(DateTime A, DateTime B)
        {
            List<BugetBills> bugetBills = new List<BugetBills>();
            while (A.CompareTo(B) <= 0) 
            {
                bugetBills.Add(GetBugetBillsByDate(A));
                A = A.AddDays(1);
            }
            return bugetBills;
        }
        public BugetBills GetBugetBillsByDate(DateTime date)
        {
            BugetBills bills = new BugetBills(getBillIDWithDay("import_bill", date),
                                    getBillIDWithDay("reserve_bill", date),
                                    getSumValue("sum_value", "import_bill", date) - getAdditionalFee(date),
                                    getSumValue("total_price", "rental_bill", date),
                                    date);
            return bills;
        }

        private List<long> getBillIDWithDay(string table, DateTime date)
        {
            List<long> IDs = new List<long>();
            string command = "SELECT id FROM " + table + " WHERE " +
                "DATE_FORMAT(create_time, '%d') = '" + date.Day + "' " +
                "AND DATE_FORMAT(create_time, '%m') = '" + date.Month + "' " +
                "AND DATE_FORMAT(create_time, '%y') = '" + date.Year + "' IS NOT NULL;";
            var reader = db.executeCommand(command);

            while (reader.Read())
            {
                IDs.Add((long)reader[0]);
            }
            db.closeConnection();
            return IDs;
        }
        private long getSumValue(string valueName, string table, DateTime date)
        {
            long sum = 0;
            string command = "SELECT SUM(" + valueName + ") FROM " + table + " WHERE " +
                "DATE_FORMAT(create_time, '%d') = '" + date.Day + "' " +
                "AND DATE_FORMAT(create_time, '%m') = '" + date.Month + "' " +
                "AND DATE_FORMAT(create_time, '%y') = '" + date.Year + "'";
            var reader = db.executeCommand(command);

            try
            {
                sum += (long)reader[0];
            }
            catch (Exception e)
            {
                sum = 0;
            }

            db.closeConnection();
            return sum;
        }
        private long getAdditionalFee(DateTime date)
        {
            long sum = 0;
            string command = "SELECT SUM(additional_fee) FROM receipt WHERE " +
                "DATE_FORMAT(create_time, '%d') = '" + date.Day + "' " +
                "AND DATE_FORMAT(create_time, '%m') = '" + date.Month + "' " +
                "AND DATE_FORMAT(create_time, '%y') = '" + date.Year + "';";
            var reader = db.executeCommand(command);

            try
            {
                sum += (long)reader[0];
            }
            catch (Exception e)
            {
                sum = 0;
            }
            db.closeConnection();
            return sum;
        }




    }


}
