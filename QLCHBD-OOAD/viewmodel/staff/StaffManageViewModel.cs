using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
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
    class StaffManageViewModel: BaseViewModel
    {
        public ICommand changePasswordCommand { get; set; }
        public ICommand deleteStaffCommand { get; set; }

        public ICommand addStaffCommand { get; set; }


        private List<Staff> _lstStaffs;
        public List<Staff> lstStaffs
        {
            get => _lstStaffs;
            set
            {
                _lstStaffs = value;
                OnPropertyChanged("lstStaffs");
            }
        }

        private Staff _selectedItem;
        public Staff selectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (value != null)
                {
                    name = _selectedItem.name;
                    password = _selectedItem.password;
                    if (_selectedItem.isManager)
                    {
                        image = "https://icons.iconarchive.com/icons/aha-soft/free-large-boss/256/Admin-icon.png";
                    }
                    else image = "https://icons.iconarchive.com/icons/aha-soft/people/256/engineer-icon.png";
                }
                else
                {
                    image = "/QLCHBD-OOAD;component/Assets/staff-ava.png";
                }
                OnPropertyChanged("selectedItem");
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

        private String _image;
        public String image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged("image");
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

        private StaffManageViewModel()
        {
            lstStaffs = StaffRepository.getInstance().getAllStaff();
            name = "";
            password = "";
            newPassword = "";
            image = "/QLCHBD-OOAD;component/Assets/staff-ava.png";
            changePasswordCommand = new RelayCommand<object>((p) => { return true; }, (p) => { changePassword();});
            deleteStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) => { deleteStaff(); });
            addStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) => { addStaff(); });
            AddStaffViewModel.update += updateList;
            ChangePasswordViewModel.update += updateList;
        }

        private void updateList()
        {
            lstStaffs = StaffRepository.getInstance().getAllStaff();
            OnPropertyChanged("password");
        }    

        private void addStaff()
        {
            AddNewStaffWindow addNewStaffWindow = new AddNewStaffWindow();
            addNewStaffWindow.Show();
        }
        private void changePassword()
        {
            if (name == "")
            {
                MessageBox.Show("Please choose the staff !");
            }
            else if (newPassword == "" || newPassword.Length < 8 || newPassword.Contains(" "))
            {
                MessageBox.Show("Please enter right format password !");
            }
            else
            {
                StaffRepository.getInstance().changePassword(newPassword, selectedItem.id);
                selectedItem = null;
                lstStaffs = StaffRepository.getInstance().getAllStaff();
                name = "";
                password = "";
                newPassword = "";
                MessageBox.Show("Change Password Success !");
            }
        }

        private void deleteStaff()
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Please choose the staff first !");
            } else if (selectedItem.isLogedIn) 
            {
                MessageBox.Show("Cant delete account currently loged in !");
            }
            else
            {
                StaffRepository.getInstance().deleteStaff(selectedItem.id);
                selectedItem = null;
                lstStaffs = StaffRepository.getInstance().getAllStaff();
                name = "";
                password = "";
                newPassword = "";
                MessageBox.Show("Delete Staff Success !");
            }
        }    

        private static StaffManageViewModel _instance;

        public static StaffManageViewModel getIntance()
        {
            if (_instance == null)
            {
                _instance = new StaffManageViewModel();
            }
            return _instance;
        }


    }
}
