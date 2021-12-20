using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.delivery
{
    class DeliveryReportViewModel: BaseViewModel
    {
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }

        public long totalAmount { get; set; }
        public long totalValue { get; set; }

        DeliveryOrderRepository deliveryOrderRepository;
        private List<DeliOrder> _deliOrdersList;
        public List<DeliOrder> deliOrdersList => _deliOrdersList;
        //-------------------------------------------------------------------------------------------------
        public DeliveryReportViewModel()
        {
            _deliOrdersList = new List<DeliOrder>();
            deliveryOrderRepository = DeliveryOrderRepository.getInstance();

        }

        //-------------------------------------------------------------------------------------------------
        private static DeliveryReportViewModel _intance;
        public static DeliveryReportViewModel getInstance()
        {
            if (_intance == null)
            {
                _intance = new DeliveryReportViewModel();
            }
            return _intance;
        }
        //-------------------------------------------------------------------------------------------------
        private void getTotal()
        {
            totalAmount = 0;
            totalValue = 0;
            foreach(var item in _deliOrdersList)
            {
                totalAmount += item.amount;
                totalValue += item.totalBills;
            }
            OnPropertyChanged("totalAmount");
            OnPropertyChanged("totalValue");
        }
        //-------------------------------------------------------------------------------------------------
        public void getDeliveryInRange()
        {
            if (dateStart != null && dateEnd != null)
                if (dateStart.CompareTo(dateEnd) <= 0)
                {
                    _deliOrdersList = deliveryOrderRepository.getImportFormInRange(dateStart, dateEnd);
                    getTotal();
                    OnPropertyChanged("deliOrdersList");
                }
                else
                {

                }
        }
    }
}
