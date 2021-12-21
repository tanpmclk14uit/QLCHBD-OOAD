using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.receipt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.buget
{
    class BugetBills
    {
        List<long> _importBillID;
        List<long> _receiptsBillsID;
        long _moneyIn;
        long _moneyOut;
        DateTime _date;

        public BugetBills(List<long> importBillID, List<long> receiptsBillsID, long moneyIn, long moneyOut, DateTime date)
        {
            _importBillID = importBillID;
            _receiptsBillsID = receiptsBillsID;
            _moneyIn = moneyIn;
            _moneyOut = moneyOut;
            _date = date;
        }

        public List<long> ImportBillID { get => _importBillID;}
        public List<long> ReceiptsBillsID { get => _receiptsBillsID;}
        public long MoneyIn { get => _moneyIn;}
        public long MoneyOut { get => _moneyOut;}
        public long TotalMoney => _moneyIn - _moneyOut;
        public DateTime Date { get => _date;}
        public String DateToString { get => _date.ToShortDateString();}
    }
}
