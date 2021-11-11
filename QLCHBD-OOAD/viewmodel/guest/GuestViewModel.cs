using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.viewmodel.rental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.guest
{
    class GuestViewModel: BaseViewModel
    {

        private GuestReponsitory guestReponsitory;
        public static event CloseForm closeForm;
        

        public ICommand Cancel { get; set; }
        public ICommand Confirm { get; set; }

        private Guest _guest;
        public Guest guest
        {
            get => _guest;
            set 
            {
                _guest = value;
                
                OnPropertyChanged("guest");
            }
        }

        public GuestViewModel( Guest guest)
        {
            if (guest != null)
            {
                this.guest = guest;
            }
            else
            {
                _guest = new Guest();
            }
            Confirm = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirmClick(_guest); });
            guestReponsitory = GuestReponsitory.getInstance();


        }
        private bool isValidName(string name)
        {
            if(name.Length != 0)
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

        private bool isValidIdentityCard(string identityCard)
        {
            if (identityCard.Length != 0)
            {
                if(identityCard.Length == 9 || identityCard.Length == 12)
                {
                    Regex objAlphaPattern = new Regex(@"^[0-9]*$");
                    bool isValid = objAlphaPattern.IsMatch(identityCard);
                    if (isValid)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Identity card can not contain character");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Identity card can have 9 or 12 character");
                    return false;
                }
               
            }
            else
            {
                MessageBox.Show("Identity card is empty");
                return false;
            }
        }
        private bool isValidBirthDate(DateTime birthDate)
        {
            DateTime date = new DateTime(2005, 1, 1);
           if (birthDate <= date)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Customer years old must be more than 16");
                return false;
            }
        }
        private bool isValidAddress(string address)
        {
            if (address.Length > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Address can not empty");
                return false;
            }
        }

        private void onConfirmClick(Guest guest)
        {
            if (isValidName(guest.name) && isValidIdentityCard(guest.cmnd) && isValidAddress(guest.address) && isValidBirthDate(guest.birthDate))
            {
                long createdId = createNewGuest(guest);   
                if (createdId != -1)
                {
                    RentalAddMemberViewModel.getIntance().setGuest(createdId.ToString());
                    MessageBox.Show("Create guest success!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    closeForm();
                }
                else
                {
                    MessageBox.Show("There are some server error! turn back later!", "Success", MessageBoxButton.OK, MessageBoxImage.Error);
                }            
                
            }
        }
            
           
        private long createNewGuest(Guest guest)
        {
            return guestReponsitory.createGuest(guest);
        }
    }
}
