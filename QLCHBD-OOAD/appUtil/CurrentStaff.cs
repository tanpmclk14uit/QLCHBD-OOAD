using QLCHBD_OOAD.model.staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.appUtil
{
    class CurrentStaff
    {
        private Staff _currentStaff;
        public Staff currentStaff {
            get => _currentStaff;
            set
            {
                _currentStaff = value;
            }
        }
        private static CurrentStaff instance;

        private CurrentStaff()
        {
            currentStaff = new Staff();
        }

        public static CurrentStaff getInstance()
        {
            if (instance == null)
            {
                instance = new CurrentStaff();
            }
            return instance;
        }    

        public void setStaff (Staff newStaff)
        {
            Replication.CopyPropertiesTo(newStaff, currentStaff);
        }    

    }
}
