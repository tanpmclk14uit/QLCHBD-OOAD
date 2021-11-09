using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.delivery
{

    public class DeliOrderItems
    {
        private long _id;
        private long _deliID;
        private string _diskName;
        private long _imPrice;
        private long _diskID;
        private int _amount;
        private int _value;
        private long _IDbyProvider;
        private DateTime _createTime;
        private DateTime _updateTime;


        public long id => _id;
        public long IDbyProvider => _IDbyProvider;
        public long deliID => _deliID;
        public string diskName => _diskName;
        public long diskID => _diskID;
        public int value => _value;
        public String createTime => _createTime.ToShortDateString();
        public String updateTime => _updateTime.ToShortDateString();

        public int Amount { get => _amount; set { _amount = value; value = (int)(_amount * _imPrice); } }
        public long imPrice { get => _imPrice; set { _imPrice = value; value = (int)(_amount * _imPrice); } }

        public void UpdateTime()
        {
            _updateTime = DateTime.Now;
        }
        public DeliOrderItems(long id, long deliID, int amount, long diskID, string diskName, long imPrice, long providerID)
        {
            this._id = id;
            this._deliID = deliID;
            this._diskName = diskName;
            this._imPrice = imPrice;
            this._diskID = diskID;
            this._amount = amount;
            this._value = (int)(amount * imPrice);
            _IDbyProvider = providerID;
            this._createTime = DateTime.Now;
            this._updateTime = DateTime.Now;
        }

        public DeliOrderItems(long id, long deliID, string diskName, long imPrice, long diskID, DateTime createTime, DateTime updateTime)
        {
            this._id = id;
            this._deliID = deliID;
            this._diskName = diskName;
            this._imPrice = imPrice;
            this._diskID = diskID;
            this._createTime = createTime;
            this._updateTime = updateTime;
        }

    }
}
