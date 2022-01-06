using QLCHBD_OOAD.appUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.retal
{
    class RentalBill
    {
        private long _id;
        public long id
        {
            get => _id;            
        }
        private string _guestName;
        public string guestName
        {
            get => _guestName;
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
        private int _totalPrice;
        public int totalPriceSave
        {
            get => _totalPrice;
        }
        public string totalPrice
        {
            get => _totalPrice.ToString("#,###");
        }

        private bool _returnedAll;
        public bool returnedALl
        {
            get => _returnedAll;
        }
        private string _staffName;
        public string staffName
        {
            get => _staffName;
        }
        public RentalBill(long id, long guestId, string guestName, DateTime createTime, int totalPrice)
        {
            this._id = id;
            this._guestId = guestId;
            this._guestName = guestName;
            this._createTime = createTime;
            this._totalPrice = totalPrice;
        }
        
        public RentalBill(long id, long guestId, string guestName, DateTime createTime, int totalPrice, string staffName, bool isReturnedAll)
        {
            this._id = id;
            this._guestId = guestId;
            this._guestName = guestName;
            this._createTime = createTime;
            this._totalPrice = totalPrice;
            _staffName = staffName;
            _returnedAll = isReturnedAll;
            
        }
        public RentalBill(long guestId, int totalPrice, RentalBillStatus status)
        {
            this._guestId = guestId;
            this._totalPrice = totalPrice;            
        }

        public RentalBill(DateTime createTime, int totalPrice)
        {
            this._createTime = createTime;
            this._totalPrice = totalPrice;
            this._id = 0;
            this._guestId = 0;
            this._guestName = "None";
            _staffName = "None";
            _returnedAll = false;
        }

    }
}
