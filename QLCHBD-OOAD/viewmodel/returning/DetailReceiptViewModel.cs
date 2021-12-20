using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.model.receipt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.viewmodel.returning
{
    class DetailReceiptViewModel: BaseViewModel
    {

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

        public ObservableCollection<ReceiptItem> receiptItems
        {
            get => receiptRepository.getAllReceiptItemsById(currentCeipt.id);
        }
        private DetailRentalBillReponsitory detailRentalBillReponsitory;
        private ReceiptRepository receiptRepository;
        private Receipt currentCeipt;
        public DetailReceiptViewModel(Receipt receipt)
        {
            _orderId = receipt.id.ToString();
            currentCeipt = receipt;
            receiptRepository = ReceiptRepository.getIntance();
            detailRentalBillReponsitory = DetailRentalBillReponsitory.getIntance();
            _guest = detailRentalBillReponsitory.getGuestById(receipt.guestId);
            if (_guest.isMember)
            {
                isMember = Visibility.Visible;
            }
            else
            {
                isMember = Visibility.Hidden;
            }
            _createBy = receipt.staffName;
            _createDate = receipt.createTime;
            
        }

    }
}
