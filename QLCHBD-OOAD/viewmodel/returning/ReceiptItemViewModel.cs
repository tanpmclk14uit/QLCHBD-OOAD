using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.viewmodel.returning
{
    class ReceiptItemViewModel: BaseViewModel
    {
        public static event CalculateFee onCalculateFee;
        private long _diskId;
        public long diskId
        {
            get => _diskId;
        }
        private string _diskName;
        public string diskName 
        {
            get => _diskName;
        }
        private int _rentalPrice;
        public string rentalPrice
        {
            get => _rentalPrice.ToString("#,###");
        }
        public String strAdditionalFee
        {
            get
            {
                if(_additionalFee != 0)
                {
                    return _additionalFee.ToString("#,###") + " VNĐ";
                }
                else
                {
                    return "0 VNĐ";
                }
                
            }
        }
        private int _amount;
        public int amount
        {
            get => _amount;
        }
        private int _returned;

        private bool _isSelected;
        public bool isSelected 
        {   get => _isSelected;
            set
            {
                _isSelected = value;
                onCalculateFee();
                OnPropertyChanged("isSelected");
            }
        }

      
        private int _overDueDays;
        public int overDueDays
        {
            get => _overDueDays;            
        }
        private double _additionalFee;
        public double additionalFee
        {
            get => _additionalFee;
            set
            {
                _additionalFee = value;
                onCalculateFee();
                OnPropertyChanged("strAdditionalFee");
            }

            
        }
        private int _lost;
        public int lost
        {
            get => _lost;
            set
            {
                if(value <= amount - returned)
                {
                    _lost = value;
                    if (_returned == 0 && lost == 0)
                    {
                        isSelected = false;
                    }
                    else
                    {
                        isSelected = true;
                    }
                    additionalFee = caculatorFee();
                }
                else
                {                    
                    MessageBox.Show("Lost and return must equals amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public int returned
        {
            get => _returned;
            set
            {
                if(value <= amount - lost)
                {
                    _returned = value;
                    if(_returned == 0 && lost == 0)
                    {
                        isSelected = false;
                    }
                    else
                    {
                        isSelected = true;
                    }
                    additionalFee = caculatorFee();
                    OnPropertyChanged("returned");
                }
                else
                {
                    MessageBox.Show("Lost and return must equals amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private double caculatorFee()
        {
            double lostFee = lost * 5 * _rentalPrice + lost * overDueDays * 1.5 * _rentalPrice;
            double overDueFee = returned * overDueDays * 1.5 * _rentalPrice;
            double total = lostFee + overDueFee;
            return total;
        }
        public ReceiptItemViewModel(long diskId, string diskName, int rentalPrice, int amount,  DateTime dueDate)
        {
            this._diskId = diskId;
            this._diskName = diskName;
            this._amount = amount;
            this._rentalPrice = rentalPrice;
            double overDueDays =(DateTime.Now - dueDate).TotalDays;
            if (overDueDays > 0)
            {
                if(overDueDays > Convert.ToInt32(overDueDays))
                {
                    this._overDueDays = Convert.ToInt32(overDueDays) + 1;
                }
                else
                {
                    this._overDueDays = Convert.ToInt32(overDueDays);
                }
            }
            else
            {
                this._overDueDays = 0;
            }
            _additionalFee = 0;

            
            
        }
    }
}
