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
        private DeliveryBilingItemsRepository deliveryItemsRepository;
        private DeliveryProviderRepository deliveryProviderRepository;
        private DeliveryBillRepository deliveryBillRepository;
        public DeliBillsItems SelectedItems { get; set; }

        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        private ObservableCollection<DeliBillsItems> _billItems;
        public ObservableCollection<DeliBillsItems> billItems { get => _billItems; set { _billItems = value; OnPropertyChanged(); } }
        public ObservableCollection<DeliBillsItems> filterBillItems()
        {
            billItems = deliveryItemsRepository.getItemsbyBillsID(id.ToString());
            OnPropertyChanged();
            return billItems;
        }
        public DeliveryaAddOrderFormViewModel()
        {
            deliveryProviderRepository = DeliveryProviderRepository.getIntance();
            deliveryItemsRepository = DeliveryBilingItemsRepository.getIntance();
            deliveryOrderRepository = DeliveryOrderRepository.getIntance();
            deliveryBillRepository = DeliveryBillRepository.getIntance();
            deliveryOrderItemsRepository = DeliveryOrderItemsRepository.getIntance();
            setUpStatusses();
            generateID();
            filterBillItems();
            deliveryBillRepository.addTemporaryBills(id, 1);
            RemoveCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onRemove(); });
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) => { openAddWindow(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onCancel(); });

        }

        private void ChangeProvider(string provider)
        {
            deliveryBillRepository.updateTemporaryBills(id, provider);
        }

        public ObservableCollection<DeliProviders> filterListProvider => filterProvider();

        private ObservableCollection<DeliProviders> filterProvider()
        {
            throw new NotImplementedException();
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
        private ObservableCollection<Provider> _providerCombobox;
        private void setUpStatusses()
        {
            _selectedStatuses = new ObservableCollection<string>();
            _providerCombobox = getProviderList();
            foreach (var provider in _providerCombobox)
            {
                _selectedStatuses.Add(provider.name);
            }
            selectedStatus = _selectedStatuses[0];
            OnPropertyChanged("selectedStatuses");
        }
        private class Provider
        {
            long _id;
            string _name;
            public long id => _id;
            public string name => _name;

            public Provider(long id, string name)
            {
                _id = id;
                _name = name;
            }
        }
        private ObservableCollection<Provider> getProviderList()
        {
            ObservableCollection<DeliProviders> deliProviders = deliveryProviderRepository.providerList();
            ObservableCollection<Provider> providerlist = new ObservableCollection<Provider>();
            foreach (var provider in deliProviders)
            {
                Provider providerT = new Provider(provider.id, provider.providerName);
                providerlist.Add(providerT);
            }
            return providerlist;
        }
        //-------------------------------------------------------------------------------------------------
        private long _id;
        public long id => _id;


        private void generateID()
        {
            Random random = new Random();
            _id = (long)random.Next();
            while (deliveryBillRepository.DeliBillsIDisNotNULL(_id))
            {
                _id = (long)random.Next();
            }
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
            deliveryItemsRepository.removeItemByID(SelectedItems.id);
            filterBillItems();
        }

        private void onConfirm()
        {
            deliveryOrderRepository.createNewImportForm(id.ToString(), selectedStatus, "1");
            foreach (var items in billItems)
            {
                deliveryOrderItemsRepository.insertItems(items, id.ToString());
            }
            closeForm();
        }

        private void onCancel()
        {
            deliveryItemsRepository.removeItemByBillsID(id);
            deliveryBillRepository.DeleteTemporaryBills(id);
            closeForm();
        }


    }
}
