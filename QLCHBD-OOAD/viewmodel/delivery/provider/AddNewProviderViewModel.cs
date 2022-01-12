using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using MessageBox = System.Windows.Forms.MessageBox;

namespace QLCHBD_OOAD.viewmodel.delivery.provider
{
    /// <summary>
    /// Interaction logic for AddNewProvider.xaml
    /// </summary>
    class AddNewProviderViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;
        private DeliveryProviderRepository deliveryProviderRepository;
        private int _id;
        public int id => _id;

        public ICommand CancelCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand AddImageCommand { get; set; }

        public AddNewProviderViewModel()
        {
            deliveryProviderRepository = DeliveryProviderRepository.getInstance();
            generateID();
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { closeForm(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            AddImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onAddImage(); });
        }
        
        private void generateID()
        {
            Random random = new Random();
            _id = (int)random.Next();
            while (deliveryProviderRepository.isProviderNull(_id.ToString()))
            {
                _id = (int)random.Next();
            }
            tbIDProvider = _id.ToString();
            OnPropertyChanged("tbIDProvider");
        }
        private string setupImageFromDialog(string name) 
        {
            if (File.Exists(image))
            {
                string extension = Path.GetExtension(image);
                string fileName = name + "_" + DateTime.Now.ToString("mmFFFFFFF") + extension;
                string linkToAssets = Path.GetFullPath("QLCHBD-OOAD/QLCHBD-OOAD/Assets/");

                for (int i = 0; i < 6; ++i)
                {
                    linkToAssets = Path.GetDirectoryName(linkToAssets);
                }
                linkToAssets += @"\Assets\";

                linkToAssets += fileName;

                var file = File.Create(linkToAssets);
                file.Close();

                File.Copy(image, linkToAssets, true);
                file.Close();
                return linkToAssets.Replace(@"\", "/");
            }
            else if (image.Contains(@"C:\") || image.Contains(@"D:\") || image.Contains(@"E:\"))
            {
                return "/QLCHBD-OOAD;component/assets/img_noImage.png";
            }
            return image;
        }
        private string getImageFromDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;


            if (dlg.ShowDialog() == DialogResult.OK)
            {
                image = dlg.FileName;
                return image;
            }
            return image;
        }

        private void onAddImage()
        {
            image = getImageFromDialog();
            OnPropertyChanged("image");

        }

        private void onConfirm()
        {
            if (BoxIsNotEmptyorNull(tbIDProvider) ||
                BoxIsNotEmptyorNull(tbName) ||
                BoxIsNotEmptyorNull(tbNumber) ||
                BoxIsNotEmptyorNull(tbMail) ||
                BoxIsNotEmptyorNull(tbName) ||
                BoxIsNotEmptyorNull(image) ||
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
                if (image.Length > 255)
            {
                MessageBox.Show("Image link is to long", "Image");
            }
            else
                if (deliveryProviderRepository.isProviderNull(tbIDProvider))
            {
                MessageBox.Show("Provider ID is existed", "ID");
            }
            else
            {
                image = setupImageFromDialog("provider");
                deliveryProviderRepository.insertProviderWithTextBox(id.ToString(), tbName, tbNumber, tbMail, tbAddress, CurrentStaff.getInstance().currentStaff.id.ToString(), image);
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
        private string _image;
        
        public string image { get => _image; set => _image = value; }
        public string tbIDProvider { get => _tbIDProvider; set => _tbIDProvider = value; }
        public string tbName { get => _tbName; set => _tbName = value; }
        public string tbNumber { get => _tbNumber; set => _tbNumber = value; }
        public string tbMail { get => _tbMail; set => _tbMail = value; }
        public string tbAddress { get => _tbAddress; set => _tbAddress = value; }
    }
}
