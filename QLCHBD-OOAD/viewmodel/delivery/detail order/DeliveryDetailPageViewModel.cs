using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCHBD_OOAD.appUtil;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryDetailPageViewModel : BaseViewModel
    {
        public static ChangePageHandler turnToDeliveryPage;
        private DeliveryDetailPageViewModel _instance;
        public DeliveryDetailPageViewModel getInstance()
        {
            if (_instance == null)
                _instance = new DeliveryDetailPageViewModel();
            return _instance;
        }
    }
}
