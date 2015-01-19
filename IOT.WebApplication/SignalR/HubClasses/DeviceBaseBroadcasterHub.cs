using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using IOT.Domain;

namespace IOT.WebApplication.SignalR.HubClasses

{
    [HubName("deviceBaseBroadcasterHub")]
    public class DeviceBaseBroadcasterHub : Hub
    {
        private DeviceBaseBroadcaster _deviceBaseBroadcaster;

        public DeviceBaseBroadcasterHub() : this(DeviceBaseBroadcaster.Instance) { }
        public DeviceBaseBroadcasterHub(DeviceBaseBroadcaster deviceBaseBroadcaster)
        {
            _deviceBaseBroadcaster = deviceBaseBroadcaster;
        }
        public IEnumerable<DeviceBase> GetAllDevices()
        {
            return _deviceBaseBroadcaster.GetAllDevices();
        }
    }
}