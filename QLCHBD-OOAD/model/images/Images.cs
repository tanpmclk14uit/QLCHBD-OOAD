using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.images
{
    public class Images
    {
        public Images(long id, string name, long idAlbum, int quantity, string image, string locate, Boolean isCheck, int rentalPrice, long idProvider, long idByProvider, int lostCharges,
            DateTime createTime, DateTime updateTime, long createBy, long updateBy )
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
        }


        public Images(long id, string name, string image, int quantity, int rented)
        {
            _id = id;
            _name = name;
            _image = image;
            _quantity = quantity;
            _rented = rented;
        }

        public Images(long id, string name, long idAlbum, int quantity, string image, string locate, Boolean isCheck, int rentalPrice, long idProvider, long idByProvider, int lostCharges,
            DateTime createTime, long createBy)
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

        public string _image;
        public string image
        {
            get => _image;
        }
        

        public int remaining { get {
                return quantity - rented;
            } }

        private int _rented;

        public int rented { get => _rented; }

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


        private long _idByProvider;
        public long idByProvider
        {
            get => _idByProvider;
        }

        public int _lostCharges;
        public int lostCharges
        {
            get => _lostCharges;
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
