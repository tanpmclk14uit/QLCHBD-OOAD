using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using QLCHBD_OOAD.view.rental;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private ObservableCollection<RentalBillItem> _rentalBillItems;
        public ObservableCollection<RentalBillItem> rentalBillItems
        {
            get 
            {               
                return _rentalBillItems; 
            }
        }

        public static event TurnBackPageHandler turnBackToRentalAllOrders;

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
        public ICommand Confirm { get; set; }

        public RentalAddPageViewModel()
        {
            
            _rentalBillItems = new ObservableCollection<RentalBillItem>();            
            Cancel = new RelayCommand<object>((p) => { return true; }, (p) => { turnBackToRentalAllOrders(); });
            Confirm = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirmClick(); turnBackToRentalAllOrders(); });
            imagesRepository = ImagesRepository.getInstance();
            _allImages = imagesRepository.getAllImagesForRental();
            _keyword = "";
            RentalDiskDetailFormViewModel.addNewRentalBillItem += RentalDiskDetailFormViewModel_addNewRentalBillItem;
            rentalBillReponsitory = RentalBillRepository.getIntance();

        }
        private void onConfirmClick()
        {
            RentalBill rentalBill = new RentalBill(1, Convert.ToInt32(totalPrice), RentalBillStatus.WAITING);
            rentalBillReponsitory.createNewRentalBill(rentalBill, 1, rentalBillItems);           
        }
        private double _totalPrice;
        private RentalBillRepository rentalBillReponsitory;

        public double totalPrice
        {
            get => _totalPrice;
        }
        private void caculateTotalPrice(RentalBillItem rentalBillItem)
        {
            _totalPrice += rentalBillItem.amount * rentalBillItem.rentalPrice * Convert.ToInt32((rentalBillItem.getDueDate().Date - DateTime.Now.Date).TotalDays);         
            OnPropertyChanged("totalPrice");
        }
 
        private void RentalDiskDetailFormViewModel_addNewRentalBillItem(RentalBillItem rentalBillItem)
        {
                
            caculateTotalPrice(rentalBillItem);
            _rentalBillItems.Add(rentalBillItem);
            OnPropertyChanged("rentalBillItems");
        }
      

    }
}
