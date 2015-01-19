using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Domain
{
    public class DeviceBase
    {
        public int DeviceTypeId { get; set; }
        public int DeviceId { get; set; }
        public int MessageTypeId { get; set; }
        public string Message { get; set; }
        public DateTime EventDate { get; set; }
        public int Id { get; set; }
    }
}
