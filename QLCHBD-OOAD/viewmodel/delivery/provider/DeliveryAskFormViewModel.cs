using QLCHBD_OOAD.appUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QLCHBD_OOAD.view.delivery.DeliveryPage;
using QLCHBD_OOAD.view.delivery.component;
using System.Windows;

namespace QLCHBD_OOAD.viewmodel.delivery.Component
{
    class DeliveryAskFormViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;
        public ICommand ImportCommand { get; set; }
        public ICommand ByHandCommand { get; set; }
        public ICommand CancleCommand { get; set; }

        public DeliveryAskFormViewModel()
        {
            CancleCommand = new RelayCommand<object>((p) => { return true; }, (p) => { closeForm(); });
            ImportCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UseFile(); });
            ByHandCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UseHand(); });
        }
        private void UseFile()
        {
            MessageBox.Show("Use hand still being making color process, try again another time", "Error");
        }
        private void UseHand()
        {
            closeForm();
            AddNewProvider newProviderWindow = new AddNewProvider();
            newProviderWindow.ShowDialog();
        }
    }
}
