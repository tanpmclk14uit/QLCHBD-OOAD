using QLCHBD_OOAD.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.delivery.detail_order
{
    class DeliveryReceiveParcelViewModel : BaseViewModel
    {
        private DeliveryOrderRepository orderRepository;
        private DeliveryOrderItemsRepository orderItemsRepository;
        private DeliveryBillRepository billRepository;
        private DeliveryBilingItemsRepository bilingItemsRepository;

        public DeliveryReceiveParcelViewModel()
        {
            orderRepository = DeliveryOrderRepository.getIntance();
            orderItemsRepository = DeliveryOrderItemsRepository.getIntance();
            billRepository = DeliveryBillRepository.getIntance();
            bilingItemsRepository = DeliveryBilingItemsRepository.getIntance();


        }
    }
}
