using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.album;
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

namespace QLCHBD_OOAD.viewmodel.delivery.detail_order
{

    class DeliveryCheckOutViewModel : BaseViewModel
    {
        public static ChangePageHandler turnToDeliveryMainPage;
        long createID = CurrentStaff.getInstance().currentStaff.id;
        FileStream file;
        public ICommand UpdateCommand { get; set; }
        public ICommand ConfirmAllCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }
        public string UpdateContent { get; set; }
        public string ConfirmAllContent { get; set; }


        private DeliveryBillRepository billRepository;
        private DeliveryBilingItemsRepository itemsRepository;
        private DeliveryOrderRepository orderRepository;
        private ImagesRepository imagesRepository;
        private DeliveryOrderItemsRepository orderItemsRepository;
        private AlbumRepository albumRepository;
        private DeliveryProviderRepository providerRepository;


        public ObservableCollection<DeliOrderItems> orderItemsList { get; }

        private ObservableCollection<Images> imagesItemsList;



        public ObservableCollection<DeliBillsItems> billsItemsList;


        public DeliveryCheckOutViewModel(string id)
        {
            orderRepository = DeliveryOrderRepository.getInstance();
            billRepository = DeliveryBillRepository.getInstance();
            itemsRepository = DeliveryBilingItemsRepository.getInstance();
            imagesRepository = ImagesRepository.getInstance();
            orderItemsRepository = DeliveryOrderItemsRepository.getInstance();
            albumRepository = AlbumRepository.getInstance();
            providerRepository = DeliveryProviderRepository.getInstance();

            imagesItemsList = new ObservableCollection<Images>();
            billsItemsList = new ObservableCollection<DeliBillsItems>();

            orderItemsList = orderItemsRepository.getItemsbyImportFormsID(id);

            setUpStatusses();
            generateID();
            setupItemsList(id);
            selectedItems = orderItemsList[0];

            setupUI(id);

        }
        //-------------------------------------------------------------------------------------------------
        private void setupUI(string id)
        {
            image = "/QLCHBD-OOAD;component/assets/img_noImage.png";
            UpdateContent = "UPDATE";
            ConfirmAllContent = "CONFIRM";
            rentalPrice = 0;

            if (orderRepository.getImportFormStatusWithID(id).Equals("DELIVERED"))
            {
                setSelectedUIAfterConfirmAll();
                if (billRepository.getImportBillStatusByImportFormID(id).Equals("UNPAID"))
                {
                    
                    UpdateCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onAfterConfirmAll(); });
                    ConfirmAllCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onPay(id); });
                    ChangeImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
                    UpdateContent = "🏠";
                    ConfirmAllContent = "PAY";
                    image = "/QLCHBD-OOAD;component/assets/img_unpay.png";
                    OnPropertyChanged("image");
                    OnPropertyChanged("UpdateContent");
                    OnPropertyChanged("ConfirmAllContent");
                }
                else if (billRepository.getImportBillStatusByImportFormID(id).Equals("PAID"))
                {
                    
                    UpdateCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => { onAfterConfirmAll(); });
                    ConfirmAllCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => {});
                    ChangeImageCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => {});
                    UpdateContent = "🏠";
                    ConfirmAllContent = "PAID";
                    image = "/QLCHBD-OOAD;component/assets/img_paid.png";
                    OnPropertyChanged("image");
                    OnPropertyChanged("UpdateContent");
                    OnPropertyChanged("ConfirmAllContent");
                }

            }
            else if (orderRepository.getImportFormStatusWithID(id).Equals("ERROR"))
            {
                UpdateCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => { onAfterConfirmAll(); });
                ConfirmAllCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => { });
                ChangeImageCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => { });
                UpdateContent = "🏠";
                ConfirmAllContent = "XXXX";
                image = "/QLCHBD-OOAD;component/assets/img_cancel.png";
                OnPropertyChanged("image");
                OnPropertyChanged("UpdateContent");
                OnPropertyChanged("ConfirmAllContent");
            }
            else
            {
                ConfirmAllCommand = new RelayCommand<object>((p) => { return UserRoles(); }, (p) => { onConfirmCommand(id); });
                UpdateCommand = new RelayCommand<object>((p) => { if (selectedItems != null && UserRoles()) return true; return false; }, (p) => { onUpdate(id); });
                ChangeImageCommand = new RelayCommand<object>((p) => { if (selectedItems != null && UserRoles()) return true; return false; }, (p) => { onChangeImageCommand(); });
            }
        }
        //-------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------
        private bool UserRoles()
        {
            return true;
        }
        //-------------------------------------------------------------------------------------------------
        private void setSelectedUIAfterConfirmAll()
        {
            name = "";
            locate = "";
            selectedAlbum = null;
            rentalPrice = 0;
            totalCopy = 0;
            OnPropertyChanged("name");
            OnPropertyChanged("locate");
            OnPropertyChanged("rentalPrice");
            OnPropertyChanged("totalCopy");
            OnPropertyChanged("selectedAlbum");
        }
        //-------------------------------------------------------------------------------------------------
        public string name { get; set; }
        public string image { get; set; }
        public string locate { get; set; }
        public int rentalPrice { get; set; }
        public int totalCopy { get; set; }

        private DeliOrderItems _selectedItems;
        public DeliOrderItems selectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                if (_selectedItems.id == -10) image = "/QLCHBD-OOAD;component/assets/img_done.png";
                else
                {
                    locate = getImageFromSelectedItem(selectedItems).locate;
                    rentalPrice = getImageFromSelectedItem(selectedItems).rentalPrice;
                    totalCopy = getImageFromSelectedItem(selectedItems).quantity;
                    selectedAlbum = getAlbumFromID(getImageFromSelectedItem(selectedItems).idAlbum);
                    name = getImageFromSelectedItem(selectedItems).name;
                }


                OnPropertyChanged("name");
                OnPropertyChanged("totalCopy");
                OnPropertyChanged("image");
                OnPropertyChanged("locate");
                OnPropertyChanged("rentalPrice");
                OnPropertyChanged("selectedAlbum");
                OnPropertyChanged("selectedItems");
            }
        }

        private Images getImageFromSelectedItem(DeliOrderItems items) => imagesItemsList[orderItemsList.IndexOf(items)];
        private Album getAlbumFromID(long id)
        {
            foreach (var item in selectedAlbumList)
            {
                if (item.id == id) return item;
            }
            return selectedAlbumList[0];
        }
        //-------------------------------------------------------------------------------------------------
        private int count = 0;
        private void setupItemsList(string id)
        {
            var providerID = providerRepository.getProviderIDbyImportFormID(id);
            foreach (var item in orderItemsList)
            {
                billsItemsList.Add(new DeliBillsItems(count,
                                        billID,
                                        item.diskName,
                                        item.imPrice,
                                        item.diskID,
                                        totalCopy));
                imagesItemsList.Add(new Images(item.diskID,
                                item.diskName,
                                selectedAlbumList[0].id,
                                totalCopy,
                                "/QLCHBD-OOAD;component/assets/img_noImage.png",
                                "Unkown",
                                false,
                                0,
                                providerID,
                                item.IDbyProvider,
                                (int)item.imPrice,
                                DateTime.Now,
                                createID, 0));
                count++;
            }
        }

        //-------------------------------------------------------------------------------------------------
        private int _billID;
        public int billID => _billID;
        private void generateID()
        {
            Random random = new Random();
            _billID = (int)random.Next();
            while (billRepository.DeliBillsIDisNotNULL(_billID))
            {
                _billID = (int)random.Next();
            }
        }
        //-------------------------------------------------------------------------------------------------
        private ObservableCollection<Album> _selectedAlbumList;
        public ObservableCollection<Album> selectedAlbumList => _selectedAlbumList;

        private Album _selectedAlbum;
        public Album selectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                if (value != null && value.name.Equals("Add...")) openAddAlbumWindow();
                OnPropertyChanged("selectedAlbum");

            }
        }
        //-------------------------------------------------
        private void setUpStatusses()
        {
            _selectedAlbumList = albumRepository.getAllAlbum();
            _selectedAlbumList.Add(new Album(-1, "Add...", DateTime.Now));
            selectedAlbum = _selectedAlbumList[0];
            OnPropertyChanged("selectedAlbumList");
        }
        //-------------------------------------------------
        private void openAddAlbumWindow()
        {
            AddNewAlbumWindow window = new AddNewAlbumWindow();
            window.ShowDialog();
            setUpStatusses();
            selectedAlbum = selectedAlbumList[selectedAlbumList.Count - 2];
        }
        //-------------------------------------------------------------------------------------------------

        private void onConfirmCommand(string id)
        {
            if (ConfirmAllContent.Equals("PAY")) onPay(id);
            else if (ConfirmAllContent.Equals("PAID")) { }
            else onConfirmAll(id);
        }
        //-------------------------------------------------------------------------------------------------
        private void onUpdate(string id)
        {
            if (ConfirmAllContent.Equals("CONFIRM"))
            {
                getImageFromSelectedItem(selectedItems).locate = locate;
                getImageFromSelectedItem(selectedItems).rentalPrice = rentalPrice;
                getImageFromSelectedItem(selectedItems).quantity = totalCopy;
                getImageFromSelectedItem(selectedItems).idAlbum = selectedAlbum.id;
                getImageFromSelectedItem(selectedItems).image = image;
                billsItemsList[orderItemsList.IndexOf(selectedItems)].setAmount = totalCopy;

                OnPropertyChanged("billsItemsList");
                OnPropertyChanged("imagesItemsList");

                if (selectedItems.isConfirm == false)
                {
                    selectedItems.isConfirm = true;
                }
            }
            else onAfterConfirmAll();
        }
        //-------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------
        private void onConfirmAll(string id)
        {
            MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.CONFIRM, "You definitely want to Confirm all disks?");
            myDialog.ShowDialog();
            if (myDialog.action == true)
            {
                billRepository.addTemporaryBills(billID, id, orderRepository.getDeliOrderById(id).provider, sumAmount(), sumValue(), createID);
                foreach (var item in imagesItemsList)
                {
                    itemsRepository.insertItems(item, billID.ToString());
                    if (imagesRepository.imageIsNotNull(item.id.ToString()) && item.quantity != 0)
                    {
                        confirmImage(item);
                        imagesRepository.updateImage(item);
                    }
                    else if (item.quantity != 0)
                    {
                        confirmImage(item);
                        item.publish = DateTime.Now;
                        imagesRepository.uploadNewImage(item);
                    }

                }
                orderRepository.updateStatusDELIVERED(id);

                setSelectedUIAfterConfirmAll();
                ConfirmAllContent = "PAY";
                UpdateContent = "🏠";

                image = "/QLCHBD-OOAD;component/assets/img_unpay.png";
                OnPropertyChanged("image");
                OnPropertyChanged("ConfirmAllContent");
                OnPropertyChanged("UpdateContent");
            }

        }
        //-------------------------------------------------------------------------------------------------
        private void onPay(string id)
        {
            billRepository.updateTemporaryBillsWithImportFormID(id);
            ConfirmAllContent = "PAID";
            image = "/QLCHBD-OOAD;component/assets/img_paid.png";
            OnPropertyChanged("image");
            OnPropertyChanged("ConfirmAllContent");
        }
        //-------------------------------------------------------------------------------------------------
        private void onAfterConfirmAll()
        {
            turnToDeliveryMainPage();
        }
        //-------------------------------------------------------------------------------------------------
        private int sumAmount()
        {
            int sum = 0;
            foreach (var item in imagesItemsList)
            {
                sum += item.quantity;
            }
            return sum;
        }
        private long sumValue()
        {
            int value = 0;
            foreach (var item in imagesItemsList)
            {
                value += item.lostCharges;
            }
            return value;
        }
        //-------------------------------------------------------------------------------------------------
        private void onChangeImageCommand()
        {
            image = getImageFromDialog("Image");
            OnPropertyChanged("image");
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
            }
            return image;
        }

        private void confirmImage(Images images)
        {
            string extension = Path.GetExtension(images.image);
            string fileName = images.name + "_" + DateTime.Now.ToString("mmFFFFFFF") + extension;
            string linkToAssets = Path.GetFullPath("QLCHBD-OOAD/QLCHBD-OOAD/Assets/");

            for (int i = 0; i < 6; ++i)
            {
                linkToAssets = Path.GetDirectoryName(linkToAssets);
            }
            linkToAssets += @"\Assets\";

            linkToAssets += fileName;


            if (images.image != "/QLCHBD-OOAD;component/assets/img_noImage.png")
            {
                file = File.Create(linkToAssets);
                file.Close();

                File.Copy(images.image, linkToAssets, true);
                file.Close();
                images.image = linkToAssets.Replace(@"\", "/");
            }

        }

    }
}