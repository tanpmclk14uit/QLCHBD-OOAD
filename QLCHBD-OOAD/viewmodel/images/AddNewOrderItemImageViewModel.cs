using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.images;
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
    class AddNewOrderItemImageViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;
        private DeliveryOrderItemsRepository deliveryItemsRepository;


        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public static event ConfirmAddOrderItemImage confirm;

        public AddNewOrderItemImageViewModel()
        {
            deliveryItemsRepository = DeliveryOrderItemsRepository.getInstance();
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onCancel(); });
        }


        private void onConfirm()
        {
            if (BoxIsNotEmptyorNull(tbIDStore) ||
                BoxIsNotEmptyorNull(tbIDProvider) ||
                BoxIsNotEmptyorNull(tbName) ||
                BoxIsNotEmptyorNull(tbPrice) ||
                BoxIsNotEmptyorNull(tbAmount))
            {
                MessageBox.Show("Please fill all before confirm", "Blank Text");
            }
            else
                if (!int.TryParse(tbIDStore, out int m))
            {
                MessageBox.Show("Format accepted: " + "123456790", "ID Store");
            }
            else
                if (!int.TryParse(tbIDProvider, out int n))
            {
                MessageBox.Show("Format accepted: " + "123456790", "ID Provider");
            }
            else
                if (!int.TryParse(tbPrice, out int l))
            {
                MessageBox.Show("Format accepted: " + "123456790", "Price");
            }
            else
                if (!int.TryParse(tbAmount, out int j))
            {
                MessageBox.Show("Format accepted: " + "123456790", "Amount");
            }
            else
            {
                Images images = new Images(Convert.ToInt64(tbIDStore), tbName, Convert.ToInt64(tbIDProvider), tbIDByProvider, tbAmount, tbPrice);
                confirm(images);
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
        private string _tbIDByProvider;
        private string _tbName;
        private string _tbPrice;
        private string _tbAmount;
        public string tbIDStore { get => _tbIDStore; set => _tbIDStore = value; }
        public string tbIDProvider { get => _tbIDProvider; set => _tbIDProvider = value; }
        public string tbIDByProvider { get => _tbIDByProvider; set => _tbIDByProvider = value; }
        public string tbName { get => _tbName; set => _tbName = value; }
        public string tbPrice { get => _tbPrice; set => _tbPrice = value; }
        public string tbAmount { get => _tbAmount; set => _tbAmount = value; }
    }
}
