using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.retal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalPageViewModel : BaseViewModel
    {

        public ObservableCollection<RentalBill> filterListRentalBill
        {
            get => filterByInfo();
        }
        private RentalBillRepository rentalBillReponsitory;
        private ObservableCollection<RentalBill> _rentalBills;
        public ObservableCollection<RentalBill> rentalBills
        {
            get => _rentalBills;
        }
        private ObservableCollection<String> _selectedStatuses;
        public ObservableCollection<String> selectedStatuses
        {
            get => _selectedStatuses;
        }

        private String _selectedStatus;
        public String selectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                _rentalBills = filterRentalBills(value);
                OnPropertyChanged("seachKey");
                OnPropertyChanged("filterListRentalBill");
                OnPropertyChanged("selectedStatus");
            }
        }

        private String _seachKey;
        public String seachKey
        {
            get => _seachKey;
            set
            {
                _seachKey = value;
                OnPropertyChanged("filterListRentalBill");
                OnPropertyChanged("seachKey");
            }
        }

        private static RentalPageViewModel _intance;
        public static RentalPageViewModel getIntance()
        {
            if (_intance == null)
            {
                _intance = new RentalPageViewModel();
            }
            return _intance;
        }
        private void setUpStatuses()
        {
            _selectedStatuses = new ObservableCollection<string>();
            _selectedStatuses.Add("All");
            _selectedStatuses.Add("Over due");
            _selectedStatuses.Add("Received all");
            _selectedStatuses.Add("Waiting");
            selectedStatus = _selectedStatuses[0];
            OnPropertyChanged("selectedStatuses");
        }
        private ObservableCollection<RentalBill> filterRentalBills(String filter)
        {
            ObservableCollection<RentalBill> filterRentalBills = new ObservableCollection<RentalBill>();
            switch (filter)
            {
                case "All":
                    {
                        filterRentalBills = rentalBillReponsitory.getAllRentalBill();
                        break;
                    }
                case "Over due":
                    {
                        filterRentalBills = rentalBillReponsitory.getRentalBillsByFilterStatus(RentalBillStatus.OVERDUE.ToString());
                        break;
                    }
                case "Received all":
                    {
                        filterRentalBills = rentalBillReponsitory.getRentalBillsByFilterStatus(RentalBillStatus.RECEIVEDALL.ToString());
                        break;
                    }
                case "Waiting":
                    {
                        filterRentalBills = rentalBillReponsitory.getRentalBillsByFilterStatus(RentalBillStatus.WAITING.ToString());
                        break;
                    }
            }
            return filterRentalBills;
        }
        private RentalPageViewModel()
        {
            seachKey = "";
            _rentalBills = new ObservableCollection<RentalBill>();
            rentalBillReponsitory = RentalBillRepository.getIntance();
            setUpStatuses();

        }
        private ObservableCollection<RentalBill> filterByInfo()
        {
            ObservableCollection<RentalBill> filterList = new ObservableCollection<RentalBill>();

            if (seachKey == "" || seachKey[0] != '#')
            {
                foreach (var rentalBill in rentalBills)
                {

                    foreach (PropertyInfo prop in rentalBill.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        if (type == typeof(string) || type == typeof(int) || type == typeof(DateTime))
                        {
                            var rentalBill_field = prop.GetValue(rentalBill, null);
                            if (rentalBill_field != null)
                            {
                                String rentalBill_data = rentalBill_field.ToString().Trim().ToLower();
                                String keyWord = seachKey.ToLower();
                                if (rentalBill_data != null && keyWord != null)
                                {
                                    if (rentalBill_data.Contains(keyWord))
                                    {
                                        filterList.Add(rentalBill);
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
                    filterList = rentalBillReponsitory.getRentalBillsById(id);
                }
                
            }

            return filterList;
        }
    }

}
