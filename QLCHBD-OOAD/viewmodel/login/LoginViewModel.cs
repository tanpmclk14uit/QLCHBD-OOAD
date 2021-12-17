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

namespace QLCHBD_OOAD.viewmodel.login
{
    class LoginViewModel: BaseViewModel
    {

        private StaffRepository staffRepository;
        private string _name;
        public string name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }

        private string _password;
        public string password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("password");
            }
        }

        public LoginViewModel()
        {
            name = "";
            password = "";
            staffRepository = StaffRepository.getInstance();
            loginCommand = new RelayCommand<object>((p) => { return true; }, (p) => { login(); });
        }    

        public ICommand loginCommand { get; set;}
        public static event Login loginHanlder;
        public void login()
        {
            if (staffRepository.isHaveUserName(name) == 1)
            {
                if (staffRepository.isRightPassword(name, password))
                {
                    Staff staff = staffRepository.getStaffWithUsername(name);
                    CurrentStaff.getInstance().setStaff(staff);
                    MessageBox.Show(CurrentStaff.getInstance().currentStaff.userName);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginHanlder();
                }
                else MessageBox.Show("Wrong username or password");
            }    
            else MessageBox.Show("Wrong username or password");
        }
    }
}
