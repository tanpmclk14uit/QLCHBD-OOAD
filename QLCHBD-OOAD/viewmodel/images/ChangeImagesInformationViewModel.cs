using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.album;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class ChangeImagesInformationViewModel: BaseViewModel
    {
        private ImagesRepository imagesRepository = ImagesRepository.getInstance();
        private AlbumRepository albumRepository = AlbumRepository.getInstance();
        private DeliveryProviderRepository deliveryProviderRepository = DeliveryProviderRepository.getInstance();
        public ICommand saveCommand { get; set; }
        public ICommand addPictureCommand { get; set; }

        public static event SaveImagesHandler saveImage;



        private int _rented;
        public int rented
        {
            get => _rented;
            set
            {
                _rented = value;
                OnPropertyChanged("rented");
            }
        }

        public ChangeImagesInformationViewModel(Images images)
        {
            _album = albumRepository.getAllAlbum();
            _providers = deliveryProviderRepository.getAllProvider();
            image = images.image;
            rented = images.rented;
            isCheck = images.isCheck;
            name = images.name;
            quantity = images.quantity.ToString();
            provider = deliveryProviderRepository.getProviderNameById(images.idProvider);
            idByProvider = images.idByProvider.ToString();
            createDate = images.publish;
            loss = images.lostCharges.ToString();
            price = images.rentalPrice.ToString();
            locate = images.locate;
            _selectedAlbum = albumRepository.getAlbumNameById(images.idAlbum);
            saveCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (validate())
                {
                    image = setupImageFromDialog("disk");
                    saveImage(images);
                }
            });
            addPictureCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onAddImage(); });
            saveImage += onSaveImages;
        }

        public bool validate()
        {

            if (selectedAlbum == "" || locate == "" || provider == "" || name == null || price == "" || loss == "" || idByProvider == "")
            {
                System.Windows.MessageBox.Show("Vui lòng nhập tất cả thông tin");
                return false;
            }
            else if (!price.All((ch) => Char.IsDigit(ch)) || !loss.All((ch) => Char.IsDigit(ch)))
            {
                System.Windows.MessageBox.Show("Giá tiền không thể có ký tự nào khác ngoài số");
                return false;
            }
            else if (!idByProvider.All((ch) => Char.IsDigit(ch)))
            {
                System.Windows.MessageBox.Show("id không thể có ký tự nào khác ngoài số");
                return false;
            }
            else if (createDate > DateTime.Now)
            {
                System.Windows.MessageBox.Show("Ngày phát hành không thể lớn hơn hiện tại");
                return false;
            }
            else if (Convert.ToInt32(quantity) < rented)
            {
                System.Windows.MessageBox.Show("Tổng số lượng đĩa không thể bé hơn số lượng đã cho mượn");
                return false;
            }    
            return true;
        }

        public bool validateForUI()
        {

            if (selectedAlbum == "" || locate == "" || provider == "" || name == null || price == "" || loss == "" || idByProvider == "")
            {
                return false;
            }
            else if (!price.All((ch) => Char.IsDigit(ch)) || !loss.All((ch) => Char.IsDigit(ch)))
            {
                return false;
            }
            else if (!idByProvider.All((ch) => Char.IsDigit(ch)))
            {
                return false;
            }
            else if (createDate > DateTime.Now)
            {
                return false;
            }
            else if (Convert.ToInt32(quantity) < rented)
            {
                return false;
            }
            return true;
        }

        private void onSaveImages(Images images)
        {
            
            imagesRepository.updateImage(images.id, image, isCheck, name, Convert.ToInt32(quantity), deliveryProviderRepository.getProviderIdByName(provider), Convert.ToInt64(idByProvider), createDate, Convert.ToInt32(loss), Convert.ToInt32(price), locate, albumRepository.getAlbumIdByName(selectedAlbum));
            Images newImage = new Images(images.id, name, albumRepository.getAlbumIdByName(_selectedAlbum), Convert.ToInt32(quantity), image, locate, isCheck, Convert.ToInt32(price), deliveryProviderRepository.getProviderIdByName(provider), Convert.ToInt64(idByProvider), Convert.ToInt32(loss), ImageDetailViewModel.selectedDisk.publish, DateTime.Parse(ImageDetailViewModel.selectedDisk.createTime), DateTime.Now, 1, 1, images.rented);
            ImageDetailViewModel.selectedDisk = newImage;
            ImagesViewModel.getIntance().onChange();
            ImageDetailViewModel.getIntance().onChange();
        }    
        

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

        private string _selectedAlbum;
        public string selectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
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

        private DateTime _createDate;
        public DateTime createDate
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

        private ObservableCollection<Album> _album;
        public ObservableCollection<Album> album
        {
            get => _album;
            set
            {
                _album = value;
            }
        }

        public ObservableCollection<String> albumName
        {
            get => getAlbumName();
        }

        private ObservableCollection<DeliProviders> _providers;
        public ObservableCollection<DeliProviders> providers
        {
            get => _providers;
            set
            {
                _providers = value;
            }
        }

        public ObservableCollection<String> providerName
        {
            get => getProviderName();
        }

        public ObservableCollection<string> getAlbumName()
        {
            ObservableCollection<string> listAlbumName = new ObservableCollection<string>();
            foreach (Album albums in _album)
            {
                listAlbumName.Add(albums.name);
            }
            return listAlbumName;
        }

        public ObservableCollection<string> getProviderName()
        {
            ObservableCollection<string> listProviderName = new ObservableCollection<string>();
            foreach (DeliProviders provider in _providers)
            {
                listProviderName.Add(provider.providerName);
            }
            return listProviderName;
        }

        private string setupImageFromDialog(string name)
        {
            if (File.Exists(image))
            {
                string extension = Path.GetExtension(image);
                string fileName = name + "_" + DateTime.Now.ToString("mmFFFFFFF") + extension;
                string linkToAssets = Path.GetFullPath("QLCHBD-OOAD/QLCHBD-OOAD/Assets/");

                for (int i = 0; i < 6; ++i)
                {
                    linkToAssets = Path.GetDirectoryName(linkToAssets);
                }
                linkToAssets += @"\Assets\";

                linkToAssets += fileName;

                var file = File.Create(linkToAssets);
                file.Close();

                File.Copy(image, linkToAssets, true);
                file.Close();
                return linkToAssets.Replace(@"\", "/");
            }
            else if (image.Contains(@"C:\") || image.Contains(@"D:\") || image.Contains(@"E:\"))
            {
                return "/QLCHBD-OOAD;component/assets/img_noImage.png";
            }
            return image;
        }
        private string getImageFromDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;


            if (dlg.ShowDialog() == DialogResult.OK)
            {
                image = dlg.FileName;
                return image;
            }
            return image;
        }

        private void onAddImage()
        {
            image = getImageFromDialog();
            OnPropertyChanged("image");

        }
    }
}
