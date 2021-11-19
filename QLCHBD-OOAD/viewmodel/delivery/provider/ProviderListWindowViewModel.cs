using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.view.delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.delivery.provider
{
    class ProviderListWindowViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;
        DeliveryProviderRepository deliveryProviderRepository;
        public static ChangePageHandler turnToProviderDetailPage;

        private List<DeliProviders> _providerList;
        public List<DeliProviders> providerList => _providerList;
         public DeliProviders SelectedProvider { get; set; }

        public ICommand SelectCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ProviderListWindowViewModel()
        {
            deliveryProviderRepository = DeliveryProviderRepository.getInstance();
            _providerList = deliveryProviderRepository.providerList();
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { closeForm(); });
            SelectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onSelect(); });
        }

        private void onSelect()
        {
            if (SelectedProvider != null)
            {
                turnToProviderDetailPage(SelectedProvider.id.ToString());
                closeForm();
            }
        }
    }
}
