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
        int createID = 1;
        FileStream file;
        public ICommand UpdateCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }
        public string bttContent { get; set; }


        private DeliveryBillRepository billRepository;
        private DeliveryBilingItemsRepository itemsRepository;
        private DeliveryOrderRepository orderRepository;
        private ImagesRepository imagesRepository;
        private DeliveryOrderItemsRepository orderItemsRepository;
        private AlbumRepository albumRepository;
        private DeliveryProviderRepository providerRepository;

        private DeliOrderItems orderItems;

        public ObservableCollection<DeliOrderItems> orderItemsList { get; }

        private Images images;
        private ObservableCollection<Images> imagesItemsList;


        private DeliBills _Bill;
        private DeliBillsItems billItem;
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
            image = "/QLCHBD-OOAD;component/assets/img_noImage.png";
            bttContent = "Update";


            UpdateCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirmCommand(id); });
            ChangeImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onChangeImageCommand(); });
            
            if (orderRepository.getImportFormStatusWithID(id).Equals("DELIVERED"))
            {
                image = "/QLCHBD-OOAD;component/assets/img_paid.png";
                OnPropertyChanged("image");
                UpdateCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
                ChangeImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
                bttContent = "PAID";
                OnPropertyChanged("bttContent");
            }

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
                image = getImageFromSelectedItem(selectedItems).image;
                locate = getImageFromSelectedItem(selectedItems).locate;
                rentalPrice = getImageFromSelectedItem(selectedItems).rentalPrice;
                totalCopy = getImageFromSelectedItem(selectedItems).quantity;
                selectedAlbum = getAlbumFromID(getImageFromSelectedItem(selectedItems).idAlbum);
                name = getImageFromSelectedItem(selectedItems).name;
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
                imagesItemsList.Add(new Images(count,
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
                if (value.name.Equals("Add...")) openAddAlbumWindow();
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
        }
        //-------------------------------------------------------------------------------------------------

        private void onConfirmCommand(string id)
        {
            if (count > 0) onUpdate(id);
            else
            if (bttContent.Equals("PAY")) onPay(id);
            else
            if (count <= 0) onConfirmAll(id);

        }
        //-------------------------------------------------------------------------------------------------
        private void onUpdate(string id)
        {
            if (selectedItems != null)
            {
                if (selectedItems.Amount != 0 && rentalPrice != 0)
                {
                    getImageFromSelectedItem(selectedItems).locate = locate;
                    getImageFromSelectedItem(selectedItems).rentalPrice = rentalPrice;
                    getImageFromSelectedItem(selectedItems).quantity = totalCopy;
                    getImageFromSelectedItem(selectedItems).idAlbum = selectedAlbum.id;
                    getImageFromSelectedItem(selectedItems).image = image;
                    billsItemsList[orderItemsList.IndexOf(selectedItems)].setAmount = totalCopy;

                    OnPropertyChanged("billsItemsList");
                    OnPropertyChanged("imagesItemsList");
                }


                if (selectedItems.isConfirm == false)
                {
                    count--;
                    selectedItems.isConfirm = true;
                    OnPropertyChanged("selectedItems");
                    if (count <= 0)
                    {
                        bttContent = "Confirm All";
                        OnPropertyChanged("bttContent");
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------
        private void onConfirmAll(string id)
        {
            billRepository.addTemporaryBills(billID, orderRepository.getDeliOrderById(id).provider, sumAmount(), sumValue(), createID);
            foreach (var item in orderItemsList)
            {
                itemsRepository.insertItems(item, billID.ToString());
            }
            foreach (var item in imagesItemsList)
            {
                if (imagesRepository.imageIsNotNull(item.id.ToString()) && item.quantity != 0)
                {
                    imagesRepository.updateImage(item);
                }
                else if (item.quantity != 0)
                {
                    imagesRepository.uploadNewImage(item);
                }
            }
            orderRepository.updateStatusDELIVERED(id);
            bttContent = "PAY";
            OnPropertyChanged("bttContent");
        }
        //-------------------------------------------------------------------------------------------------
        private void onPay(string id)
        {
            billRepository.updateTemporaryBills(id);
            bttContent = "PAID";
            OnPropertyChanged("bttContent");
        }
        //-------------------------------------------------------------------------------------------------
        private int sumAmount()
        {
            int sum = 0;
            foreach (var item in billsItemsList)
            {
                sum += item.amount;
            }
            return sum;
        }
        private long sumValue()
        {
            int value = 0;
            foreach (var item in billsItemsList)
            {
                value += item.value;
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
                string extension = Path.GetExtension(dlg.FileName);
                string fileName = name + "_" + DateTime.Now.ToString("mmFFFFFFF") + extension;
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

    }
}