using Microsoft.Office.Interop.Word;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.receipt;
using QLCHBD_OOAD.model.retal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;

namespace QLCHBD_OOAD.viewmodel.budget
{
    class BugetViewModel : BaseViewModel
    {
        private DeliveryBillRepository deliveryBillRepository;
        private ReceiptRepository receiptRepository;
        private RentalBillRepository rentalBillRepository;
        private List<DeliBills> _deliBillsList;
        private List<Receipt> _receiptsList;
        private List<RentalBill> _rentalBillList;
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
            rentalBillRepository = RentalBillRepository.getIntance();

            dateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateEnd = DateTime.Now;


            ExportDocxCommand = new RelayCommand<object>((p) => { return true; }, (p) => { exportDocXFile(); });
            ExportXlxsCommand = new RelayCommand<object>((p) => { return true; }, (p) => { exportElxsFile(); });
        }


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
        private void exportDocXFile()
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
            ref oMissing, ref oMissing);

            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "REPORT";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.Range.InsertParagraphAfter();
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            oPara1.Range.Font.Bold = 0;

            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, 6, 2, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

            oTable.Cell(1, 1).Range.Text = "Store: ";
            oTable.Cell(2, 1).Range.Text = "Money In: ";
            oTable.Cell(3, 1).Range.Text = "Money Out: ";
            oTable.Cell(4, 1).Range.Text = "Total Money: ";
            oTable.Cell(5, 1).Range.Text = "Day Start: ";
            oTable.Cell(6, 1).Range.Text = "Day End: ";

            oTable.Cell(1, 2).Range.Text = "Ahihi Store";
            oTable.Cell(2, 2).Range.Text = totalIn.ToString();
            oTable.Cell(3, 2).Range.Text = totalOut.ToString();
            oTable.Cell(4, 2).Range.Text = (totalIn - totalOut).ToString();
            oTable.Cell(5, 2).Range.Text = dateStart.ToShortDateString();
            oTable.Cell(6, 2).Range.Text = dateEnd.ToShortDateString();

            //Add some text after the table.
            Word.Paragraph oPara4;
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara4.Range.InsertParagraphBefore();
            oPara4.Range.Text = "Report Detail";
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.Range.Font.Bold = 1;
            oPara4.Format.SpaceAfter = 24;
            oPara4.Range.InsertParagraphAfter();
            oPara4.Range.Font.Bold = 0;
            //Insert a report table
            int count = 1 + bugetList.Count;
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, count, 3, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
            oTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

            oTable.Cell(1, 1).Range.Text = "Day";
            oTable.Cell(1, 2).Range.Text = "Money In";
            oTable.Cell(1, 3).Range.Text = "Money Out";

            for (int i = 2; i <= count; i++)
            {
                oTable.Cell(i, 1).Range.Text = bugetList[i - 2].Date;
                oTable.Cell(i, 2).Range.Text = bugetList[i - 2].MoneyIn.ToString();
                oTable.Cell(i, 3).Range.Text = bugetList[i - 2].MoneyOut.ToString();
            }

            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Cell(1, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


            oPara4.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


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

            // bắt đầu duyệt mảng và ghi tiếp tục
            int rowIndex = 2;
            foreach (var item in _bugetList)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);

                // set giá trị
                newRow.CreateCell(0).SetCellValue(item.Date);
                newRow.CreateCell(1).SetCellValue(item.MoneyIn);
                newRow.CreateCell(2).SetCellValue(item.MoneyOut);
                // tăng index
                rowIndex++;
            }

            // xong hết thì save file lại
            string format = "ddMMyy_hhmmss";
            string directoryPath = @"c:\Report";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string path = @"c:\Report\BugetReport_" + DateTime.Now.ToString(format) + ".xlsx";
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
            foreach (var item in _rentalBillList)
            {
                totalIn += item.totalPriceSave;
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
                    _rentalBillList = rentalBillRepository.getRentalBillInRange(dateStart, dateEnd);
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
                    dateStart = new DateTime(dateEnd.Year, dateEnd.Month, 1);

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
            foreach (var item in _rentalBillList)
            {
                if (item.createTimeButInDateTimeFormat.Date == A.Date &&
                    item.createTimeButInDateTimeFormat.Month == A.Month &&
                    item.createTimeButInDateTimeFormat.Year == A.Year)
                {
                    sum += item.totalPriceSave;
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
