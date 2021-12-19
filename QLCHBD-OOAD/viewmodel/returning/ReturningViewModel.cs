using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
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

namespace QLCHBD_OOAD.viewmodel.returning
{
    class ReturningViewModel : BaseViewModel
    {
        public ICommand Back { get; set; }


        public static event TurnBackPageHandler turnBackPageHandler;

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
        private DetailRentalBillReponsitory detailRentalBillReponsitory;

        private ObservableCollection<RentalBillItem> _rentalBillItems;
 
        private ObservableCollection<ReceiptItemViewModel> _receiptItems;
        public ObservableCollection<ReceiptItemViewModel> receiptItems
        {
            get => _receiptItems;
            set => _receiptItems = value;
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
        private long rentalId;
        public ReturningViewModel(long rentalId, long guestId)
        {
            Back = new RelayCommand<object>((p) => { return true; }, (p) => { backToALlRentalPage(); });           
            _orderId = rentalId.ToString();
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
            this.rentalId = rentalId;
            _createBy = detailRentalBillReponsitory.getOrderCreateBy(rentalId);
            _createDate = detailRentalBillReponsitory.getOrderCreateDate(rentalId);           
            _rentalBillItems = detailRentalBillReponsitory.getAllRentalBillItemByRentalId(rentalId);
            _receiptItems = mapToReceiptItems(_rentalBillItems);
            ReceiptItemViewModel.onCalculateFee += ReceiptItemViewModel_onCalculateFee;
        }
        public void returnProcess()
        {
            makeNewReceipt();
            updateRentedDisk();
            updateReturnedByRentalBillItem();
            updateReturnedAllFieldByRentalId();
            MyDialog myDialog = new MyDialog(MyDialogStyle.INFORMATION, "Returned success");
            myDialog.ShowDialog();
            turnBackPageHandler();
        }

        private void makeNewReceipt()
        {
            //do make new recepit

        }
        public void updateReturnedAllFieldByRentalId()
        {
            if (detailRentalBillReponsitory.isRentalReturnedAll(long.Parse(orderId))) {
                detailRentalBillReponsitory.updateReturnAll(long.Parse(orderId));
            }
        }
        public bool isReturnListEmpty()
        {
            foreach (ReceiptItemViewModel receipt in receiptItems)
            {
                if (receipt.isSelected && (receipt.returned != 0 || receipt.lost != 0))
                {
                    return false;
                }
                
            }
            return true;
        }
        private void updateRentedDisk()
        {
            foreach (ReceiptItemViewModel receipt in receiptItems)
            {

                if (receipt.isSelected && receipt.returned != 0)
                {
                    detailRentalBillReponsitory.updateRentedById(receipt.diskId, receipt.returned);
                }   
                if(receipt.isSelected && receipt.lost != 0)
                {
                    detailRentalBillReponsitory.updateRentedById(receipt.diskId, receipt.lost);
                    detailRentalBillReponsitory.updateTotalDiskWhenLost(receipt.diskId, receipt.lost);
                }

            }
        }        
       
        private void updateReturnedByRentalBillItem()
        {
            foreach (ReceiptItemViewModel receipt in receiptItems)
            {
                if(receipt.isSelected == true && receipt.returned !=0)
                {
                    detailRentalBillReponsitory.updateReturnById(receipt.id, receipt.returned);
                }                
                if(receipt.isSelected && receipt.lost != 0)
                {
                    detailRentalBillReponsitory.updateLostQuantityById(receipt.id, receipt.lost);
                }
            }
            _rentalBillItems = detailRentalBillReponsitory.getAllRentalBillItemByRentalId(rentalId);
            _receiptItems = mapToReceiptItems(_rentalBillItems);
            OnPropertyChanged("receiptItems");
        }
        private double _totalFee;

        public string totalFee
        {
            get
            {
                if(_totalFee == 0)
                {
                    return "0";
                }
                return _totalFee.ToString("#,###");
            }
        }

        private void ReceiptItemViewModel_onCalculateFee()
        {
            double totalFee = 0;
            foreach (var receipt in receiptItems)
            {
                if (receipt.isSelected)
                {
                    totalFee += receipt.additionalFee;
                }
            }
            _totalFee = totalFee;
            OnPropertyChanged("totalFee");
        }

        private ObservableCollection<ReceiptItemViewModel> mapToReceiptItems(ObservableCollection<RentalBillItem> rentalBillItems)
        {
            ObservableCollection<ReceiptItemViewModel> receiptItems = new ObservableCollection<ReceiptItemViewModel>();
            foreach (var rentalItem in rentalBillItems)
            {
                int amount = rentalItem.amount - rentalItem.returned - rentalItem.lost;
                ReceiptItemViewModel receipt = new ReceiptItemViewModel(rentalItem.id, rentalItem.diskId, rentalItem.diskName, rentalItem.rentalPrice, amount, rentalItem.getDueDate());
                receiptItems.Add(receipt);
            }
            return receiptItems;
        }
        private void backToALlRentalPage()
        {
            turnBackPageHandler();
        }
        public void selectAll()
        {
            foreach(var receipt in receiptItems)
            {
                receipt.isSelected = true;
            }
        }
        public void unSelectAll()
        {
            foreach (var receipt in receiptItems)
            {
                receipt.isSelected = false;
            }
        }
        public void returnAll()
        {
            foreach (var receipt in receiptItems)
            {
                receipt.isSelected = true;
                receipt.returned = receipt.amount - receipt.lost;
            }
        }
        public void unCheckReturnAll()
        {
            foreach (var receipt in receiptItems)
            {
                receipt.isSelected = false;
                receipt.returned = 0;
            }
        }

    }
}
