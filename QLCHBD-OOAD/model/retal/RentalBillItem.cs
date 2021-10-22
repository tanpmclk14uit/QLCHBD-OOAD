using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.retal
{
    class RentalBillItem
    {
        private long _diskId;
        public long diskId { get => _diskId; }

        private string _diskName;
        public string diskName { get => _diskName; }

        private int _amount;
        public int amount { get => _amount; }

        private int _returned;
        public int returned { get => _returned; }

        private DateTime _dueDate;
        public string dueDate { get => _dueDate.ToShortDateString(); }

        private int _rentalPrice;
        public int rentalPrice { get => _rentalPrice; }

        public RentalBillItem(long diskId, string diskName, int amount, int returned, DateTime dueDate, int rentalPrice) 
        {
            _diskId = diskId;
            _diskName = diskName;
            _amount = amount;
            _returned = returned;
            _dueDate = dueDate;
            _rentalPrice = rentalPrice;
        }
        
    }
}
