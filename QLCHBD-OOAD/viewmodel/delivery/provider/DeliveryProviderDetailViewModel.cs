using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QLCHBD_OOAD.viewmodel.delivery.provider
{
    class DeliveryProviderDetailViewModel :BaseViewModel
    {
        public static ChangePageHandler turnToDeliveryDetailPage;
        DeliveryProviderRepository providerRepository;
        ImagesRepository imagesRepository;
        private DeliProviders _provider;
        public string mail { get => _provider.providerMail; set { _provider.updateMail = value; OnPropertyChanged("mail"); } }
        public string address { get => _provider.providerAddress; set { _provider.updateAddres = value; OnPropertyChanged("address"); } }
        public int number { get => _provider.providerNumber; set { _provider.updateNumber = value; OnPropertyChanged("number"); } }
        public string image { get => _provider.image; set { _provider.updateImage = value; OnPropertyChanged("image"); } }
        public string name => _provider.providerName;
        public long id => _provider.id;
        public string dayCreate => _provider.createTime;
        public ICommand ChangeImageCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private ObservableCollection<Images> _imageList;
        public ObservableCollection<Images> imageList => _imageList;


        public DeliveryProviderDetailViewModel(string id)
        {
            providerRepository = DeliveryProviderRepository.getIntance();
            imagesRepository = ImagesRepository.getInstance();
            _provider = providerRepository.getProviderbyID(id);
            _imageList = imagesRepository.getImagesByProviderID(id);

            ChangeImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onChangeImage(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onDelete(id); });

        }

        private void onChangeImage()
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = "c:\\";
            //dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            //dlg.RestoreDirectory = true;

            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    image = dlg.FileName;
            //    var linkToAssets = Path.GetFullPath("QLCHBD-OOAD/QLCHBD-OOAD/Assets/");
            //    for (int i = 0; i < 6; ++i)
            //    {
            //        linkToAssets = Path.GetDirectoryName(linkToAssets);
            //    }
            //    linkToAssets += @"\Assets\";
            //    string fileName = "img_Provider" + id + ".png";
            //    string assetsPath = linkToAssets + fileName;
            //    try
            //    {
            //        File.Copy(image, assetsPath, true);
            //    }
            //    catch (IOException copyError)
            //    {
            //        File.Delete(assetsPath);
            //        File.Move(image, assetsPath);
            //    }
            //    image = assetsPath;
            //}

        }

        private void onDelete(string id)
        {
            providerRepository.removeProviderByID(id);
            turnToDeliveryDetailPage();
        }

        private void onConfirm()
        {
            providerRepository.updateByProvider(_provider);
            turnToDeliveryDetailPage();
        }
    }
}
