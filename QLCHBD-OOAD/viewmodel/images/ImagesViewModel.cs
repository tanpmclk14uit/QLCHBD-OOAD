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
                }
                OnPropertyChanged("selectedImage");
                _selectedImage = null;
            }
        }
        public ImagesViewModel()
        {
            searchKey = "";
            _images = new ObservableCollection<Images>();
            imagesRepository = ImagesRepository.getInstance();
            albumRepository = AlbumRepository.getInstance();
            _album = albumRepository.getAllAlbum();
            _images = imagesRepository.getAllImages();
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

        private ObservableCollection<Images> _images;
        public ObservableCollection<Images> images
        {
            get => _images;
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

        public ObservableCollection<Images> filterListImages
        {
            get => filterByInfo();
        }
        private ObservableCollection<Images> filterByAlbum(string albumName)
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
            ImageFunctionViewModel.getIntance().SlideFrame = new ImageDetailPage();
            if (selectedImage != null)
            {
                ImageDetailViewModel.selectedDisk = selectedImage;
            }

        }

        private ObservableCollection<Images> filterByInfo()
        {
            ObservableCollection<Images> resultList = new ObservableCollection<Images>();       

            if (searchKey == "" || searchKey[0] != '#')
            {
                foreach (var imageItem in _images)
                {

                    foreach (PropertyInfo prop in imageItem.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        if (type == typeof(string) || type == typeof(int))
                        {
                            var rentalBill_field = prop.GetValue(imageItem, null);
                            if (rentalBill_field != null)
                            {
                                String rentalBill_data = rentalBill_field.ToString().Trim().ToLower();
                                String keyWord = searchKey.ToLower();
                                if (rentalBill_data != null && keyWord != null)
                                {
                                    if (rentalBill_data.Contains(keyWord))
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
