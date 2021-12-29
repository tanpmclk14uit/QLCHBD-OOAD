using Microsoft.Office.Interop.Word;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryReportViewModel: BaseViewModel
    {
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }

        public long totalAmount { get; set; }
        public long totalValue { get; set; }
        public long totalAll { get; set; }
        public long totalAmountCancel { get; set; }
        public long totalValueCancel { get; set; }
        public long totalCancel { get; set; }
        public long totalAmountDelivered { get; set; }
        public long totalValueDelivered { get; set; }
        public long totalDelivered { get; set; }
        public long totalAmountDelivering { get; set; }
        public long totalValueDelivering { get; set; }
        public long totalDelivering { get; set; }


        DeliveryOrderRepository deliveryOrderRepository;
        private List<DeliOrder> _deliOrdersList;
        public List<DeliOrder> deliOrdersList => _deliOrdersList;
        public ICommand ExportDocxCommand { get; set; }
        public ICommand ExportXlxsCommand { get; set; }
        //-------------------------------------------------------------------------------------------------
        private DeliveryReportViewModel()
        {
            _deliOrdersList = new List<DeliOrder>();
            deliveryOrderRepository = DeliveryOrderRepository.getInstance();
            dateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateEnd = DateTime.Now;

            ExportDocxCommand = new RelayCommand<object>((p) => { return true; }, (p) => { exportDocXFile(); });
            ExportXlxsCommand = new RelayCommand<object>((p) => { return true; }, (p) => { exportElxsFile(); });
        }

        //-------------------------------------------------------------------------------------------------
        private static DeliveryReportViewModel _intance;
        public static DeliveryReportViewModel getInstance()
        {
            if (_intance == null)
            {
                _intance = new DeliveryReportViewModel();
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
            oTable = oDoc.Tables.Add(wrdRng, 5, 2, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oPara1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

            oTable.Cell(1, 1).Range.Text = "Store: ";
            oTable.Cell(2, 1).Range.Text = "Total Ammount: ";
            oTable.Cell(3, 1).Range.Text = "Total Money: ";
            oTable.Cell(3, 1).Range.Text = "Day Start: ";
            oTable.Cell(4, 1).Range.Text = "Day End: ";

            oTable.Cell(1, 2).Range.Text = "Ahihi Store";
            oTable.Cell(2, 2).Range.Text = totalAmount.ToString();
            oTable.Cell(3, 2).Range.Text = totalValue.ToString();
            oTable.Cell(4, 2).Range.Text = dateStart.ToShortDateString();
            oTable.Cell(5, 2).Range.Text = dateEnd.ToShortDateString();

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
            int count = 1 + deliOrdersList.Count;
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, count, 6, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
            oTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

            oTable.Cell(1, 1).Range.Text = "Day";
            oTable.Cell(1, 2).Range.Text = "ID";
            oTable.Cell(1, 3).Range.Text = "Provider";
            oTable.Cell(1, 4).Range.Text = "Amount";
            oTable.Cell(1, 5).Range.Text = "Total Bills";
            oTable.Cell(1, 6).Range.Text = "Status";

            for (int i = 2; i <= count; i++)
            {
                oTable.Cell(i, 1).Range.Text = deliOrdersList[i - 2].createTime;
                oTable.Cell(i, 2).Range.Text = deliOrdersList[i - 2].id.ToString();
                oTable.Cell(i, 3).Range.Text = deliOrdersList[i - 2].provider;
                oTable.Cell(i, 4).Range.Text = deliOrdersList[i - 2].amount.ToString();
                oTable.Cell(i, 5).Range.Text = deliOrdersList[i - 2].totalBills.ToString();
                oTable.Cell(i, 6).Range.Text = deliOrdersList[i - 2].stringStatus;
            }

            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Cell(1, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Cell(1, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Cell(1, 5).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            oTable.Cell(1, 6).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


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
            string name = $"Delivery Report from {dateStart} to {dateEnd}";
            row0.GetCell(0).SetCellValue(name);
            // Ghi tên cột ở row 1
            var row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("Day");
            row1.CreateCell(1).SetCellValue("ID");
            row1.CreateCell(2).SetCellValue("Provider");
            row1.CreateCell(3).SetCellValue("Amount");
            row1.CreateCell(4).SetCellValue("Total Bills");
            row1.CreateCell(5).SetCellValue("Status");

            // bắt đầu duyệt mảng và ghi tiếp tục
            int rowIndex = 2;
            foreach (var item in _deliOrdersList)
            {
                // tao row mới
                var newRow = sheet.CreateRow(rowIndex);

                // set giá trị
                newRow.CreateCell(0).SetCellValue(item.createTime);
                newRow.CreateCell(1).SetCellValue(item.id);
                newRow.CreateCell(2).SetCellValue(item.provider);
                newRow.CreateCell(3).SetCellValue(item.amount);
                newRow.CreateCell(4).SetCellValue(item.totalBills);
                newRow.CreateCell(5).SetCellValue(item.stringStatus);
                // tăng index
                rowIndex++;
            }

            // xong hết thì save file lại
            string format = "ddMMyy_hhmmss";
            string path = @"c:\Report\DeliveryReport_" + DateTime.Now.ToString(format) + ".xlsx";
            FileStream fs = new FileStream(path, FileMode.CreateNew);
            wb.Write(fs);
            Process.Start(path);
        }
        //-------------------------------------------------------------------------------------------------
        private void getTotal()
        {
            totalAmount = 0;
            totalValue = 0;
            totalAll = 0;
            totalAmountCancel = 0;
            totalValueCancel = 0;
            totalCancel = 0;
            totalAmountDelivered = 0;
            totalValueDelivered = 0;
            totalDelivered = 0;
            totalAmountDelivering = 0;
            totalValueDelivering = 0;
            totalDelivering = 0;
            foreach(var item in _deliOrdersList)
            {
                totalAmount += item.amount;
                totalValue += item.totalBills;
                totalAll++;
                if (item.status == DeliveryOrderStatus.WATING)
                {
                    totalAmountDelivering += item.amount;
                    totalValueDelivering += item.totalBills;
                    totalDelivering++;
                }
                if (item.status == DeliveryOrderStatus.DELIVERED)
                {
                    totalAmountDelivered += item.amount;
                    totalValueDelivered += item.totalBills;
                    totalDelivered++;
                }
                if (item.status == DeliveryOrderStatus.ERROR)
                {
                    totalAmountCancel += item.amount;
                    totalValueCancel += item.totalBills;
                    totalCancel++;
                }
                OnPropertyChanged("totalAmount");
                OnPropertyChanged("totalValue");
                OnPropertyChanged("totalAll");
                OnPropertyChanged("totalAmountCancel");
                OnPropertyChanged("totalValueCancel");
                OnPropertyChanged("totalCancel");
                OnPropertyChanged("totalAmountDelivered");
                OnPropertyChanged("totalValueDelivered");
                OnPropertyChanged("totalDelivered");
                OnPropertyChanged("totalAmountDelivering");
                OnPropertyChanged("totalValueDelivering");
                OnPropertyChanged("totalDelivering");
            }
        }

        //-------------------------------------------------------------------------------------------------
        public void getDeliveryInRange()
        {
            if (dateStart != null && dateEnd != null)
                if (dateStart.CompareTo(dateEnd) <= 0)
                {
                    _deliOrdersList = deliveryOrderRepository.getImportFormInRange(dateStart, dateEnd);
                    getTotal();
                    OnPropertyChanged("deliOrdersList");
                }
                else
                {
                    MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.ERROR, "Start Date must be earlier than End Date");
                    myDialog.ShowDialog();
                }
        }
        //-------------------------------------------------------------------------------------------------

    }
}
