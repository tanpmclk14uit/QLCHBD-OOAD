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
    class DeliveryProviderDetailViewModel : BaseViewModel
    {
        public static ChangePageHandler turnToDeliveryDetailPage;
        DeliveryProviderRepository providerRepository;
        DeliveryOrderRepository orderRepository;
        ImagesRepository imagesRepository;
        private DeliProviders _provider;
        private FileStream file;
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
            providerRepository = DeliveryProviderRepository.getInstance();
            orderRepository = DeliveryOrderRepository.getInstance();
            imagesRepository = ImagesRepository.getInstance();
            _provider = providerRepository.getProviderbyID(id);
            _imageList = imagesRepository.getImagesByProviderID(id);
            checkImageExists();

            ChangeImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onChangeImage(); });
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onDelete(id); });

        }
        private void checkImageExists()
        {
            if (File.Exists(image))
            {
                file = File.OpenRead(image);
                OnPropertyChanged("image");
                file.Close();
            }
            else if (image.Contains(@"\Assets") || image.Contains("QLCHBD"))
            {
                image = "/QLCHBD-OOAD;component/assets/img_noImage.png";
                OnPropertyChanged("image");
            }
        }
        private string getImageFromDialog(string name)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;


            if (dlg.ShowDialog() == DialogResult.OK)
            {
                image = dlg.FileName;
                string extension = Path.GetExtension(dlg.FileName);
                string fileName = name + "_" + id + "_" + DateTime.Now.ToString("mmFFFFFFF") + extension;
                string linkToAssets = Path.GetFullPath("QLCHBD-OOAD/QLCHBD-OOAD/Assets/");

                for (int i = 0; i < 6; ++i)
                {
                    linkToAssets = Path.GetDirectoryName(linkToAssets);
                }
                linkToAssets += @"\Assets\";

                linkToAssets += fileName;

                file = File.Create(linkToAssets);
                file.Close();

                File.Copy(image, linkToAssets, true);
                file.Close();
                return linkToAssets.Replace(@"\", "/");
            }
            return image.Replace(@"\", "/");
        }

        private void onChangeImage()
        {
            image = getImageFromDialog("provider");
            OnPropertyChanged("image");

        }

        private void onDelete(string id)
        {
            //MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.CONFIRM, "You definitely want to delete this provider?");
            //myDialog.ShowDialog();
            //if (myDialog.action == true)
            //{
                providerRepository.removeProviderByID(id);
                turnToDeliveryDetailPage();
            //}

        }

        private void onConfirm()
        {
            //MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.CONFIRM, "Confirm?");
            //myDialog.ShowDialog();
            //if (myDialog.action == true)
            //{
                providerRepository.updateByProvider(_provider);
                turnToDeliveryDetailPage();
            //}

        }
    }
}
