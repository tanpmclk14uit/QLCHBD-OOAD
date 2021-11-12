using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.view.delivery.Add_Order;
using QLCHBD_OOAD.viewmodel.delivery.Component;
using static QLCHBD_OOAD.dao.DeliveryProviderRepository;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryaAddOrderFormViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;

        private DeliveryOrderRepository deliveryOrderRepository;
        private DeliveryOrderItemsRepository deliveryOrderItemsRepository;
        private DeliveryProviderRepository deliveryProviderRepository;
        public DeliOrderItems SelectedItems { get; set; }

        private ObservableCollection<DeliOrderItems> _importItems;
        public ObservableCollection<DeliOrderItems> importItems { get => _importItems; set { _importItems = value; OnPropertyChanged("importItems"); } }
        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public DeliveryaAddOrderFormViewModel()
        {
            importItems = new ObservableCollection<DeliOrderItems>();

            deliveryProviderRepository = getIntance();
            deliveryOrderRepository = DeliveryOrderRepository.getIntance();
            deliveryOrderItemsRepository = DeliveryOrderItemsRepository.getIntance();
            DeliveryAddNewViewModel.GetItemsFromAddWindow += AddItemToFilterList;
            setUpStatusses();
            generateID();
            RemoveCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onRemove(); });
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) => { openAddWindow(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onCancel(); });
        }
        //-------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------
        private long _totalBills = 0;
        private int _totalAmount = 0;
        public long totalBills => _totalBills;
        public int totalAmount => _totalAmount;

        //-------------------------------------------------------------------------------------------------

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
                OnPropertyChanged("selectedStatus");
            }
        }
        //-------------------------------------------------------------------------------------------------
        private void ChangeProvider(string provider)
        {
            deliveryOrderRepository.updateTemporaryImportForm(id, provider);
        }
        //-------------------------------------------------------------------------------------------------
        private List<DeliProviders> _providerCombobox;
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
        private List<DeliProviders> getProviderList()
        {
            List<DeliProviders> deliProviders = deliveryProviderRepository.providerList();
            return deliProviders;
        }
        //-------------------------------------------------------------------------------------------------
        private int _id;
        public int id => _id;
        private void generateID()
        {
            Random random = new Random();
            _id = (int)random.Next();
            while (deliveryOrderRepository.DeliOrderIDisNotNULL(_id))
            {
                _id = (int)random.Next();
            }
        }
        //-------------------------------------------------------------------------------------------------
        private void AddItemToFilterList(DeliOrderItems item)
        {
            _totalAmount += item.Amount;
            _totalBills += item.imPrice*item.Amount;
            OnPropertyChanged("totalAmount");
            OnPropertyChanged("totalBills");
            importItems.Add(new DeliOrderItems(item.id,
                                                id,
                                                item.Amount,
                                                item.diskID, item.diskName,
                                                item.imPrice,
                                                item.IDbyProvider));
        }

        //-------------------------------------------------------------------------------------------------

        private void openAddWindow()
        {
            AddNewDiskDeliveryWindow window = new AddNewDiskDeliveryWindow(importItems.Count);
            window.ShowDialog();
        }


        private void onRemove()
        {
            if (SelectedItems == null) {}
            else
            {
                _totalAmount -= SelectedItems.Amount;
                _totalBills -= SelectedItems.imPrice*SelectedItems.Amount;
                importItems.Remove(SelectedItems);
            }
        }

        private void onConfirm()
        {
            if (totalAmount != 0)
            {
                deliveryOrderRepository.createNewImportForm(id.ToString(), selectedStatus, totalAmount, totalBills, 1);
                foreach (var item in importItems)
                {
                    deliveryOrderItemsRepository.insertItems(item);
                }
            }

            closeForm();
        }

        private void onCancel()
        {
            closeForm();
        }


    }
}
