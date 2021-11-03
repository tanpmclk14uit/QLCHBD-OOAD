using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.view.delivery.Add_Order;
using static QLCHBD_OOAD.dao.DeliveryProviderRepository;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryaAddOrderFormViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;

        private DeliveryOrderRepository deliveryOrderRepository;
        private DeliveryOrderItemsRepository deliveryOrderItemsRepository;
        private DeliveryProviderRepository deliveryProviderRepository;
        public DeliBillsItems SelectedItems { get; set; }

        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        private ObservableCollection<DeliOrderItems> _importItems;
        public ObservableCollection<DeliOrderItems> importItems { get => _importItems; set { _importItems = value; OnPropertyChanged(); } }
        public ObservableCollection<DeliOrderItems> filterBillItems()
        {
            importItems = deliveryOrderItemsRepository.getItemsbyImportFormsID(id.ToString());
            OnPropertyChanged();
            return importItems;
        }
        public DeliveryaAddOrderFormViewModel()
        {
            deliveryProviderRepository = getIntance();
            deliveryOrderRepository = DeliveryOrderRepository.getIntance();
            deliveryOrderItemsRepository = DeliveryOrderItemsRepository.getIntance();
            setUpStatusses();
            generateID();
            RemoveCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onRemove(); });
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) => { openAddWindow(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onCancel(); });

        }
        //-------------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------------
        private void ChangeProvider(string provider)
        {
            deliveryOrderRepository.updateTemporaryImportForm(id, provider);
        }

        //-------------------------------------------------------------------------------------------------
        private ObservableCollection<String> _selectedStatuses;
        public ObservableCollection<String> selectedStatuses => _selectedStatuses;

        private String _selectedStatus;
        public String selectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                ChangeProvider(value);
                OnPropertyChanged("ChangeProvider");
                OnPropertyChanged("selectedStatus");
                OnPropertyChanged("onConfirm");
            }
        }
        //-------------------------------------------------------------------------------------------------
        private ObservableCollection<DeliProviders> _providerCombobox;
        private void setUpStatusses()
        {
            _selectedStatuses = new ObservableCollection<string>();
            _providerCombobox = getProviderList();
            foreach (var provider in _providerCombobox)
            {
                _selectedStatuses.Add(provider.providerName);
            }
            selectedStatus = _selectedStatuses[0];
            OnPropertyChanged("selectedStatuses");
        }
        private ObservableCollection<DeliProviders> getProviderList()
        {
            ObservableCollection<DeliProviders> deliProviders = deliveryProviderRepository.providerList();
            return deliProviders;
        }
        //-------------------------------------------------------------------------------------------------
        private long _id;
        public long id => _id;
        private void generateID()
        {
            Random random = new Random();
            _id = (long)random.Next();
            while (deliveryOrderRepository.DeliOrderIDisNotNULL(_id))
            {
                _id = (long)random.Next();
            }
            deliveryOrderRepository.addTemporaryImportForm(id, selectedStatus, 1);
        }
        //-------------------------------------------------------------------------------------------------
        private void openAddWindow()
        {
            AddNewDiskDeliveryWindow window = new AddNewDiskDeliveryWindow(id);
            window.ShowDialog();
            filterBillItems();
        }

        private void onRemove()
        {
            deliveryOrderItemsRepository.removeItemByID(SelectedItems.id.ToString());
            filterBillItems();
        }

        private void onConfirm()
        {
            closeForm();
        }

        private void onCancel()
        {
            deliveryOrderItemsRepository.removeItemByImportFormsID(id.ToString());
            deliveryOrderRepository.DeleteImportFormByID(id.ToString());
            closeForm();
        }


    }
}
