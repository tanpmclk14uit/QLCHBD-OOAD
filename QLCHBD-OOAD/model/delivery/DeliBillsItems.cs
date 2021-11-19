using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.delivery
{
    class DeliBillsItems
    {
        private long _id;
        private long _billID;
        private string _diskName;
        private long _price;
        private long _diskID;
        private int _amount;
        private int _value;
        private DateTime _createTime;
        private DateTime _update_Time;

        public DeliBillsItems(long id, long billID, string diskName, long price, long diskID, int amount, DateTime createTime, DateTime update_Time)
        {
            _id = id;
            _billID = billID;
            _diskName = diskName;
            _price = price;
            _diskID = diskID;
            _amount = amount;
            _value = (int)(price * amount);
            _createTime = createTime;
            _update_Time = update_Time;
        }

        public DeliBillsItems(long id, long billID, string diskName, long price, long diskID, int amount)
        {
            _id = id;
            _billID = billID;
            _diskName = diskName;
            _price = price;
            _diskID = diskID;
            _amount = amount;
            _value = (int)(price * amount);
            _createTime = DateTime.Now;
            _update_Time = DateTime.Now;
        }

        public long id { get => _id;}
        public long billID { get => _billID;}
        public string diskName { get => _diskName;}
        public long price { get => _price;}
        public long diskID { get => _diskID;}
        public int amount => _amount;
        public int value => _value;
        public DateTime createTime { get => _createTime; }
        public DateTime update_Time { get => _update_Time;}

        public int setAmount { set { _amount = value; } }
    }
}
