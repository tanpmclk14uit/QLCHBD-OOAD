using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;

namespace QLCHBD_OOAD.viewmodel.delivery.provider
{
    /// <summary>
    /// Interaction logic for AddNewProvider.xaml
    /// </summary>
    class AddNewProviderViewModel
    {
        public static event CloseFormHandler closeForm;
        private DeliveryProviderRepository deliveryProviderRepository;

        public ICommand CancelCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }


        public AddNewProviderViewModel()
        {
            deliveryProviderRepository = DeliveryProviderRepository.getIntance();
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { closeForm(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
        }

        private void onConfirm()
        {
            if (BoxIsNotEmptyorNull(tbIDProvider) ||
                BoxIsNotEmptyorNull(tbName) ||
                BoxIsNotEmptyorNull(tbNumber) ||
                BoxIsNotEmptyorNull(tbMail) ||
                BoxIsNotEmptyorNull(tbName) ||
                BoxIsNotEmptyorNull(tbAddress))
            {
                MessageBox.Show("Please fill all before confirm", "Error");
            }
            else
                if (!int.TryParse(tbIDProvider, out int n))
            {
                MessageBox.Show("Format accepted: " + "123456790", "ID Provider");
            }
            else
                if (!int.TryParse(tbNumber, out int m))
            {
                MessageBox.Show("Format accepted: "+"123456790", "Number");
            }
            else
                if (tbMail.IndexOf("@") == -1)
            {
                MessageBox.Show("Email not found", "Mail");
            }
            else
                if (tbName.Length > 10)
            {
                MessageBox.Show("Max length: 10", "Provider");
            }
            else
            {
                deliveryProviderRepository.insertProvider(tbIDProvider, tbName, tbNumber, tbMail, tbAddress);
                MessageBox.Show("Provider is added to Database", "Error");
                closeForm();
            }
        }
        private bool BoxIsNotEmptyorNull(string box)
        {
            return (string.IsNullOrEmpty(box) || string.IsNullOrWhiteSpace(box));
        }

        private string _tbIDProvider;
        private string _tbName;
        private string _tbNumber;
        private string _tbMail;
        private string _tbAddress;
        public string tbIDProvider { get => _tbIDProvider; set => _tbIDProvider = value; }
        public string tbName { get => _tbName; set => _tbName = value; }
        public string tbNumber { get => _tbNumber; set => _tbNumber = value; }
        public string tbMail { get => _tbMail; set => _tbMail = value; }
        public string tbAddress { get => _tbAddress; set => _tbAddress = value; }
    }
}
