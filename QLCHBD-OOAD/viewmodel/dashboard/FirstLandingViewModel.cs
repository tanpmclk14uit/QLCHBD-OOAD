using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.dashboard
{
    class FirstLandingViewModel: BaseViewModel
    {
        private static FirstLandingViewModel _instance;
        public static FirstLandingViewModel getIntance()
        {
            if (_instance == null)
            {
                return _instance = new FirstLandingViewModel();
            }
            return _instance;
        }

    }
}
