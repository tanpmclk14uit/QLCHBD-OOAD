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
            get => _totalPrice.ToString();
        }
        private RentalBillStatus _status;
        public RentalBillStatus status
        {
            get => _status;
        }
        private String _stringStatus;
        public String stringStatus
        {
            get => _stringStatus;
            set
            {
                if(value == RentalBillStatus.OVERDUE.ToString())
                {
                    _stringStatus = "Over due";
                }
                else
                {
                    if(value == RentalBillStatus.RECEIVEDALL.ToString())
                    {
                        _stringStatus = "Received all";
                    }
                    else
                    {
                        _stringStatus = "Wating for receive";
                    }
                }
            }
        }
        private String _backgroundStatus;
        public String backgroundStatus
        {
            get => _backgroundStatus;
            set
            {
                if (value == RentalBillStatus.WAITING.ToString())
                {
                    _backgroundStatus = "#FFFFFF";
                }
                else
                {
                    if (value == RentalBillStatus.RECEIVEDALL.ToString())
                    {
                        _backgroundStatus = "#C6C6C6";
                    }
                    else
                    {
                        _backgroundStatus = "#E4C7C7";
                    }
                }
            }
        }
        
        public RentalBill(long id, long guestId, string guestName, DateTime createTime, int totalPrice, RentalBillStatus status)
        {
            this._id = id;
            this._guestId = guestId;
            this._guestName = guestName;
            this._createTime = createTime;
            this._totalPrice = totalPrice;
            this._status = status;
            this.stringStatus = status.ToString();
            this.backgroundStatus = status.ToString();
        }
        public RentalBill(long guestId, int totalPrice, RentalBillStatus status)
        {
            this._guestId = guestId;
            this._totalPrice = totalPrice;
            this._status = status;
        }

    }
}
