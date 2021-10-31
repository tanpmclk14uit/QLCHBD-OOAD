using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCHBD_OOAD.appUtil;

namespace QLCHBD_OOAD.model.delivery
{ 
    class DeliBills
    {
        private long _id;
        private string _providerName;
        private string _paymentDate;
        private DeliveryBillStatus _status;
        private DateTime _createTime;
        private DateTime _updateTime;
        private long _createID;
        private long _updateID;

        public DeliBills(long id, string providerName, string paymentDate, DeliveryBillStatus status, DateTime createTime, DateTime updateTime, long createID, long updateID)
        {
            _id = id;
            _providerName = providerName;
            _paymentDate = paymentDate;
            _status = status;
            _createTime = createTime;
            _updateTime = updateTime;
            _createID = createID;
            _updateID = updateID;
        }
        public DeliBills(long id, string providerName, string paymentDate, long createID, long updateID)
        {
            _id = id;
            _providerName = providerName;
            _paymentDate = paymentDate;
            _status = DeliveryBillStatus.UNPAID;
            _createTime = DateTime.Now;
            _updateTime = DateTime.Now;
            _createID = createID;
            _updateID = updateID;
        }

        public long id => _id;
        public string providerName => _providerName;
        public string paymentDate => _paymentDate;
        public DeliveryBillStatus status => _status;
        public String createTime => _createTime.ToShortDateString();
        public String updateTime => _updateTime.ToShortDateString();
        public long createID => _createID;
        public long updateID => _updateID;


    }
}
