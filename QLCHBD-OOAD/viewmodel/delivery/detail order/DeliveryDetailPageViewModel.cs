using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.view.delivery.DeliveryPage;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryDetailPageViewModel : BaseViewModel
    {
        public static ChangePageHandler turnToDeliveryPage;
        public static ChangePageHandler BackToDeliveryPage;
        public static ChangePageHandler turnToDeliveryCheckOutPage;


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
        public ICommand DeleteCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        public DeliveryDetailPageViewModel(string id)
        {
            deliveryOrderItemsRepository = DeliveryOrderItemsRepository.getInstance();
            deliveryOrderRepository = DeliveryOrderRepository.getInstance();
            Items = deliveryOrderItemsRepository.getItemsbyImportFormsID(id);
            _importForm = deliveryOrderRepository.getDeliOrderById(id);

            BackCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => { BackToDeliveryPage(); });
            DeleteCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => { onDelete(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return deliveryOrderRepository.ImportFormWithStatusByID(id, "WATING") && UserRoles(); }, (p) => { onConfirm(); });
        }
        private bool UserRoles()
        {
            return CurrentStaff.getInstance().currentStaff.isManager;
        }
        private void onConfirm()
        {

            MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.CONFIRM, "Parcel has been delivered?");
            myDialog.ShowDialog();
            if (myDialog.action == true)
            {
                turnToDeliveryCheckOutPage(id.ToString());
            }
            
        }

        private void onDelete()
        {
            MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.ALERT, "Parcel has been delivered?");
            myDialog.ShowDialog();
            if (myDialog.action == true)
            {
                deliveryOrderItemsRepository.removeItemByImportFormsID(id);
                deliveryOrderRepository.DeleteImportFormByID(id);
                turnToDeliveryPage(id.ToString());
            }
            
        }
    }
}
