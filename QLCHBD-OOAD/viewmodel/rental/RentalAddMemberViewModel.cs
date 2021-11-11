using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.viewmodel.guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalAddMemberViewModel: BaseViewModel
    {
        
        private static RentalAddMemberViewModel instance;
        public static RentalAddMemberViewModel getIntance()
        {
            if(instance == null)
            {
                instance = new RentalAddMemberViewModel();
            }
            return instance;
        }
        public static event GuestTranferInformation guestTranferInformation;

       
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
        public ICommand Confirm { get; set; }
        private string _keyword;
        public string keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                if (value != null)
                {
                    string id = Regex.Replace(value, @"[^0-9]", string.Empty);
                    if (id != "")
                    {
                        guest = findGuestById(id);
                    }
                }                
            }
        }
        private GuestReponsitory guestReponsitory;

        public void setGuest(string id)
        {
            guest = findGuestById(id);
        }
        private RentalAddMemberViewModel()
        {
            guestReponsitory = GuestReponsitory.getInstance();            
            Confirm = new RelayCommand<object>((p) => { return true; }, (p) => { guestTranferInformation(guest); });
        }


        public Guest findGuestById(string id)
        {
            Guest guest = guestReponsitory.findRentalGuestById(id);
            return guest;
        }


    }
}
