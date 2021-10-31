using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.delivery.Component
{
    class DeliveryAddNewViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;
        private DeliveryBilingItemsRepository deliveryItemsRepository;


        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public DeliveryAddNewViewModel(long id)
        {
            deliveryItemsRepository = DeliveryBilingItemsRepository.getIntance();
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(id); });
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onCancel(); });
        }


        private void onConfirm(long id)
        {
            if (BoxIsNotEmptyorNull(tbIDStore) ||
                BoxIsNotEmptyorNull(tbIDProvider) ||
                BoxIsNotEmptyorNull(tbName) ||
                BoxIsNotEmptyorNull(tbPrice) ||
                BoxIsNotEmptyorNull(tbAmount))
            {
                MessageBox.Show("Please fill all before confirm", "Error");
            }
            else
                if (!int.TryParse(tbIDStore, out int m))
            {
                MessageBox.Show("ID Provider accept only number", "Error");
            }
            else 
                if (!int.TryParse(tbIDProvider, out int n))
            {
                MessageBox.Show("ID Provider accept only number", "Error");
            }
            else
                if (!int.TryParse(tbPrice, out int l))
            {
                MessageBox.Show("Number accepted: " + "123456790", "Error");
            }
            else 
                if(!int.TryParse(tbAmount, out int j))
            {
                MessageBox.Show("Number accepted: " + "123456790", "Error");
            }
            else
            {
                deliveryItemsRepository.insertItems(tbIDProvider, id.ToString(), tbName, tbPrice, tbIDStore, tbAmount);
                OnPropertyChanged();
                closeForm();
            }
        }
        private bool BoxIsNotEmptyorNull(string box)
        {
            return (string.IsNullOrEmpty(box) || string.IsNullOrWhiteSpace(box));
        }

        private void onCancel()
        {
            closeForm();
        }
        private string _tbIDStore;
        private string _tbIDProvider;
        private string _tbName;
        private string _tbPrice;
        private string _tbAmount;
        public string tbIDStore { get => _tbIDStore; set => _tbIDStore = value; }
        public string tbIDProvider { get => _tbIDProvider; set => _tbIDProvider = value; }
        public string tbName { get => _tbName; set => _tbName = value; }
        public string tbPrice { get => _tbPrice; set => _tbPrice = value; }
        public string tbAmount { get => _tbAmount; set => _tbAmount = value; }
    }
}
