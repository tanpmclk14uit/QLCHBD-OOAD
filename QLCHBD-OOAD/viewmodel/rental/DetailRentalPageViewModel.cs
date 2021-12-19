using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.model.retal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class DetailRentalPageViewModel: BaseViewModel
    {
   

        public static event TurnBackPageHandler turnBackPageHandler;

        private DetailRentalBillReponsitory detailRentalBillReponsitory;

        private ObservableCollection<RentalBillItem> _rentalBillItems;
        public ObservableCollection<RentalBillItem> rentalBillItems
        {
            get => _rentalBillItems;
        }

        private string _createBy;

        public string createBy { get => _createBy; }

        private string _createDate;
        public string createDate { get => _createDate; }

        private Guest _guest;

        public Guest guest
        {
            get => _guest;
        }

        private string _orderId;
        public string orderId
        {
            get => _orderId;
        }
        public ICommand Back { get; set; }
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

        public RentalBill currentRentalBill;
        private RentalBillRepository rentalBillRepository;
        public DetailRentalPageViewModel(long rentalId, long guestId)
        {            
            Back =  new RelayCommand<object>((p) => { return true; }, (p) => { backToALlRentalPage(); });
            _orderId = rentalId.ToString();
            rentalBillRepository = RentalBillRepository.getIntance();
            currentRentalBill = rentalBillRepository.getAllRentalBillsById(_orderId)[0];
            detailRentalBillReponsitory = DetailRentalBillReponsitory.getIntance();           
            _guest = detailRentalBillReponsitory.getGuestById(guestId);
            if (_guest.isMember)
            {
                isMember = Visibility.Visible;
            }
            else
            {
                isMember = Visibility.Hidden;
            }
            _createBy = detailRentalBillReponsitory.getOrderCreateBy(rentalId);
            _createDate = detailRentalBillReponsitory.getOrderCreateDate(rentalId);
            _rentalBillItems = new ObservableCollection<RentalBillItem>();
            _rentalBillItems = detailRentalBillReponsitory.getAllRentalBillItemByRentalId(rentalId);
        }

        private void backToALlRentalPage()
        {           
            turnBackPageHandler();
        }
        public void deleteOrder()
        {
            rentalBillRepository.deleteRentalById(currentRentalBill.id);
        }
    }
}
