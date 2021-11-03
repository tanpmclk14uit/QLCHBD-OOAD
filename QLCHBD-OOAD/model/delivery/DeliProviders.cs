using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.delivery
{ 
    class DeliProviders
    {
        private long _id;
        private string _providerName;
        private int _providerNumber;
        private string _providerMail;
        private string _providerAddress;
        private DateTime _createTime;
        private DateTime _updateTime;
        private long _createID;
        private long _updateID;

        public long id => _id;
        public int providerNumber => _providerNumber;
        public string providerMail => _providerMail;
        public string providerAddres => _providerAddress;
        public string providerName => _providerName;
        public String createTime => _createTime.ToShortDateString();
        public String updateTime => _updateTime.ToShortDateString();
        public long createID => _createID;
        public long updateID => _updateID;


        public void UpdateTime(long ID)
        {
            _updateTime = DateTime.Now;
            _updateID = ID;
        }
        public DeliProviders(long id, string providerName, int providerNumber, string providerMail, string providerAddress, long createID)
        {
            this._id = id;
            this._providerName = providerName;
            this._providerNumber = providerNumber;
            this._providerMail = providerMail;
            this._providerAddress = providerAddress;
            this._createTime = DateTime.Now;
            this._updateTime = DateTime.Now;
            this._createID = createID;
            this._updateID = createID;
        }
        public DeliProviders(long id, string providerName, int providerNumber, string providerMail, string providerAddress)
        {
            this._id = id;
            this._providerName = providerName;
            this._providerNumber = providerNumber;
            this._providerMail = providerMail;
            this._providerAddress = providerAddress;
            this._createTime = DateTime.Now;
            this._updateTime = DateTime.Now;
            this._createID = 1;
            this._updateID = 1;
        }

        public DeliProviders(long id, string providerName, int providerNumber, string providerMail, string providerAddress, DateTime createTime, DateTime updateTime, long createID, long updateID)
        {
            _id = id;
            _providerName = providerName;
            _providerNumber = providerNumber;
            _providerMail = providerMail;
            _providerAddress = providerAddress;
            _createTime = createTime;
            _updateTime = updateTime;
            _createID = createID;
            _updateID = updateID;
        }
    }
}
