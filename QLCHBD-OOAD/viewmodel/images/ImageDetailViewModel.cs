using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using QLCHBD_OOAD.view.images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class ImageDetailViewModel : BaseViewModel
    {
       
        private ImagesRepository imagesRepository = ImagesRepository.getInstance();
        private static ImageDetailViewModel _instance;
        private RentalBillRepository rentalBillRepository = RentalBillRepository.getIntance();
        public static Images selectedDisk;


        public ObservableCollection<ImageRentalInformation> rentalBills
        {
            get => rentalBillRepository.getWaitingRentalBillsByDiskId(selectedDisk.id.ToString());
            set
            {
                OnPropertyChanged("selectedDisk");
            }
        }

        public ICommand backCommand { get; set; }

        public static event BackHandler back;

        private ImageDetailViewModel()
        {
            backCommand = new RelayCommand<object>((p) => { return true; }, (p) => { back(); });
            back += backToImages;
        }


        public void backToImages()
        {
            ImageFunctionViewModel.getIntance().SlideFrame = new ImagesPage();
        }

        public string name
        {     
            get => selectedDisk.name;
            set
            {
                OnPropertyChanged("selectedDisk");
            }
        }
   
        public string image
        {
            get => selectedDisk.image;
            set
            {
                
                OnPropertyChanged("selectedDisk");
            }
        }

    
        public string nameProvider
        {
            get => selectedDisk.idProvider.ToString();
            set
            {
                OnPropertyChanged("selectedDisk");
            }
        }

        public string createDate
        {
            get => selectedDisk.createTime.ToString();
            set
            {               
                OnPropertyChanged("selectedDisk");
            }
        }
        public string quantity
        {
            get => selectedDisk.quantity.ToString();
            set
            {
                OnPropertyChanged("selectedDisk");
            }
        }
        public static ImageDetailViewModel getIntance()
        {
            if (_instance == null)
            {
                _instance = new ImageDetailViewModel();
            }
            return _instance;
        }

       


    }
}
