using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.buget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.budget
{
    class BugetViewModel : BaseViewModel
    {
        private BugetBillsRepository bugetBillsRepository;
        private List<BugetBills> _bugetBillsList;
        public List<BugetBills> bugetBillsList => _bugetBillsList;
        public BugetBills selectedBills { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }

        public long totalIn { get; set; }
        public long totalOut { get; set; }
        public ICommand ExportDocxCommand { get; set; }
        public ICommand ExportXlxsCommand { get; set; }
        private BugetViewModel()
        {
            bugetBillsRepository = BugetBillsRepository.getInstance();
            _bugetBillsList = new List<BugetBills>();


            ExportDocxCommand = new RelayCommand<object>((p) => { return true; }, (p) => {});
            ExportXlxsCommand = new RelayCommand<object>((p) => { return true; }, (p) => {});
        }
        //-------------------------------------------------------------------------------------------------
        private static BugetViewModel _intance;
        public static BugetViewModel getInstance()
        {
            if (_intance == null)
            {
                _intance = new BugetViewModel();
            }
            return _intance;
        }
        //-------------------------------------------------------------------------------------------------
        public void GetTotalINOUT()
        {
            totalIn = 0;
            totalOut = 0;
            foreach (var item in bugetBillsList)
            {
                totalIn += item.MoneyIn;
                totalOut += item.MoneyOut;
            }
            OnPropertyChanged("totalIn");
            OnPropertyChanged("totalOut");
        }
        
        public void getBugetInRange()
        {
            if (dateStart != null && dateEnd != null)
                if (dateStart.CompareTo(dateEnd) <= 0)
                {
                    _bugetBillsList = bugetBillsRepository.getBugetBillsInRange(dateStart, dateEnd);
                    GetTotalINOUT();
                    OnPropertyChanged("bugetBillsList");
                }
                else
                {

                }
        }
        private BugetBills getBugetByDate(DateTime date) => bugetBillsRepository.GetBugetBillsByDate(date);

    }
}
