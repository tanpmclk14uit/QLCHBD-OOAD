using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class AddNewOrderImageViewModel: BaseViewModel
    {
        public ICommand removeCommand { get; set; }

        public static event DeleteOrderDiskItemHandler deleteOrderItem;

        public ICommand confirmCommand { get; set; }
        public ICommand addCommand { get; set; }

        public static event ConfirmAddOrderImage confirmOrderImage;

        private DeliveryOrderItemsRepository deliveryOrderItemsRepository = DeliveryOrderItemsRepository.getIntance();
        private DeliveryOrderRepository deliveryOrderRepository = DeliveryOrderRepository.getIntance();
        private ImagesRepository imagesRepository = ImagesRepository.getInstance();

        private Images _selectedItem;
        public  Images selectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
            }
        }

        private List<Images> _lstOnOrder;
        public List<Images> lstOnOrder
        {
            get => _lstOnOrder;
            set
            {
                _lstOnOrder = value;
                OnPropertyChanged("lstOnOrder");
            }
        }

        public List<Images> _lstOnOrderBackup;
        public List<Images> lstOnOrderBackup
        {
            get => _lstOnOrderBackup;
            set
            {
                _lstOnOrderBackup = value;
            }
        }

        public int totalAmount
        {
            get 
            {
                int amount = 0;
                foreach (Images item in lstOnOrder)
                {
                    amount += Convert.ToInt32(item.orderAmount);
                }
                return amount;
            }
        }

        public int totalValue
        {
            get
            {
                int value = 0;
                foreach (Images item in lstOnOrder)
                {
                    value += Convert.ToInt32(item.value);
                }
                return value;
            }
        }

        private List<Images> defaultList = new List<Images>();
       
        public AddNewOrderImageViewModel(List<Images> lstOnOrder)
        {
            this.lstOnOrder = lstOnOrder;
            this.defaultList = lstOnOrder;
            removeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteOrderDiskItem(selectedItem); deleteOrderItem(); });
            confirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { pushList(); confirmOrderImage(); });
            addCommand = new RelayCommand<object>((p) => { return true; }, (p) => { openAddNewItemForm(); });
            AddNewOrderItemImageViewModel.confirm += AddNewItemToListOrder;
        }
        
        private void AddNewItemToListOrder(Images images)
        {
            _lstOnOrder.Add(images);
            updateList();
            deleteOrderItem();
        }

        private void openAddNewItemForm()
        {
            AddNewOrderItemImageWindow addNewOrderItemImageWindow = new AddNewOrderItemImageWindow();
            addNewOrderItemImageWindow.Show();
        }
        private void pushList()
        {
            List<long> currentListOfProvider = new List<long>();
            foreach (Images item in lstOnOrder)
            {
                if (!currentListOfProvider.Contains(item.idProvider))
                {
                    currentListOfProvider.Add(item.idProvider);
                }
            }    
            List<DeliOrderItems> listToPush = new List<DeliOrderItems>();
            foreach (long providerItem in currentListOfProvider)
            {
                long currentMaxImportFormId = deliveryOrderRepository.findCurrentMaxId();
                List<DeliOrderItems> listOfProvider = new List<DeliOrderItems>();
                int totalValue = 0;
                int totalAmount = 0;
                long currentMaxImportFormItemId = deliveryOrderItemsRepository.findCurrentMaxId();
                foreach (Images item in lstOnOrder)
                {
                    if (providerItem == item.idProvider && Convert.ToInt32(item.orderAmount) > 0)
                    {
                        DeliOrderItems deliOrderItems = new DeliOrderItems( currentMaxImportFormItemId++, currentMaxImportFormId + 1, Convert.ToInt32(item.orderAmount), item.id, item.name, Convert.ToInt64(item.providerPrice), Convert.ToInt64(item.idByProviderForEdit));
                        listOfProvider.Add(deliOrderItems);
                        totalAmount += Convert.ToInt32(item.orderAmount);
                        totalValue += item.value;
                    }
                }
                if (listOfProvider.Count != 0)
                {
                    DeliOrder newDeliOrder = new DeliOrder(currentMaxImportFormId+1, imagesRepository.getProviderNameById(providerItem), totalAmount, totalValue, 1, 1, DeliveryOrderStatus.WATING);
                    if (deliveryOrderRepository.pushNewOrder(newDeliOrder))
                    { MessageBox.Show("Add new order successfully"); }
                    else { MessageBox.Show("Add new order failed"); }
                    foreach (DeliOrderItems item in listOfProvider)
                    {
                        deliveryOrderItemsRepository.insertItems(item);
                    }    
                }
            }
        }


        private void DeleteOrderDiskItem(Images selectedItem)
        {
            _lstOnOrder.Remove(selectedItem);
            updateList();
        }


        public void updateList()
        {
            OnPropertyChanged("lstOnOrder");
            OnPropertyChanged("totalValue");
            OnPropertyChanged("totalAmount");
        }

        public bool isChange()
        {
            return lstOnOrder == defaultList;
        }
    }
}
