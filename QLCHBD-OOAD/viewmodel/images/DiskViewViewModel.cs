using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class DiskViewViewModel: BaseViewModel
    {

        private ImagesRepository imagesRepository = ImagesRepository.getInstance();
        
        public ICommand addImageCommand { get; set; }

        public static event AddImageHandler addImage;

        private Images newImages;

        private string _name;
        public string name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }

        private string _album;
        public string album
        {
            get => _album;
            set
            {
                _album = value;
                OnPropertyChanged("album");
            }
        }

        private string _provider;
        public string provider
        {
            get => _provider;
            set
            {
                _provider = value;
                OnPropertyChanged("provider");
            }
        }

        private string _locate;
        public string locate
        {
            get => _locate;
            set
            {
                _locate = value;
                OnPropertyChanged("locate");
            }
        }

        private string _idByProvider;
        public string idByProvider
        {
            get => _idByProvider;
            set
            {
                _idByProvider = value;
                OnPropertyChanged("idByProvider");
            }
        }

        private string _price;
        public string price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged("price");
            }
        }

        private string _loss;
        public string loss
        {
            get => _loss;
            set
            {
                _loss = value;
                OnPropertyChanged("loss");
            }
        }

        private string _createDate;
        public string createDate
        {
            get => _createDate;
            set
            {
                _createDate = value;
                OnPropertyChanged("createDate");
            }
        }
        private string _quantity = "0";
        public string quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged("quantity");
            }
        }
        private bool _isCheck = false;
        public bool isCheck
        {
            get => _isCheck;
            set
            {
                _isCheck = value;
                OnPropertyChanged("isCheck");
            }
        }

        private string _image;
        public string image
        {
            get => "https://www.indiewire.com/wp-content/uploads/2019/12/us-1.jpg?w=758";
            set
            {
                _image = value;
                OnPropertyChanged("image");
            }
        }

        public DiskViewViewModel()
        {
            addImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                newImages = new Images(imagesRepository.getNumberOfImage()+1, name, Convert.ToInt64(album), Convert.ToInt32(quantity), image, locate, isCheck, Convert.ToInt32(price), Convert.ToInt64(provider), Convert.ToInt64(idByProvider), Convert.ToInt32(loss), DateTime.Now, DateTime.Now, 1, 1);
                addImage(newImages); });
            addImage += addNewImage;
        }

        public void addNewImage(Images newImages)
        {
            imagesRepository.uploadNewImage(newImages);
            ImagesViewModel.getIntance().onChange();
        }



    }
}
