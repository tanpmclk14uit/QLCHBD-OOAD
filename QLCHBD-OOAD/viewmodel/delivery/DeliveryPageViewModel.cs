using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.view.delivery.Add_Order;
using System.Windows;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryPageViewModel : BaseViewModel
    {
        public static ChangePageHandler turnToImportFormDetailPage;
        private DeliveryOrderRepository deliOrderlReponsitory;
        public DeliOrder SelectedOrder { get; set; }
        public ICommand AddOrderCommand { get; set; }
        public ICommand AddProviderCommand { get; set; }
        public ICommand ModifyProviderCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private DeliveryPageViewModel()
        {
            _seachKey = "";
            deliOrders = new ObservableCollection<DeliOrder>();
            deliOrderlReponsitory = DeliveryOrderRepository.getInstance();
            setUpStatusses();

            AddOrderCommand = new RelayCommand<object>((p) => { return true; }, (p) => { addOrderDelivery(); });
            ModifyProviderCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onModifyProvider(); });
            AddProviderCommand = new RelayCommand<object>((p) => { return true; }, (p) => { addProviderDelivery(); });
            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onDelete(); });
        }

        //-------------------------------------------------------------------------------------------------
        private static DeliveryPageViewModel _intance;
        public static DeliveryPageViewModel getInstance()
        {
            if (_intance == null)
            {
                _intance = new DeliveryPageViewModel();
            }
            return _intance;
        }
        //-------------------------------------------------------------------------------------------------
        private ObservableCollection<DeliOrder> _deliOrders;
        public ObservableCollection<DeliOrder> deliOrders
        {
            get => _deliOrders;
            set
            {
                _deliOrders = value;
            }
        }
        //-------------------------------------------------------------------------------------------------
        private ObservableCollection<String> _selectedStatuses;
        public ObservableCollection<String> selectedStatuses => _selectedStatuses;
        //-------------------------------------------------------------------------------------------------
        private DeliOrder _selectedDeliOrder;
        public DeliOrder selectedDeliOrder
        {
            get => _selectedDeliOrder;
            set
            {
                _selectedDeliOrder = value;
                if (value != null)
                {
                    long selectedID = _selectedDeliOrder.id;
                    _selectedDeliOrder = null;
                    turnToImportFormDetailPage(selectedID.ToString());
                }
            }
        }
        //-------------------------------------------------------------------------------------------------
        private String _selectedStatus;
        public String selectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                _deliOrders = filterDeliOders(value);
                OnPropertyChanged("seachKey");
                OnPropertyChanged("fillerListDeliOder");
                OnPropertyChanged("selectedStatus");
            }
        }
        //-------------------------------------------------------------------------------------------------
        private String _seachKey;
        public String seachKey
        {
            get => _seachKey;
            set
            {
                _seachKey = value;
                OnPropertyChanged("fillerListDeliOder");
                OnPropertyChanged("seachKey");
            }
        }
        //-------------------------------------------------------------------------------------------------
        private void setUpStatusses()
        {
            _selectedStatuses = new ObservableCollection<string>();
            _selectedStatuses.Add("All");
            _selectedStatuses.Add("Waiting");
            _selectedStatuses.Add("Delivered");
            _selectedStatuses.Add("Cancel");
            selectedStatus = _selectedStatuses[0];
            OnPropertyChanged("selectedStatuses");
        }

        private ObservableCollection<DeliOrder> filterDeliOders(String filter)
        {
            ObservableCollection<DeliOrder> filterDeliOder = new ObservableCollection<DeliOrder>();
            switch (filter)
            {
                case "All":
                    {
                        filterDeliOder = deliOrderlReponsitory.getAllDeliOrder();
                        break;
                    }
                case "Waiting":
                    {
                        filterDeliOder = deliOrderlReponsitory.getDeliOrderByFilterStatus(DeliveryOrderStatus.WATING.ToString());
                        break;
                    }
                case "Delivered":
                    {
                        filterDeliOder = deliOrderlReponsitory.getDeliOrderByFilterStatus(DeliveryOrderStatus.DELIVERED.ToString());
                        break;
                    }
                case "Cancel":
                    {
                        filterDeliOder = deliOrderlReponsitory.getDeliOrderByFilterStatus(DeliveryOrderStatus.ERROR.ToString());
                        break;
                    }

            }
            return filterDeliOder;
        }

        public ObservableCollection<DeliOrder> fillerListDeliOder => filterByInfo();
        private ObservableCollection<DeliOrder> filterByInfo()
        {
            ObservableCollection<DeliOrder> filterList = new ObservableCollection<DeliOrder>();

            if (seachKey == "" || seachKey[0] != '#')
            {
                foreach (var deliOrder in deliOrders)
                {

                    foreach (PropertyInfo prop in deliOrders.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        if (type == typeof(string) || type == typeof(int) || type == typeof(DateTime))
                        {
                            var deliOrder_field = prop.GetValue(deliOrders, null);
                            if (deliOrder_field != null)
                            {
                                String deliOrder_data = deliOrder_field.ToString().Trim().ToLower();
                                String keyWord = seachKey.ToLower();
                                if (deliOrder_data != null && keyWord != null)
                                {
                                    if (deliOrder_data.Contains(keyWord))
                                    {
                                        filterList.Add(deliOrder);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                string id = Regex.Replace(seachKey, @"[^0-9]", string.Empty);
                if (id != "")
                {
                    filterList = deliOrderlReponsitory.filterDeliOrderByID(id);
                }

            }

            return filterList;
        }
        //-------------------------------------------------------------------------------------------------

        private void onModifyProvider()
        {
            DeliveryProviderListWindow providerList = new DeliveryProviderListWindow();
            providerList.ShowDialog();
            selectedStatus = selectedStatus;
        }

        private void addOrderDelivery()
        {
            NewDeliveryWindow newDeliveryWindow = new NewDeliveryWindow();
            newDeliveryWindow.ShowDialog();
            selectedStatus = selectedStatuses[0];
        }

        private void addProviderDelivery()
        {
            DeliveryAskForm askForm = new DeliveryAskForm();
            askForm.ShowDialog();
        }

        private void onDelete()
        {
            deliOrderlReponsitory.deleteFormWithID(selectedDeliOrder.id.ToString());
            deliOrders.Remove(selectedDeliOrder);
            OnPropertyChanged("seachKey");
            OnPropertyChanged("fillerListDeliOder");
            OnPropertyChanged("selectedStatus");
        }

    }
}
