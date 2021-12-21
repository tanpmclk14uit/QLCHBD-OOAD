using QLCHBD_OOAD.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.dashboard
{
    class FirstLandingViewModel: BaseViewModel
    {
        private static FirstLandingViewModel _instance;
        private ImagesRepository imagesRepository;
        private RentalBillRepository rentalBillRepository;
        private DeliveryOrderItemsRepository deliveryOrderItemsRepository;
        public static FirstLandingViewModel getIntance()
        {
            if (_instance == null)
            {
                return _instance = new FirstLandingViewModel();
            }
            return _instance;
        }

        private FirstLandingViewModel()
        {
            imagesRepository = ImagesRepository.getInstance();
            rentalBillRepository = RentalBillRepository.getIntance();
            deliveryOrderItemsRepository = DeliveryOrderItemsRepository.getInstance();
            _total = imagesRepository.getTotalOfImage();
            _numberOnBorrow = rentalBillRepository.getNumberBorrowedImage();
            _numberInStock = _total - numberOnBorrow;
            _numberOnDelivery = deliveryOrderItemsRepository.getNumberDeliveringImage();
        }

        private long _numberOnDelivery;
        public long numberOnDelivery
        {
            get => _numberOnDelivery;
            set
            {
                _numberOnDelivery = value;
                OnPropertyChanged("numberOnDelivery");
            }
        }

        private long _numberOnBorrow;
        public long numberOnBorrow
        {
            get => _numberOnBorrow;
            set
            {
                _numberOnBorrow = value;
                OnPropertyChanged("numberOnBororow");
            }
        }

        private long _numberInStock;
        public long numberInStock
        {
            get => _numberInStock;
            set
            {
                _numberInStock = value;
                OnPropertyChanged("numberInStock");
            }
        }

        private long _total;
        public long total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged("total");
            }
        }
    }
}
