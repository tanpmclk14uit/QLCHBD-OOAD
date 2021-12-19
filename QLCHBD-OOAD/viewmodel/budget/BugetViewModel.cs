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
        public List<BugetBills> bugetBillsList;
        public BugetBills selectedBills;
        public long totalIn = 0;
        public long totalOut = 0;
        public ICommand ExportDocxCommand { get; set; }
        public ICommand ExportXlxsCommand { get; set; }
        private BugetViewModel()
        {
            bugetBillsRepository = BugetBillsRepository.getInstance();
            bugetBillsList = new List<BugetBills>();


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

        private void getBugetInRange(DateTime A, DateTime B) => bugetBillsList = bugetBillsRepository.getBugetBillsInRange(A, B);
        private BugetBills getBugetByDate(DateTime date) => bugetBillsRepository.GetBugetBillsByDate(date);

    }
}
