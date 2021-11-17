using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryDetailPageViewModel : BaseViewModel
    {
        public static ChangePageHandler turnToDeliveryPage;

        private DeliveryOrderRepository deliveryOrderRepository;
        private DeliOrder _importForm;
        public long id => _importForm.id;
        public string provider => _importForm.provider;
        public long totalBills => _importForm.totalBills;
        public long amount => _importForm.amount;
        public string dayCreate => _importForm.createTime;
        public long idCreate_By => _importForm.idCreate_By;
        public string image => _importForm.image;
        public String stringStatus => _importForm.stringStatus;

        private DeliveryOrderItemsRepository deliveryOrderItemsRepository;
        public ObservableCollection<DeliOrderItems> Items { get;}

        public ICommand BackCommand { get; set; }

        public DeliveryDetailPageViewModel(string id)
        {
            deliveryOrderItemsRepository = DeliveryOrderItemsRepository.getIntance();
            deliveryOrderRepository = DeliveryOrderRepository.getIntance();
            Items = deliveryOrderItemsRepository.getItemsbyImportFormsID(id);
            _importForm = deliveryOrderRepository.getDeliOrderById(id);

            BackCommand = new RelayCommand<object>((p) => { return true; }, (p) => { turnToDeliveryPage(); });
        }
    }
}
