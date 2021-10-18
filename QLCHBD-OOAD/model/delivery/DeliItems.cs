using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.model.delivery
{

    class DeliItems
    {
        private long _id;
        private long _deliID;
        private string _diskName;
        private long _imPrice;
        private long _diskID;
        private DateTime _createTime;
        private DateTime _updateTime;

        public long id { get => _id; }
        public long deliID { get => _deliID; }
        public string diskName { get => _diskName; }
        public long imprice { get => _imPrice; }
        public long diskID { get => _diskID; }
        public String createTime { get => _createTime.ToShortDateString(); }
        public String updateTime { get => _updateTime.ToShortDateString(); }
        
        public DateTime SetUpdateTime { set { _updateTime = value; } }

        public DeliItems(long id, long deliID, string diskName, long imPrice, long diskID, DateTime createTime, DateTime updateTime)
        {
            this._id = id;
            this._deliID = deliID;
            this._diskName = diskName;
            this._imPrice = imPrice;
            this._diskID = diskID;
            this._createTime = createTime;
            this._updateTime = updateTime;
        }

    }
}
