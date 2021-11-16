using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.appUtil
{
    public enum RentalBillStatus
    {
        OVERDUE, RECEIVEDALL, WAITING
    }
    public enum DeliveryOrderStatus
    {
        WATING, DELIVERED, ERROR
    }
    public enum DeliveryBillStatus
    {
        UNPAID, PAID
    }

    public enum StaffStatus
    {
        WORKING, FIRED 
    }
}
