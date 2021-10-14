using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.album;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                _intance = new ImagesViewModel();
            }
            return _intance;
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
        }

        public ObservableCollection<Images> filterListImages
        {
            get => filterByInfo();
        }

        private ObservableCollection<Images> filterByInfo()
        {
            ObservableCollection<Images> resultList = new ObservableCollection<Images>();       

            if (searchKey == "" || searchKey[0] != '#')
            {
                foreach (var imageItem in images)
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
