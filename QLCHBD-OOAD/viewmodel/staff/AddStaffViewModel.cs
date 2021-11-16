using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.staff
{
    class AddStaffViewModel: BaseViewModel
    {
        public ICommand addStaffCommand { get; set; }
        public ICommand cancelCommand { get; set; }
        public static event CancelAddStaffHandler cancel;
        public static event updateAddStaffHandler update;

        public AddStaffViewModel()
        {
            name = "";
            userName = "";
            residentId = "";
            password = "";
            birthday = DateTime.Now;
            addStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) => { addStaff(); });
            cancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { cancel(); });
        }

        private String _name;
        public String name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }

        private String _residentId;
        public String residentId
        {
            get => _residentId;
            set
            {
                _residentId = value;
                OnPropertyChanged("residentID");
            }
        }

        private String _password;
        public String password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("password");
            }
        }

        private String _userName;
        public String userName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged("userName");
            }
        }

        private DateTime _birthday;
        public DateTime birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged("birthday");
            }
        }
        private bool isValidName(string name)
        {
            if (name.Length != 0)
            {
                Regex objAlphaPattern = new Regex("^[a-zA-Z ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]*$");
                bool isValid = objAlphaPattern.IsMatch(name);
                if (isValid)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Name can not contain special character or number");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Name is empty");
                return false;
            }

        }

        private bool isValidPassword()
        {
            if (password.Contains(" ") || password.Length < 8)
            {
                MessageBox.Show("Please giving right password format !");
                return false;
            }
            return true;
        }

        private bool isValidBirthday()
        {
            if (birthday == null)
            {
                MessageBox.Show("Please choose staff birthday !");
                return false;
            }
            return true;
        }

        private bool isValidUserName()
        {
            if (userName == "" || userName.Contains(" "))
            {
                MessageBox.Show("Please giving right user name format !");
                return false;
            }
            return true;
        }

        private bool isValidResidentID()
        {
            if (residentId.Length < 9 || residentId.Length > 12 || residentId.Length == 11 || !residentId.All(char.IsDigit))
            {
                MessageBox.Show("Please giving right resident Id format !");
                return false;
            }
            return true;
        }

        private void addStaff()
        {
            if (isValidName(name) && isValidPassword() && isValidBirthday() && isValidUserName() && isValidResidentID())
            {
                StaffRepository.getInstance().addStaff(name, residentId, birthday, userName, password);
                update();
                cancel();
            }
        }    
    }
}
