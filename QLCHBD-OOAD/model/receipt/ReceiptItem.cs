using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.receipt
{
    class ReceiptItem
    {
        private long _id;
        public long id
        {
            get => _id;
        }

        private long _receipt;
        public long receipt
        {
            get => _receipt;
        }

        private int _returnedQuantity;
        public int returnedQuantity
        {
            get => _returnedQuantity;
        }
        private int _lostQuantity;
        public int lostQuantity
        {
            get => _lostQuantity;
        }
        private long _diskId;
        public long diskId
        {
            get => _diskId;
        }
        private string _diskName;
        public string diskName
        {
            get => _diskName;
        }
        private int _delayDays;
        public int delayDays
        {
            get => _delayDays;
        }
        public ReceiptItem(long diskId,string diskName, int returnedQuantity, int lostQuantity, int delayDays)
        {
            _diskId = diskId;
            _diskName = diskName;
            _returnedQuantity = returnedQuantity;
            _lostQuantity = lostQuantity;
            _delayDays = delayDays;
        }
        public ReceiptItem(long receiptId,long diskId, string diskName, int returnedQuantity, int lostQuantity, int delayDays)
        {
            _receipt = receiptId;
            _diskId = diskId;
            _diskName = diskName;
            _returnedQuantity = returnedQuantity;
            _lostQuantity = lostQuantity;
            _delayDays = delayDays;
        }

    }
}
