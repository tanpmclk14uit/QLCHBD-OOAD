using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.retal;
using QLCHBD_OOAD.view.rental;
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
    class RentalReportViewModel: BaseViewModel
    {
        
        private RentalBill _selectedRentalBill;
        public RentalBill selectedRentalBill
        {
            get => _selectedRentalBill;
            set
            {
                _selectedRentalBill = value;
                if(value != null)
                {
                    RentalDetailOrderWindown rental = new RentalDetailOrderWindown(value.id, value.guestId);
                    rental.ShowDialog();
                    _selectedRentalBill = null;
                }
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
        public ObservableCollection<RentalBill> filterListRentalBill
        {
            get => filterByInfo();
        }
        private RentalBillRepository rentalBillReponsitory;

        public ObservableCollection<RentalBill> rentalBills
        {
            get => rentalBillReponsitory.getAllRentalBills();
        }
        public RentalReportViewModel()
        {
            rentalBillReponsitory = RentalBillRepository.getIntance();
            seachKey = "";
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
                    filterList = rentalBillReponsitory.getAllRentalBillsById(id);
                }
            }
            return filterList;
        }
    }
}
