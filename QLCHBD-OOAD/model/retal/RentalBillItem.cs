using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.retal
{
    public class RentalBillItem
    {
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

        public RentalBillItem(long diskId, string diskName, int amount, int returned, DateTime dueDate, int rentalPrice) 
        {
            _diskId = diskId;
            _diskName = diskName;
            _amount = amount;
            _returned = returned;
            _dueDate = dueDate;
            _rentalPrice = rentalPrice;
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
