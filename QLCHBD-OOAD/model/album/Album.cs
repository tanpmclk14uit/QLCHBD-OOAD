using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.album
{
    class Album
    {

        public Album(long id, string name, DateTime createTime, DateTime updateTime)
        {
            this._id = id;
            this._name = name;
            this._createTime = createTime;
            this._updateTime = updateTime;
        }

        public Album(long id, string name, DateTime createTime)
        {
            this._id = id;
            this._name = name;
            this._createTime = createTime;
            this._updateTime = createTime;
        }

        private long _id;
        public long id
        {
            get => _id;
        }

        private string _name;
        public string name
        {
            get => _name;
        }

        private DateTime _createTime;
        public DateTime createTime
        {
            get => _createTime;
        }

        private DateTime _updateTime;
        public DateTime updateTime
        {
            get => _updateTime;
        }
    }
}
