using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.buget;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            dateStart = DateTime.Now;
            dateEnd = DateTime.Now;


            ExportDocxCommand = new RelayCommand<object>((p) => { return true; }, (p) => {});
            ExportXlxsCommand = new RelayCommand<object>((p) => { return true; }, (p) => { exportElxsFile(); });
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
        private void exportElxsFile()
        {
            // export
            XSSFWorkbook wb = new XSSFWorkbook();

            // Tạo ra 1 sheet
            ISheet sheet = wb.CreateSheet();

            // Bắt đầu ghi lên sheet

            // Tạo row
            var row0 = sheet.CreateRow(0);
            // Merge lại row đầu 3 cột
            row0.CreateCell(0); // tạo ra cell trc khi merge
            CellRangeAddress cellMerge = new CellRangeAddress(0, 0, 0, 5);
            sheet.AddMergedRegion(cellMerge);
            string name = $"Buget report from {dateStart} to {dateEnd}";
            row0.GetCell(0).SetCellValue(name);
            // Ghi tên cột ở row 1
            var row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("Time");
            row1.CreateCell(1).SetCellValue("Money in");
            row1.CreateCell(2).SetCellValue("Money out");
            row1.CreateCell(3).SetCellValue("Total money");

            // bắt đầu duyệt mảng và ghi tiếp tục
            int rowIndex = 2;
            foreach (var item in bugetBillsList)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);

                // set giá trị
                newRow.CreateCell(0).SetCellValue(item.DateToString);
                newRow.CreateCell(1).SetCellValue(item.MoneyIn);
                newRow.CreateCell(2).SetCellValue(item.MoneyOut);
                newRow.CreateCell(3).SetCellValue(item.TotalMoney);

                // tăng index
                rowIndex++;
            }

            // xong hết thì save file lại
            string format = "dd-MM-yyyy-hh-mm-ss";
            string path = @"D:\Report\" + "BugetReport" + DateTime.Now.ToString(format) + ".xlsx";
            FileStream fs = new FileStream(path, FileMode.CreateNew);
            wb.Write(fs);
            Process.Start(path);
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

                    //MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.ERROR, "Start Date must be earlier than End Date");
                    //myDialog.ShowDialog();

                }
        }
        private BugetBills getBugetByDate(DateTime date) => bugetBillsRepository.GetBugetBillsByDate(date);

    }
}
