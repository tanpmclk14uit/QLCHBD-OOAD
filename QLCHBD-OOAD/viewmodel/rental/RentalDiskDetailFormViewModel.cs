using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalDiskDetailFormViewModel: BaseViewModel
    {
        private Images _selectedImage;
        public Images selectedImage
        {
            get => _selectedImage;
        }
        private int _amount;
        public int amount 
        {
            get => _amount;
            set 
            {
                _amount = value;
                OnPropertyChanged("amount");
            }
        }
        private DateTime _returnDate;
        public DateTime returnDate
        {
            get => _returnDate;
            set
            {
                         
                if(value <= DateTime.Now)
                {
                    MessageBox.Show("Disk rental time minimum is one day");
                    _returnDate = DateTime.Now.AddDays(1);
                }
                else
                {
                    _returnDate = value;
                    if(value != null && selectedRentalBillItem != null)
                    {
                        selectedRentalBillItem.setDueDate(_returnDate);
                    }                    
                }
            }
        }
        private RentalBillItem selectedRentalBillItem;

        public ICommand AddMoreAmount { get; set; }
        public ICommand ReduceAmount { get; set; }

        public ICommand Add { get; set; }

        public static event CloseFormHandler closeForm;
        public static event AddNewRentalBillItem addNewRentalBillItem;

        public RentalDiskDetailFormViewModel(Images images)
        {
            _selectedImage = images;
            AddMoreAmount = new RelayCommand<object>((p) => { return true; }, (p) => { addMoreAmount(); });
            ReduceAmount = new RelayCommand<object>((p) => { return true; }, (p) => { reduceAmount(); });
            Add = new RelayCommand<object>((p) => { return true; }, (p) => { onAddClick(); });
            if(images.remaining == 0)
            {
                amount = 0;
            }
            else
            {
                amount = 1;
            }            
            returnDate = DateTime.Now.AddDays(1);
            selectedRentalBillItem = new RentalBillItem(images.id, images.name, amount, returnDate, images.rentalPrice, images.image);
        }
        private void onAddClick()
        {            
            closeForm();
            if(amount != 0)
            {
                addNewRentalBillItem(selectedRentalBillItem);
            }
                       
        }


        private void addMoreAmount()
        {
            if(amount < _selectedImage.remaining)
            {
                amount ++;
                selectedRentalBillItem.amount = amount;
            }
        }
        private void reduceAmount()
        {
            if(amount > 1)
            {
                amount--;
                selectedRentalBillItem.amount = amount;
            }
        }
    }
}
