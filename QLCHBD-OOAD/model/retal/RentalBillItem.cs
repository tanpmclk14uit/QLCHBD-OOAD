using QLCHBD_OOAD.appUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.retal
{
    public class RentalBillItem
    {
        private long _id;
        public long id
        {
            get => _id;
        }

        private long _diskId;
        public long diskId { get => _diskId; }

        private string _diskName;
        public string diskName { get => _diskName; }

        private int _amount;
        public int amount { get => _amount; set => _amount = value; }

        private int _returned;
        public int returned { get => _returned; }

        private DateTime _dueDate;

  
        public string dueDate { get => _dueDate.ToShortDateString();}

        public void setDueDate( DateTime date)
        {
            _dueDate = date;
        }
        public DateTime getDueDate()
        {
            return _dueDate;
        }

        private int _rentalPrice;
        public int rentalPrice { get => _rentalPrice; }

        private string _image;
        public string image { get => _image; }

        private RentalBillStatus _rentalBillStatus;

        public void setRentalBIllStatus(RentalBillStatus status)
        {
            _rentalBillStatus = status;
        }
        public string rentalBillStatus
        {
            get
            {
                if(_rentalBillStatus == RentalBillStatus.OVERDUE)
                {
                    return "#FF0000";
                }
                else if(_rentalBillStatus == RentalBillStatus.WAITING)
                {
                    return "#FFFF00";
                }
                else if(_rentalBillStatus == RentalBillStatus.RETURNED)
                {
                    return "#00FF00";
                }
                else
                {
                    return "#FFFFFF";
                }

            }
        }
        private int _lost;
        public int lost
        {
            get => _lost;
            set => _lost = value;
        }

        public RentalBillItem(long id, long diskId, string diskName, int amount, int returned, DateTime dueDate, int rentalPrice, int lost) 
        {
            _id = id;
            _diskId = diskId;
            _diskName = diskName;
            _amount = amount;
            _returned = returned;
            _dueDate = dueDate;
            _rentalPrice = rentalPrice;
            _lost = lost;
        }
        public RentalBillItem(long diskId, string diskName, int amount, DateTime dueDate, int rentalPrice, string image)
        {
            _diskId = diskId;
            _diskName = diskName;
            _amount = amount;
            _dueDate = dueDate;
            _rentalPrice = rentalPrice;
            _returned = 0;            
            _image = image;
        }
       
        

    }
}
