using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class ChangeImagesInformationViewModel: BaseViewModel
    {

        public ICommand saveCommand { get; set; }

        public static event SaveImagesHandler saveImage;

        public ChangeImagesInformationViewModel(Images images)
        {
            image = images.image;
            isCheck = images.isCheck;
            name = images.name;
            quantity = images.quantity.ToString();
            provider = images.idProvider.ToString();
            idByProvider = images.idByProvider.ToString();
            createDate = images.createTime.ToString();
            loss = images.lostCharges.ToString();
            price = images.rentalPrice.ToString();
            locate = images.locate;
            album = images.idAlbum.ToString();
            saveCommand = new RelayCommand<object>((p) => { return true; }, (p) => { saveImage(images); });
            saveImage += onSaveImages;
        }

        private void onSaveImages(Images images)
        {
            imagesRepository.updateImage(images.id, image, isCheck, name, Convert.ToInt32(quantity), Convert.ToInt64(provider), Convert.ToInt64(idByProvider), createDate, Convert.ToInt32(loss), Convert.ToInt32(price), locate, Convert.ToInt64(album));
            Images newImage = new Images(images.id, name,Convert.ToInt64(album), Convert.ToInt32(quantity), image, locate, isCheck, Convert.ToInt32(price), Convert.ToInt64(provider), Convert.ToInt64(idByProvider), Convert.ToInt32(loss), DateTime.Parse(ImageDetailViewModel.selectedDisk.createTime), DateTime.Now, 1, 1, images.rented);
            ImageDetailViewModel.selectedDisk = newImage;
            ImagesViewModel.getIntance().onChange();
            ImageDetailViewModel.getIntance().onChange();
        }    
        
        private ImagesRepository imagesRepository = ImagesRepository.getInstance();

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
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged("image");
            }
        }

        
    }
}
