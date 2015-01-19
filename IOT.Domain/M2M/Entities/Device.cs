using IOT.Domain.M2M;
using IOT.Domain.M2M.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Domain.Entities.M2M
{
    public class Bike
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DeviceState State { get; set; }

        public double Voltage { get; set; }

        public bool AlarmEnabled { get; set; }

        public int GroupId { get; set; }

        public Location LatestLocation { get; set; }
    }
}
