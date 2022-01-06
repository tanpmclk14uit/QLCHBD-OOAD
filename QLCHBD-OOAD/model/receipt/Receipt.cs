using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.receipt
{
    public class Receipt
    {
        private long _id;
        public long id
        {
            get => _id;
            set => _id = value;
        }
        private long _guestId;
        public long guestId
        {
            get => _guestId;
        }
        private DateTime _createTime;
        public String createTime
        {
            get => _createTime.ToShortDateString();
        }
        public DateTime createTimeButInDateTimeFormat
        {
            get => _createTime;
        }
        private string _guestName;
        public string guestName
        {
            get => _guestName;
        }
        private long _createBy;
        public long createBy
        {
            get => _createBy;
        }
        private string _staffName;

        public string staffName
        {
            get => _staffName;
        }
        private int _additionalFee;
        public int additionalFee
        {
            get => _additionalFee;
        }
        public Receipt(long id, string guestName, DateTime createTime, string staffName, int additionalFee, long guestId, long createBy)
        {
            _id = id;
            _guestName = guestName;
            _createTime = createTime;
            _staffName = staffName;
            _additionalFee = additionalFee;
            _guestId = guestId;
            _createBy = createBy;
        }
        public Receipt(long guestId, long createBy, int additionalFee)
        {
            _guestId = guestId;
            _createBy = createBy;
            _additionalFee = additionalFee;
        }
        
    }

}
