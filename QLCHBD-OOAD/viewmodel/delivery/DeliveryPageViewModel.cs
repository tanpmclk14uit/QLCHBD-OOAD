using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryPageViewModel : BaseViewModel
    {
        public ObservableCollection<DeliOrder> fillerListDeliOder => filterByInfo();
        private DeliveryOrderRepository deliOrderlReponsitory;

        private static DeliveryPageViewModel _intance;
        private ObservableCollection<DeliOrder> _deliOrders;
        private ObservableCollection<String> _selectedStatuses;
        private String _selectedStatus;
        private String _seachKey;

        public static DeliveryPageViewModel getInstance()
        {
            if (_intance == null)
            {
                _intance = new DeliveryPageViewModel();
            }
            return _intance;
        }

        private DeliveryPageViewModel()
        {
            _seachKey = "";
            _deliOrders = new ObservableCollection<DeliOrder>();
            deliOrderlReponsitory = DeliveryOrderRepository.getIntance();
            setUpStatusses();
        }

        public ObservableCollection<DeliOrder> deliOrders => _deliOrders;

        public ObservableCollection<String> selectedStatuses => _selectedStatuses;
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

        private void setUpStatusses()
        {
            _selectedStatuses = new ObservableCollection<string>();
            _selectedStatuses.Add("All");
            _selectedStatuses.Add("Waiting");
            _selectedStatuses.Add("Delivery");
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
                case "Delivery":
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
                    filterList = deliOrderlReponsitory.getDeliOrderById(id);
                }

            }

            return filterList;
        }


    }
}
