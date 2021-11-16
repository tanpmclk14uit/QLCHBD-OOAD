using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.view.guest;
using QLCHBD_OOAD.view.rental;
using QLCHBD_OOAD.viewmodel.rental;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

        private ObservableCollection<Guest> _guests;
        public ObservableCollection<Guest> guests
        {
            get  {
                _guests = guestReponsitory.getAllGuest();
                return _guests;
            }
        }


        public ICommand Cancel { get; set; }
        public ICommand Confirm { get; set; }       
        public ICommand AddMember { get; set; }

        private Guest _guest;
        public Guest guest
        {
            get => _guest;
            set 
            {                                    
                if(value!= null)
                {
                   
                    onGuestSelected(value);
                }
                
            }
        }

        private void onGuestSelected(Guest guest)
        {
            GuestDetailInformation guestDetail = new GuestDetailInformation(guest);
            guestDetail.Show();
            _guest = null;
            OnPropertyChanged("guest");
        }

        private String _seachKey;
        public String seachKey
        {
            get => _seachKey;
            set
            {
                _seachKey = value;
                OnPropertyChanged("filterListGuest");
                OnPropertyChanged("seachKey");
            }
        }
        public ObservableCollection<Guest> filterListGuest
        {
            get => filterByInfo();
        }

        private ObservableCollection<Guest> filterByInfo()
        {
            ObservableCollection<Guest> filterList = new ObservableCollection<Guest>();
              foreach (var guest in guests)
              {

                  foreach (PropertyInfo prop in guest.GetType().GetProperties())
                  {
                      var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                      if (type == typeof(string) || type == typeof(int) || type == typeof(DateTime))
                      {
                          var guest_field = prop.GetValue(guest, null);
                          if (guest_field != null)
                          {
                              String guest_data = guest_field.ToString().Trim().ToLower();
                              String keyWord = seachKey.ToLower();
                              if (guest_data != null && keyWord != null)
                              {
                                  if (guest_data.Contains(keyWord))
                                  {
                                      filterList.Add(guest);
                                      break;
                                  }
                              }
                          }
                      }

                  }
              }
            return filterList;
        }
        private bool isUpdate;

        public GuestViewModel( Guest guest)
        {
            if (guest != null)
            {
                this.guest = guest;
                isUpdate = true;
            }
            else
            {
                _guest = new Guest();
                isUpdate = false;
            }
            Confirm = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirmClick(_guest); });
            AddMember = new RelayCommand<object>((p) => { return true; }, (p) => { onAddMemberClick(); });
            guestReponsitory = GuestReponsitory.getInstance();
            _guests = guestReponsitory.getAllGuest();
            seachKey = "";
        }
        private void onAddMemberClick()
        {
            RentalAddNewMember rental = new RentalAddNewMember();
            rental.ShowDialog();            
            OnPropertyChanged("filterListGuest");
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
                        if (guestReponsitory.findRentalGuestByIdCard(identityCard) == null || isUpdate)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Identity card is exist!");
                            return false;
                        }                 
                        
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
                if (!isUpdate)
                {
                    createNewGuest(guest);
                }
                else
                {
                    updateGuest(guest);
                }
                                     
            }
        }
        private void updateGuest(Guest guest)
        {
            long updateId = guestReponsitory.updateGuest(guest);
            if (updateId != -1)
            {
                RentalAddMemberViewModel.getIntance().setGuest(updateId.ToString());
                MessageBox.Show("Update guest success!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                closeForm();
            }
            else
            {
                MessageBox.Show("There are some server error! turn back later!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
            
        private void createNewGuest(Guest guest)
        {
            long createdId = guestReponsitory.createGuest(guest);
            if (createdId != -1)
            {
                RentalAddMemberViewModel.getIntance().setGuest(createdId.ToString());
                MessageBox.Show("Create guest success!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                closeForm();
            }
            else
            {
                MessageBox.Show("There are some server error! turn back later!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }
    }

}
