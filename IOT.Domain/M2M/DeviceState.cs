using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Domain.M2M
{
    [Flags]
    public enum DeviceState
    {
        Unknown = 0x00,

        Ok = 0x01,

        Rented = 0x02,

        Locked = 0x04,

        Alarm = 0x08,

        FallDown = 0x10
    }
}
