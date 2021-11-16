using QLCHBD_OOAD.model.staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.staff
{
    class StaffManageViewModel: BaseViewModel
    {
        private List<Staff> _lstStaffs;
        public List<Staff> lstStaffs
        {
            get => _lstStaffs;
            set
            {
                _lstStaffs = value;
            }
        }

        public Staff selectedItem;

        private StaffManageViewModel()
        { }

        private static StaffManageViewModel _instance;

        public static StaffManageViewModel getIntance()
        {
            if (_instance == null)
            {
                _instance = new StaffManageViewModel();
            }
            return _instance;
        }
    }
}
