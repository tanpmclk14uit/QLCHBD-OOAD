using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.view.rental;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalAddPageViewModel: BaseViewModel
    {

        private ImagesRepository imagesRepository;

        private Images _selectedDisk;

        public Images selectedDisk
        {
            get => _selectedDisk;
            set 
            {
                _selectedDisk = value;
                if(value != null)
                {                    
                    _selectedDisk.rentalPrice = imagesRepository.getPriceById(value.id);
                    _selectedDisk.providerName = imagesRepository.getProviderNameById(value.id);
                    RenalDiskDetailForm renalDiskDetailForm = new RenalDiskDetailForm(_selectedDisk);
                    renalDiskDetailForm.Show();                    
                }
                
            }
        }

        public static TurnBackPageHandler turnBackToRentalAllOrders;

        private string _keyword;
        public string keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                if(value != null)
                {
                    _allImages = imagesRepository.getAllImagesForRentalLikeKeywordAndMatchId(value.Trim().ToLower());
                }
                OnPropertyChanged("allImages");
            }
        }

        private ObservableCollection<Images> _allImages;
        public ObservableCollection<Images> allImages
        {
            get => _allImages;
        }

        public ICommand Cancel { get; set; }

        public RentalAddPageViewModel()
        {
            Cancel = new RelayCommand<object>((p) => { return true; }, (p) => { turnBackToRentalAllOrders(); });
            imagesRepository = ImagesRepository.getInstance();
            _allImages = imagesRepository.getAllImagesForRental();
            _keyword = "";
        }

    }
}
