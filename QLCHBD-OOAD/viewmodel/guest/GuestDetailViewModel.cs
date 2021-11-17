using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.view.rental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        public long allRenting { get; set; }
        public long allCurrentRenting { get; set; }
        public long allOverdueRenting { get; set; }
        public ICommand Edit { get; set; }



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


        public void setGuest(Guest guest)
        {
            this.guest = guestReponsitory.findDetailGuestById(guest.id.ToString());
            staffName = guestReponsitory.findStaffNameById(guest.createById);
            allRenting = guestReponsitory.countAllRentedBook(guest.id.ToString());
            allCurrentRenting = guestReponsitory.countCurrentRentingBookByStatus(guest.id.ToString(), "WAITING");
            allOverdueRenting = guestReponsitory.countCurrentRentingBookByStatus(guest.id.ToString(), "OVERDUE");

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

       
        private GuestDetailViewModel()
        {
            guestReponsitory = GuestReponsitory.getInstance();
            Edit = new RelayCommand<object>((p) => { return true; }, (p) => { onEditCommandClick(guest); });
        }

        private void onEditCommandClick(Guest guest)
        {
            RentalAddNewMember rental = new RentalAddNewMember(guest);
            rental.Show();
        }
    }
}
