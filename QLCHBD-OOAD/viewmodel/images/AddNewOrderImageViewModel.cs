using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.viewmodel.images
{
    class AddNewOrderImageViewModel: BaseViewModel
    {

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
