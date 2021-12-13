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
        private int _totalPrice;
        public string totalPrice
        {
            get => _totalPrice.ToString("#,###");
        }
        
        public RentalBill(long id, long guestId, string guestName, DateTime createTime, int totalPrice)
        {
            this._id = id;
            this._guestId = guestId;
            this._guestName = guestName;
            this._createTime = createTime;
            this._totalPrice = totalPrice;           
        }
        public RentalBill(long guestId, int totalPrice, RentalBillStatus status)
        {
            this._guestId = guestId;
            this._totalPrice = totalPrice;            
        }

    }
}
