using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.images
{
    class ImageRentalInformation
    {

        public ImageRentalInformation(long rentalId, string name, int quantity, int value, DateTime dueDate)
        {
            this._dueDate = dueDate;
            this._rentalId = rentalId;
            this._name = name;
            this._value = value * quantity;
            this._quantity = quantity;
        }


        private long _rentalId;
        public long rentalId
        {
            get =>_rentalId;
        }

        private string _name;
        public string name
        {
            get => _name;
        }

        private int _value;
        public string value
        {
            get => _value.ToString() + "VNĐ";
        }

        private int _quantity;
        public int quantity
        {
            get => _quantity;
        }

        private DateTime _dueDate;
        public String dueDate 
        {
            get => _dueDate.ToShortDateString();
        }

    }
}
