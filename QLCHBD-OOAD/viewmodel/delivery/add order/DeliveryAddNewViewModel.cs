using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
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
        private DeliveryOrderItemsRepository deliveryItemsRepository;
        public static event GetImportItems GetItemsFromAddWindow;


        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public DeliveryAddNewViewModel(int count)
        {
            deliveryItemsRepository = DeliveryOrderItemsRepository.getIntance();
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(count); });
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onCancel(); });
        }


        private void onConfirm(int count)
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
                if(!int.TryParse(tbAmount, out int j))
            {
                MessageBox.Show("Format accepted: " + "123456790", "Amount");
            }
            else
            {
                GetItemsFromAddWindow(getImportItem(count));
                closeForm();
            }
        }
        private DeliOrderItems getImportItem(int count)
        {
            return new DeliOrderItems(count,
                                        -1,
                                        int.Parse(tbAmount),
                                        long.Parse(tbIDStore),
                                        tbName,
                                        long.Parse(tbPrice),
                                        long.Parse(tbIDProvider));
        }
        private bool BoxIsNotEmptyorNull(string box)
        {
            return (string.IsNullOrEmpty(box) || string.IsNullOrWhiteSpace(box));
        }

        private void onCancel()
        {
            closeForm();
        }

        public string tbIDStore { get; set; }
        public string tbIDProvider { get; set; }
        public string tbName { get; set; }
        public string tbPrice { get; set; }
        public string tbAmount { get; set; }
    }
}
