using QLCHBD_OOAD.appUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalAddPageViewModel: BaseViewModel
    {
        public static TurnBackPageHandler turnBackToRentalAllOrders;

        public ICommand Cancel { get; set; }

        public RentalAddPageViewModel()
        {
            Cancel = new RelayCommand<object>((p) => { return true; }, (p) => { turnBackToRentalAllOrders(); });
        }

    }
}
