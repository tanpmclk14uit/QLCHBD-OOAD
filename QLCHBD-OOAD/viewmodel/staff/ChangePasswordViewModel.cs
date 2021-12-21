using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.staff
{
    class ChangePasswordViewModel: BaseViewModel
    {
        public ICommand changePasswordCommand { get; set; }
        public static event updateAddStaffHandler update;
        public static event CancelAddStaffHandler cancel;
        public ChangePasswordViewModel()
        {
            this.name = CurrentStaff.getInstance().currentStaff.userName;
            this.newPassword = "";
            this.password = CurrentStaff.getInstance().currentStaff.password;
            changePasswordCommand = new RelayCommand<object>((p) => { return true; }, (p) => { changePassword(); });
        }

        private void changePassword()
        {
            if (newPassword == "" || newPassword.Length < 8 || newPassword.Contains(" "))
            {
                MessageBox.Show("Please enter right format password !");
            }
            else
            {
                StaffRepository.getInstance().changePassword(newPassword, CurrentStaff.getInstance().currentStaff.id);
                update();
                name = "";
                password = "";
                newPassword = "";
                MessageBox.Show("Change Password Success !");
                cancel();
            }
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

        private String _newPassword;
        public String newPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged("newPassword");
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
    }
}
