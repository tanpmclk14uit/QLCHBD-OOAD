using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private ImageDetailViewModel()
        {

        }


    }
}
