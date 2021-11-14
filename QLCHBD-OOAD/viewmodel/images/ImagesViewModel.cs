using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.album;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using QLCHBD_OOAD.view.images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class ImagesViewModel: BaseViewModel
    {
 
        private ImagesRepository imagesRepository;
        private AlbumRepository albumRepository;
        private static ImagesViewModel _intance;

        private String _searchKey;
        public String searchKey
        {
            get => _searchKey;
            set
            {
                _searchKey = value;
                OnPropertyChanged("filterListImages");
                OnPropertyChanged("seachKey");
            }
        }

        private String _selectedAlbum;
        public String selectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                _selectedImage = null;
                _images = filterByAlbum(value);
                OnPropertyChanged("filterListImages");
                OnPropertyChanged("selectedAlbum");
            }
        }

        private Images _selectedImage;
        public Images selectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                if (selectedImage != null)
                {
                    openImageDetailPage();
                    OnPropertyChanged("selectedImage");
                }
                _selectedImage = null;
            }
        }

        public ICommand addOrderCommand { get; set; }

        public static event AddOrderHandler addOrder;

        private ImagesViewModel()
        {
            searchKey = "";          
            _images = new List<Images>();
            imagesRepository = ImagesRepository.getInstance();
            albumRepository = AlbumRepository.getInstance();
            _album = albumRepository.getAllAlbum();
            imagesBackup = imagesRepository.getAllImages();
            _images = imagesRepository.getAllImages();
            addOrderCommand = new RelayCommand<object>((p) => { return true; }, (p) => { addOrder(); });
            addOrder += addNewOrder;
            _filterListImages = filterByInfo();
            ImagesPage.onchange += selectedImageChange;
        }

        private void selectedImageChange()
        {
            //NotifyPropertyChanged(nameof(filterListImages));
            OnPropertyChanged("filterListImages");
        }


        private void addNewOrder()
        {
            List<Images> lstOrder = new List<Images>();
            foreach (Images item in images)
            {
                if (item.isSelected)
                {
                    lstOrder.Add(item);
                }
            }
            _images = imagesRepository.getAllImages();
            OnPropertyChanged("filterListImages");
            openAddOrderWindow(lstOrder);
        }

        private void openAddOrderWindow(List<Images> lstOrder)
        {
            AddNewOrderImageWindow addNewOrderImageWindow = new AddNewOrderImageWindow(lstOrder);
            addNewOrderImageWindow.ShowDialog();
        }

        public static ImagesViewModel getIntance()
        {
            if (_intance == null)
            {
                return _intance = new ImagesViewModel();
            }
            return _intance;
        }

        public void onChange()
        {
            _images = imagesRepository.getAllImages();
            OnPropertyChanged("filterListImages");
        }    

        private List<Images> _images;
        public List<Images> images
        {
            get => _images;
            set
            {
                _images = value;
            }
        }

        private  List<Images> _imagesBackup;
        public  List<Images> imagesBackup
        {
            get => _imagesBackup;
            set {
                _imagesBackup = value;
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

        public ObservableCollection<string> getAlbumName()
        {
            ObservableCollection<string> listAlbumName = new ObservableCollection<string>();
            foreach(Album albums in _album )
            {
                listAlbumName.Add(albums.name);
            }
            return listAlbumName;
        }

        public List<Images> _filterListImages;
        public List<Images> filterListImages
        {
            get => _filterListImages = filterByInfo();
        }
        private List<Images> filterByAlbum(string albumName)
        {
            long id = 0;
            foreach (Album albums in _album)
            {
                if (albums.name == albumName)
                {
                    id = albums.id;
                }
            }
            return imagesRepository.getImagesByAlbum(id);
        }

        public void openImageDetailPage()
        {
            
            if (selectedImage != null)
            {
                ImageDetailViewModel.selectedDisk = selectedImage;
                ImageFunctionViewModel.getIntance().SlideFrame = new ImageDetailPage();
            }

        }

        private List<Images> filterByInfo()
        {
            List<Images> resultList = new List<Images>();       

            if (searchKey == "" || searchKey[0] != '#')
            {

                foreach (var imageItem in _images)
                {


                    foreach (PropertyInfo prop in imageItem.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        if (type == typeof(string) || type == typeof(int) || imageItem.isSelected)
                        {
                            var rentalBill_field = prop.GetValue(imageItem, null);
                            if (rentalBill_field != null)
                            {
                                String rentalBill_data = rentalBill_field.ToString().Trim().ToLower();
                                String keyWord = searchKey.ToLower();
                                if (rentalBill_data != null && keyWord != null || imageItem.isSelected)
                                {
                                    if (rentalBill_data.Contains(keyWord) || imageItem.isSelected)
                                    {

                                        resultList.Add(imageItem);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                string id = Regex.Replace(searchKey, @"[^0-9]", string.Empty);
                if (id != "")
                {
                    resultList = imagesRepository.getImagesById(id);
                }

            }
            return resultList;
        }
    }
}
