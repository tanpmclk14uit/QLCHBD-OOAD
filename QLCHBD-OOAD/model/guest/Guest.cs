using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.Guest
{
    public class Guest
    {
        private long _id;
        public long id
        {
            get => _id;
        }

        private string _cmnd;
        public string cmnd { get => _cmnd; }

        private string _address;
        public string address { get => _address; }

        private string _name;
        public string name { get => _name; }

        public Guest(long id, string cmnd, string address, string name)
        {
            _id = id;
            _cmnd = cmnd;
            _address = address;
            _name = name;
        }
    }
}
