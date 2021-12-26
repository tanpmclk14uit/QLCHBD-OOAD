using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.receipt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.budget
{
    class BugetViewModel : BaseViewModel
    {
        private DeliveryBillRepository deliveryBillRepository;
        private ReceiptRepository receiptRepository;
        private List<DeliBills> _deliBillsList;
        private List<Receipt> _receiptsList;
        private List<BugetTotalBill> _bugetList;
        public List<BugetTotalBill> bugetList => _bugetList;

        public List<DeliBills> deliBillsList => _deliBillsList;
        public DeliBills selectedBills { get; set; }
        public List<Receipt> receiptsList => _receiptsList;
        public Receipt selectedReceipt { get; set; }




        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }


        public long totalIn { get; set; }
        public long totalOut { get; set; }
        public ICommand ExportDocxCommand { get; set; }
        public ICommand ExportXlxsCommand { get; set; }
        private BugetViewModel()
        {
            deliveryBillRepository = DeliveryBillRepository.getInstance();
            receiptRepository = ReceiptRepository.getIntance();

            dateStart = DateTime.Now;
            dateEnd = DateTime.Now;


            ExportDocxCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
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
            row1.CreateCell(2).SetCellValue("Time");
            row1.CreateCell(3).SetCellValue("Money out");

            // bắt đầu duyệt mảng và ghi tiếp tục
            int rowIndex = 2;
            foreach (var item in _receiptsList)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);

                // set giá trị
                newRow.CreateCell(0).SetCellValue(item.createTime);
                newRow.CreateCell(1).SetCellValue(item.additionalFee);
                // tăng index
                rowIndex++;
            }
            rowIndex = 2;
            foreach (var item in _deliBillsList)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);

                // set giá trị
                newRow.CreateCell(2).SetCellValue(item.CreateTime);
                newRow.CreateCell(3).SetCellValue(item.SumValue);
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
            foreach (var item in _deliBillsList)
            {
                totalOut += item.SumValue;
            }
            foreach (var item in _receiptsList)
            {
                totalIn += item.additionalFee;
            }
            OnPropertyChanged("totalIn");
            OnPropertyChanged("totalOut");
        }

        public void getBugetInRange()
        {
            if (dateStart != null && dateEnd != null)
                if (dateStart.CompareTo(dateEnd) <= 0)
                {
                    _deliBillsList = deliveryBillRepository.GetDeliBillInRange(dateStart, dateEnd);
                    _receiptsList = receiptRepository.getReceiptInRange(dateStart, dateEnd);
                    getBugetTotalBillInRange(dateStart, dateEnd);
                    GetTotalINOUT();
                    OnPropertyChanged("deliBillsList");
                    OnPropertyChanged("receiptsList");
                    OnPropertyChanged("bugetList");
                }
                else
                {

                    MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.ERROR, "Start Date must be earlier than End Date");
                    myDialog.ShowDialog();

                }
        }

        public void getBugetTotalBillInRange(DateTime A, DateTime B)
        {
            _bugetList = new List<BugetTotalBill>();
            long moneyIntemp = 0;
            long moneyOuttemp = 0;
            while (A.CompareTo(B) <= 0)
            {
                moneyIntemp = getMoneyInInDaty(A);
                moneyOuttemp = getMoneyOutInDaty(A);
                if (moneyIntemp == 0 && moneyOuttemp == 0) { }
                else
                {
                    _bugetList.Add(new BugetTotalBill(A, getMoneyInInDaty(A), getMoneyOutInDaty(A)));
                }
                A = A.AddDays(1);
            }
        }

        private long getMoneyInInDaty(DateTime A)
        {
            long sum = 0;
            foreach(var item in _receiptsList)
            {
                if (item.createTimeButInDateTimeFormat.Date == A.Date &&
                    item.createTimeButInDateTimeFormat.Month == A.Month &&
                    item.createTimeButInDateTimeFormat.Year == A.Year)
                {
                    sum += item.additionalFee;
                }
            }
            return sum;
        }

        private long getMoneyOutInDaty(DateTime A)
        {
            long sum = 0;
            foreach (var item in _deliBillsList)
            {
                if (item.PaymentDateButInDateTimeFormat.Date == A.Date &&
                    item.PaymentDateButInDateTimeFormat.Month == A.Month &&
                    item.PaymentDateButInDateTimeFormat.Year == A.Year)
                {
                    sum += item.SumValue;
                }
            }
            return sum;
        }

    }
    class BugetTotalBill
    {
        DateTime date;
        private long moneyIn = 0;
        private long moneyOut = 0;

        public BugetTotalBill(DateTime date, long moneyIn, long moneyOut)
        {
            this.date = date;
            this.moneyIn = moneyIn;
            this.moneyOut = moneyOut;
        }

        public long MoneyIn { get => moneyIn;}
        public long MoneyOut { get => moneyOut;}
        public String Date { get => date.ToShortDateString();}
    }
}
