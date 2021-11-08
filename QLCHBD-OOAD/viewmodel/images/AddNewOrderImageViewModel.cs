using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class AddNewOrderImageViewModel: BaseViewModel
    {
        public ICommand removeCommand { get; set; }

        public static event DeleteOrderDiskItemHandler deleteOrderItem;

        private Images _selectedItem;
        public  Images selectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
            }
        }

        private List<Images> _lstOnOrder;
        public List<Images> lstOnOrder
        {
            get => _lstOnOrder;
            set
            {
                _lstOnOrder = value;
                OnPropertyChanged("lstOnOrder");
            }
        }

        public List<Images> _lstOnOrderBackup;
        public List<Images> lstOnOrderBackup
        {
            get => _lstOnOrderBackup;
            set
            {
                _lstOnOrderBackup = value;
            }
        }

        public int totalAmount
        {
            get 
            {
                int amount = 0;
                foreach (Images item in lstOnOrder)
                {
                    amount += Convert.ToInt32(item.orderAmount);
                }
                return amount;
            }
        }

        public int totalValue
        {
            get
            {
                int value = 0;
                foreach (Images item in lstOnOrder)
                {
                    value += Convert.ToInt32(item.value);
                }
                return value;
            }
        }

        private List<Images> defaultList = new List<Images>();
       
        public AddNewOrderImageViewModel(List<Images> lstOnOrder)
        {
            this.lstOnOrder = lstOnOrder;
            this.defaultList = lstOnOrder;
            removeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteOrderDiskItem(selectedItem); deleteOrderItem(); });
        }

        private void DeleteOrderDiskItem(Images selectedItem)
        {
            _lstOnOrder.Remove(selectedItem);
            updateList();
        }


        public void updateList()
        {
            OnPropertyChanged("lstOnOrder");
            OnPropertyChanged("totalValue");
            OnPropertyChanged("totalAmount");
        }

        public bool isChange()
        {
            return lstOnOrder == defaultList;
        }
    }
}
