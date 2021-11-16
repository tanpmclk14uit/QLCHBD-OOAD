using QLCHBD_OOAD.appUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.staff
{
    class Staff
    {
        public Staff(long id, String name, String userName, String password, String residentId, bool isManager, bool isLogedIn, StaffStatus status, DateTime birthDay)
        {
            this._id = id;
            this._name = name;
            this._password = password;
            this._userName = userName;
            this._residentId = residentId;
            this._isManager = isManager;
            this._isLogedIn = isLogedIn;
            this._status = status;
            this._birthDay = birthDay;
        }

        private long _id;
        public long id
        {
            get => _id;
        }

        private int _salaryCoefficient;
        public int salaryCoefficient
        {
            get => _salaryCoefficient;
        }

        private String _name;
        public String name
        {
            get => _name;
        }

        private String _userName;
        public String userName
        {
            get => _userName;
        }

        private String _password;
        public String password
        {
            get => _password;
            set
            {
                _password = value;
            }
        }

        private String _residentId;
        public String residentId
        {
            get => _residentId;
        }

        private StaffStatus _status;
        public StaffStatus status
        {
            get => _status;
            set
            {
                _status = value;
            }
        }

        private DateTime _birthDay;
        public String birthDay
        {
            get => _birthDay.ToShortDateString();
        }

        private bool _isManager;
        public bool isManager
        {
            get => _isManager;
        }

        private bool _isLogedIn;
        public bool isLogedIn
        {
            get => _isLogedIn;
        }

        private DateTime _createTime;
        public String createTime
        {
            get => _createTime.ToShortDateString();
        }

        private DateTime _updateTime;
        public String updateTime
        {
            get => _updateTime.ToShortDateString();
        }

        private long _createBy;
        public long createBy
        {
            get => _createBy;
        }

        private long _updateBy;
        public long updateBy
        {
            get => _updateBy;
        }
    }
}
