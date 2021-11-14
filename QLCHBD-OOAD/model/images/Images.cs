using QLCHBD_OOAD.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace QLCHBD_OOAD.model.images
{
    public class Images
    {
        public Images(long id, string name, long idAlbum, int quantity, string image, string locate, Boolean isCheck, int rentalPrice, long idProvider, long idByProvider, int lostCharges,
            DateTime createTime, DateTime updateTime, long createBy, long updateBy, int rented )
        {
            this._id = id;
            this._name = name;
            this._idAlbum = idAlbum;
            this._quantity = quantity;
            this._image = image;
            this._locate = locate;
            this._isCheck = isCheck;
            this._rentalPrice = rentalPrice;
            this._idProvider = idProvider;
            this._idByProvider = idByProvider;
            this._lostCharges = lostCharges;
            this._createTime = createTime;
            this._updateTime = updateTime;
            this._createBy = createBy;
            this._updateBy = updateBy;
            this._providerPrice = lostCharges.ToString();
            this._rented = rented;
            this._idByProviderForEdit = idByProvider.ToString();
            this._value = Convert.ToInt32(orderAmount) * Convert.ToInt32(_providerPrice);
        }


        public Images(long id, string name, string image, int quantity, int rented)
        {
            _id = id;
            _name = name;
            _image = image;
            _quantity = quantity;
            _rented = rented;
        }

        public Images(long id, string name, long idProvider, String idByProvider, String orderAmount, String providerPrice)
        {
            this._id = id;
            this._idByProviderForEdit = idByProvider;
            this._orderAmount = orderAmount;
            this._providerPrice = providerPrice;
            this._name = name;
            this._idProvider = idProvider;
            this._value = Convert.ToInt32(orderAmount) * Convert.ToInt32(_providerPrice);
        }

        public Images(long id, string name, long idAlbum, int quantity, string image, string locate, Boolean isCheck, int rentalPrice, long idProvider, long idByProvider, int lostCharges,
            DateTime createTime, long createBy, int rented)
        {
            this._id = id;
            this._name = name;
            this._idAlbum = idAlbum;
            this._quantity = quantity;
            this._image = image;
            this._locate = locate;
            this._isCheck = isCheck ;
            this._rentalPrice = rentalPrice;
            this._idProvider = idProvider;
            this._idByProvider = idByProvider;
            this._lostCharges = lostCharges;
            this._createTime = createTime;
            this._updateTime = createTime;
            this._createBy = createBy;
            this._updateBy = createBy;
            this._providerPrice = lostCharges.ToString();
            this._rented = rented;
            this._idByProviderForEdit = idByProvider.ToString();
            this._value = Convert.ToInt32(orderAmount) * Convert.ToInt32(_providerPrice);
        }

        private Boolean _isSelected;
        public Boolean isSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                if (_isSelected) _background = Brushes.Gray;
                else _background = Brushes.White;
            }
        }

        private String _orderAmount = "0";
        public String orderAmount
        {
            get => _orderAmount;
            set
            {
                if (value == "" || Convert.ToInt32(value) < 0) _orderAmount = "0";
                else _orderAmount = value;
                if (_orderAmount != "")
                {
                    _value = Convert.ToInt32(orderAmount) * Convert.ToInt32(_providerPrice);
                }
            }
            
        }

        private int _value ; 
        public int value
        {
            get => _value;
            set
            {
                _value= Convert.ToInt32(orderAmount) * Convert.ToInt32(_providerPrice);
            }
        }

        private Brush _background = Brushes.White;
        public Brush background
        {
            get => _background;
            set
            {
                _background = value;
            }
        }

        private long _id;
        public long id
        {
            get => _id;
        }

        public string _name;
        public string name
        {
            get => _name;
            set
            {
                name = _name;
            }
        }

        public long _idAlbum;
        public long idAlbum
        {
            get => _idAlbum;
        }

        private int _quantity;
        public int quantity
        {
            get => _quantity;
        }

        private String _displayQuantity;
        public String displayQuantity
        {
            get => remaining + "/" + _quantity.ToString();
        }

        public string _image;
        public string image
        {
            get => _image;
        }
        

        public int remaining { get {
                return quantity - rented;
            } }

        private int _rented;

        public int rented { get => _rented; set { _rented = value; } }

        public string _locate;
        public string locate
        {
            get => _locate;
        }

        private Boolean _isCheck;
        public Boolean isCheck
        {
            get => _isCheck;
        }

        public int _rentalPrice;
        public int rentalPrice
        {
            get => _rentalPrice;
            set => _rentalPrice = value;
        }
        private string _providerName;

        public string providerName
        {
            get => _providerName;
            set
            {
                _providerName = value;
            }
        }

        private long _idProvider;
        public long idProvider
        {
            get => _idProvider;
        }

        private String _idByProviderForEdit;
        public String idByProviderForEdit
        {
            get => _idByProviderForEdit;
            set
            {
                _idByProviderForEdit = value;
            }
        }

        private long _idByProvider;
        public long idByProvider
        {
            get => _idByProvider;
        }

        private int _lostCharges;
        public int lostCharges
        {
            get => _lostCharges;
            set
            {
                _lostCharges = value;

                if (_lostCharges.ToString() != "")
                {
                    _value = Convert.ToInt32(orderAmount) * Convert.ToInt32(_providerPrice);
                }
            }
        }

        private String _providerPrice;
        public String providerPrice
        {
            get => _providerPrice;
            set
            {
                if (value == "" || Convert.ToInt32(value) < 0) _providerPrice = "0";
                else _providerPrice = value;
                if (_providerPrice != "")
                {
                    _value = Convert.ToInt32(orderAmount) * Convert.ToInt32(providerPrice);
                }
            }
        }

        private DateTime _createTime;
        public String createTime
        {
            get => _createTime.ToShortDateString();
        }

        private DateTime _updateTime;
        public String updateTime
        {
            get => _updateTime.ToShortDateString();
        }

        private long _createBy;
        public long createBy
        {
            get => _createBy;
        }

        private long _updateBy;
        public long updateBy
        {
            get => _updateBy;
        }

    }
}
