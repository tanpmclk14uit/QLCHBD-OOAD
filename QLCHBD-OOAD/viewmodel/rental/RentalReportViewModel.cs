using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.receipt;
using QLCHBD_OOAD.model.retal;
using QLCHBD_OOAD.view.rental;
using QLCHBD_OOAD.view.returning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalReportViewModel: BaseViewModel
    {
        
        private RentalBill _selectedRentalBill;
        public RentalBill selectedRentalBill
        {
            get => _selectedRentalBill;
            set
            {
                _selectedRentalBill = value;
                if(value != null)
                {
                    RentalDetailOrderWindown rental = new RentalDetailOrderWindown(value.id, value.guestId);
                    rental.ShowDialog();
                    OnPropertyChanged("filterListRentalBill");
                    _selectedRentalBill = null;
                }
            }
        }

        private String _seachKey;
        public String seachKey
        {
            get => _seachKey;
            set
            {
                _seachKey = value;
                OnPropertyChanged("filterListRentalBill");
                OnPropertyChanged("filterListReceipts");
                OnPropertyChanged("seachKey");
            }
        }
        public ObservableCollection<RentalBill> filterListRentalBill
        {
            get => filterByInfo();
        }
        private RentalBillRepository rentalBillReponsitory;

        public ObservableCollection<RentalBill> rentalBills
        {
            get => rentalBillReponsitory.getAllRentalBills();
        }
        private ReceiptRepository receiptRepository;

        public ObservableCollection<Receipt> filterListReceipts
        {
            get => filterReceiptByInfo(); 
        }
        public ObservableCollection<Receipt> receipts
        {
            get => receiptRepository.getAllReceipts();
        }
        private Receipt _selectedReceipt;
        public Receipt selectedReceipt
        {
            get => _selectedReceipt;
            set
            {
                _selectedReceipt = value;
                if(value != null)
                {
                    ReceiptDetailWindow receiptDetailWindow = new ReceiptDetailWindow(value);
                    receiptDetailWindow.ShowDialog();
                    OnPropertyChanged("filterListReceipts");
                    _selectedReceipt = null;
                }
                
            }
        }
        public RentalReportViewModel()
        {
            rentalBillReponsitory = RentalBillRepository.getIntance();
            receiptRepository = ReceiptRepository.getIntance();
            seachKey = "";
        }

        private ObservableCollection<RentalBill> filterByInfo()
        {
            ObservableCollection<RentalBill> filterList = new ObservableCollection<RentalBill>();

            if (seachKey == "" || seachKey[0] != '#')
            {
                foreach (var rentalBill in rentalBills)
                {

                    foreach (PropertyInfo prop in rentalBill.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        if (type == typeof(string) || type == typeof(int) || type == typeof(DateTime))
                        {
                            var rentalBill_field = prop.GetValue(rentalBill, null);
                            if (rentalBill_field != null)
                            {
                                String rentalBill_data = rentalBill_field.ToString().Trim().ToLower();
                                String keyWord = seachKey.ToLower();
                                if (rentalBill_data != null && keyWord != null)
                                {
                                    if (rentalBill_data.Contains(keyWord))
                                    {
                                        filterList.Add(rentalBill);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string id = Regex.Replace(seachKey, @"[^0-9]", string.Empty);
                if (id != "")
                {
                    filterList = rentalBillReponsitory.getAllRentalBillsById(id);
                }
            }
            return filterList;
        }

        private ObservableCollection<Receipt> filterReceiptByInfo()
        {
            ObservableCollection<Receipt> filterList = new ObservableCollection<Receipt>();

            if (seachKey == "" || seachKey[0] != '#')
            {
                foreach (var receipt in receipts)
                {

                    foreach (PropertyInfo prop in receipt.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        if (type == typeof(string) || type == typeof(int) || type == typeof(DateTime))
                        {
                            var receipt_field = prop.GetValue(receipt, null);
                            if (receipt_field != null)
                            {
                                String receipt_data = receipt_field.ToString().Trim().ToLower();
                                String keyWord = seachKey.ToLower();
                                if (receipt_data != null && keyWord != null)
                                {
                                    if (receipt_data.Contains(keyWord))
                                    {
                                        filterList.Add(receipt);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string id = Regex.Replace(seachKey, @"[^0-9]", string.Empty);
                if (id != "")
                {
                    filterList = receiptRepository.getReceiptById(id);
                }
            }
            return filterList;
        }
    }
}
