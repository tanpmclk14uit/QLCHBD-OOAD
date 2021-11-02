using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCHBD_OOAD.appUtil;

namespace QLCHBD_OOAD.model.delivery
{
    class DeliOrder
    {
        private long _id;
        private string _provider;
        private long _totalBills;
        private long _amount;
        private DateTime _createTime;
        private DateTime _updateTime;
        private long _idCreate_By;
        private long _idUpdate_By;
        private DeliveryOrderStatus _status;
        private String _stringStatus;

        public long id { get => _id; }
        public string provider { get => _provider; }
        public long totalBills { get => _totalBills; }
        public long amount { get => _amount; }

        public long setAmount { set { _amount = value; } }
        public long SetTotalBills { set { _totalBills = value; } }
        public String createTime { get => _createTime.ToShortDateString(); }
        public String updateTime { get => _updateTime.ToShortDateString(); }

        public long idCreate_By { get => _idCreate_By; }
        public long idUpdate_By { get => _idUpdate_By; }

        public DeliveryOrderStatus status { get => _status; }

        public String stringStatus
        {
            get => _stringStatus;
            set
            {
                if (value == DeliveryOrderStatus.DELIVERED.ToString())
                {
                    _stringStatus = "Delivered";
                }
                else
                {
                    if (value == DeliveryOrderStatus.WATING.ToString())
                    {
                        _stringStatus = "Waiting";
                    }
                    else
                    {
                        _stringStatus = "Cancel";
                    }
                }
            }
        }


        public DeliOrder(long id, string provide, long amount, DateTime createTime, DateTime updateTime, long idCreate_By, long idUpdate_By, DeliveryOrderStatus status, long totalBills)
        {
            this._id = id;
            this._provider = provide;
            this._amount = amount;
            this._createTime = createTime;
            this._updateTime = updateTime;
            this._idCreate_By = idCreate_By;
            this._idUpdate_By = idUpdate_By;
            this._status = status;
            this._totalBills = totalBills;
            this._stringStatus = status.ToString();
        }
        public DeliOrder(long id, string provide, long amount, long idCreate_By, long idUpdate_By, DeliveryOrderStatus status)
        {
            this._id = id;
            this._provider = provide;
            this._amount = amount;
            this._createTime = DateTime.Now;
            this._updateTime = DateTime.Now;
            this._idCreate_By = idCreate_By;
            this._idUpdate_By = idUpdate_By;
            this._status = status;
            this._totalBills = 0;
            this._stringStatus = status.ToString();
        }

        public DeliOrder(long id, string provide, long amount, long idCreate_By)
        {
            this._id = id;
            this._provider = provide;
            this._amount = amount;
            this._createTime = DateTime.Now;
            this._updateTime = DateTime.Now;
            this._idCreate_By = idCreate_By;
            this._idUpdate_By = idCreate_By;
            this._status = DeliveryOrderStatus.WATING;
            this._totalBills = 0;
            this._stringStatus = status.ToString();
        }

    }
}
