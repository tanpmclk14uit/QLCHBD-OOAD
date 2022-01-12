using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using QLCHBD_OOAD.view.rental;
using QLCHBD_OOAD.viewmodel.guest;
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

        private static RentalAddPageViewModel intance;
        
        public static RentalAddPageViewModel getIntance()
        {
            if(intance == null)
            {
                intance = new RentalAddPageViewModel();
            }
            return intance;
        }

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
        private Guest _guest;
        public Guest guest
        {
            get => _guest;
            set 
            {
                _guest = value;
                OnPropertyChanged("guest");
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
        public ICommand AddMember { get; set; }

        private Visibility _isMember;
        public Visibility isMember
        {
            get
            {
                return _isMember;
            }
            set
            {
                _isMember = value;
                OnPropertyChanged("isMember");
            }
        }

        public void onReturn()
        {
            _rentalBillItems = new ObservableCollection<RentalBillItem>();
            _allImages = imagesRepository.getAllImagesForRental();
            isMember = Visibility.Hidden;
            _keyword = "";
            guest = null;
        }
        private RentalAddPageViewModel()
        {            
            _rentalBillItems = new ObservableCollection<RentalBillItem>();            
            Cancel = new RelayCommand<object>((p) => { return true; }, (p) => {clearData() ; turnBackToRentalAllOrders(); });
            Confirm = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirmClick(); clearData(); turnBackToRentalAllOrders();  });
            AddMember = new RelayCommand<object>((p) => { return true; }, (p) => { onAddMemberClick(); });
            imagesRepository = ImagesRepository.getInstance();
            _allImages = imagesRepository.getAllImagesForRental();
            isMember = Visibility.Hidden;
            _keyword = "";
            RentalDiskDetailFormViewModel.addNewRentalBillItem += RentalDiskDetailFormViewModel_addNewRentalBillItem;
            RentalAddMemberViewModel.guestTranferInformation += RentalAddMemberViewModel_guestTranferInformation;
            rentalBillReponsitory = RentalBillRepository.getIntance();
        }

      

        private void RentalAddMemberViewModel_guestTranferInformation(Guest guest)
        {
            if(guest != null)
            {
                this.guest = guest;
                if (guest.isMember)
                {
                    isMember = Visibility.Visible;
                }
                else
                {
                    isMember = Visibility.Hidden;
                }
            }
            
        }

        private void onAddMemberClick()
        {
            if(guest != null)
            {
                RentalAddMember rentalAddMember = new RentalAddMember(guest.id);
                rentalAddMember.Show();
            }
            else
            {
                RentalAddMember rentalAddMember = new RentalAddMember();
                rentalAddMember.Show();
            }
           
        }
        private void clearData()
        {
            _rentalBillItems.Clear();
            _totalPrice = 0;
            keyword = "";

        }
        private void onConfirmClick()
        {
            if(guest != null)
            { 
                RentalBill rentalBill = new RentalBill(guest.id, Convert.ToInt32(totalPrice), RentalBillStatus.WAITING);
                long staffID = CurrentStaff.getInstance().currentStaff.id;
                rentalBillReponsitory.createNewRentalBill(rentalBill, staffID, rentalBillItems);
            }
            else
            {
                MessageBox.Show("Please select guest");
            }

                     
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
            foreach (var rental in _rentalBillItems)
            {
                if(rental.diskId == rentalBillItem.diskId && rental.dueDate == rentalBillItem.dueDate)
                {
                    int amount = rental.amount + rentalBillItem.amount;
                    int maxInStock = rentalBillReponsitory.getAmoutOfDiskByDiskId(rentalBillItem.diskId);
                    if(amount > maxInStock)
                    {
                        amount = maxInStock;
                        MyDialog myDialog = new MyDialog(MyDialogStyle.ERROR, "Max in stock");
                        myDialog.ShowDialog();
                    }                    
                    _rentalBillItems.Remove(rental);
                    caculateTotalPrice(rentalBillItem);
                    rentalBillItem.amount = amount;
                    _rentalBillItems.Add(rentalBillItem);                   
                    OnPropertyChanged("renalBillItems");
                    return;
                }
            }
            _rentalBillItems.Add(rentalBillItem);
            caculateTotalPrice(rentalBillItem);
            OnPropertyChanged("rentalBillItems");
        }
      
    }
}
