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
            }
        }
       
        public AddNewOrderImageViewModel(List<Images> lstOnOrder)
        {
            this.lstOnOrder = lstOnOrder;
        }

        public void updateList()
        {
            OnPropertyChanged("lstOnOrder");

        }

    }
}
