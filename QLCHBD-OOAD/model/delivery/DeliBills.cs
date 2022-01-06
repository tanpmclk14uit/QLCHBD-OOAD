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
        private long _importFormID;
        private string _providerName;
        private DateTime _paymentDate;
        private long _sumAmmount;
        private long _sumValue;
        private DeliveryBillStatus _status;
        private String _stringStatus;
        private DateTime _createTime;
        private DateTime _updateTime;
        private long _createID;
        private long _updateID;

        public DeliBills(long id, long importFormID, string providerName, DateTime paymentDate, long sumAmmount, long sumValue, DeliveryBillStatus status, DateTime createTime, DateTime updateTime, long createID, long updateID)
        {
            _id = id;
            _importFormID = importFormID;
            _providerName = providerName;
            _paymentDate = paymentDate;
            _sumAmmount = sumAmmount;
            _sumValue = sumValue;
            _status = status;
            _createTime = createTime;
            _updateTime = updateTime;
            _createID = createID;
            _updateID = updateID;
            _stringStatus = status.ToString();
        }

        public long Id { get => _id;}
        public long ImportFormID { get => _importFormID;}
        public string ProviderName { get => _providerName;}
        public string PaymentDate { get => _paymentDate.ToShortDateString();}
        public DateTime PaymentDateButInDateTimeFormat { get => _paymentDate;}
        public long SumAmmount { get => _sumAmmount;}
        public long SumValue { get => _sumValue;}
        public DeliveryBillStatus Status { get => _status;}
        public String stringStatus
        {
            get => _stringStatus;
            set
            {
                if (value == DeliveryBillStatus.PAID.ToString())
                {
                    _stringStatus = "PAID";
                }
                else
                {
                    _stringStatus = "UNPAID";
                }
            }

        }
        public DateTime CreateTime { get => _createTime;}
        public DateTime UpdateTime { get => _updateTime;}
        public long CreateID { get => _createID;}
        public long UpdateID { get => _updateID;}
    }
}
