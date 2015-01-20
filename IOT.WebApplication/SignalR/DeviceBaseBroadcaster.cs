using IOT.DAL;
using IOT.Domain;
using IOT.WebApplication.SignalR.HubClasses;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace IOT.WebApplication.SignalR
{
    public class DeviceBaseBroadcaster
    {
        private readonly static Lazy<DeviceBaseBroadcaster> _instance = new Lazy<DeviceBaseBroadcaster>(() => new DeviceBaseBroadcaster(GlobalHost.ConnectionManager.GetHubContext<DeviceBaseBroadcasterHub>().Clients));

        private readonly ConcurrentDictionary<int, DeviceBase> _devices = new ConcurrentDictionary<int, DeviceBase>();

        private readonly object _updateDeviceLock = new object();

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(5000);

        private readonly Timer _timer;
        private volatile bool _updatingDevices = false;

        private DeviceBaseBroadcaster(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            PopulateDevicesFromDatabse();

            _timer = new Timer(UpdateDevices, null, _updateInterval, _updateInterval);

        }

        private void PopulateDevicesFromDatabse()
        {
            _devices.Clear();

            var allDevices = new DeviceAccessBase().GetAllDevices();

            foreach (var device in allDevices)
            {
                _devices.TryAdd(device.Id, device);
            }
        }

        public static DeviceBaseBroadcaster Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        internal IEnumerable<DeviceBase> GetAllDevices()
        {
            return _devices.Values;
        }
        
        private void UpdateDevices(object state)
        {
            lock (_updateDeviceLock)
            {
                if (!_updatingDevices)
                {
                    _updatingDevices = true;
                    
                    PopulateDevicesFromDatabse();
                    BrodcastDevices(_devices.Values);
                    
                    _updatingDevices = false;
                }
            }
        }
        #region brodcasting
        private void BrodcastDevices(ICollection<DeviceBase> devices)
        {
            Clients.All.updateDevices(devices);
        }
        #endregion
    }
}