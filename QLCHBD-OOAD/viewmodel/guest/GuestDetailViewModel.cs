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

        public static GuestDetailViewModel getInstance()
        {
            if(instance== null)
            {
                instance = new GuestDetailViewModel();
            }
            return instance;
        }

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
            this.guest = guest;
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

        }
    }
}
