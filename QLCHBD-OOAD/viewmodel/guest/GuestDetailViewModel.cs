using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.viewmodel.guest
{
    class GuestDetailViewModel: BaseViewModel
    {
        private static GuestDetailViewModel instance;
        private GuestReponsitory guestReponsitory;

        public static GuestDetailViewModel getInstance()
        {
            if(instance== null)
            {
                instance = new GuestDetailViewModel();
            }
            return instance;
        }
        public string staffName { get; set; }

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
        public void setGuest(Guest guest)
        {
            this.guest = guestReponsitory.findDetailGuestById(guest.id.ToString());
            staffName = guestReponsitory.findStaffNameById(guest.createById);
            if (guest != null)
            {
                if (guest.isMember)
                {
                    isMember = Visibility.Visible;
                }
                else
                {
                    isMember = Visibility.Hidden;
                }
            }
        }

        private Visibility _isMember;
        public Visibility isMember
        {
            get
            {
                return _isMember;
            }
            set
            {
                _isMember = value;
                OnPropertyChanged("isMember");
            }
        }
        private GuestDetailViewModel()
        {
            guestReponsitory = GuestReponsitory.getInstance();
        }
    }
}
