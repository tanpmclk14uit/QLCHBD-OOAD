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
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalPageViewModel : BaseViewModel
    {
        public static TurnToDetailPageHandler turnAllRentalToDetailRental;

        public static ChangePageHandler turnToAddPage;

        public ICommand NewOrder { get; set; }

        private RentalBill _selectedRentalBill;
        public RentalBill selectedRentalBill
        {
            get => _selectedRentalBill;
            set
            {
                _selectedRentalBill = value;
                if(value != null)
                {
                    turnAllRentalToDetailRental(_selectedRentalBill.id, _selectedRentalBill.guestId);
                    _selectedRentalBill = null;
                }               
            }
        }
        public ObservableCollection<RentalBill> filterListRentalBill
        {
            get => filterByInfo();
        }
        private RentalBillRepository rentalBillReponsitory;
       
        public ObservableCollection<RentalBill> rentalBills
        {
            get => rentalBillReponsitory.getAllRentalBill();
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
        //Add something to filter
        private void setUpStatuses()
        {
            _selectedStatuses = new ObservableCollection<string>();            
        }
        
        private RentalPageViewModel()
        {
            seachKey = "";            
            rentalBillReponsitory = RentalBillRepository.getIntance();
            NewOrder = new RelayCommand<object>((p) => { return true; }, (p) => { turnToAddPage(); });
            setUpStatuses();
            RentalAddPageViewModel.turnBackToRentalAllOrders += RentalAddPageViewModel_turnBackToRentalAllOrders;
        }

        private void RentalAddPageViewModel_turnBackToRentalAllOrders()
        {
            selectedStatus = "All";
            OnPropertyChanged("filterListRentalBill");
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
