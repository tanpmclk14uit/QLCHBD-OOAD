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
        public Staff() { }
        public Staff(long id, String name, String userName, String password, String residentId, bool isManager, bool isLogedIn, StaffStatus status, DateTime birthday, string image)
        {
            this._id = id;
            this._name = name;
            this._password = password;
            this._userName = userName;
            this._residentId = residentId;
            this._isManager = isManager;
            this._isLogedIn = isLogedIn;
            this._status = status;
            this._birthday = birthday;
            this._image = image;
        }

        private long _id;
        public long id
        {
            get => _id;
            set
            {
                _id = value;
            }
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
            set
            {
                _name = value;
            }
        }

        private String _userName;
        public String userName
        {
            get => _userName;
            set
            {
                _userName = value;
            }
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
            set
            {
                _residentId = value;
            }
        }

        private String _image;
        public String image
        {
            get => _image;
            set
            {
                _image = value;
            }
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

        private DateTime _birthday;
        public String birthday
        {
            get => _birthday.ToShortDateString();
        }

        private bool _isManager;
        public bool isManager
        {
            get => _isManager;
            set
            {
                _isManager = value;
            }
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
