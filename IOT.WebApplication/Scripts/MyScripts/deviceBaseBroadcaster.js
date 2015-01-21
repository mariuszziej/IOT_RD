$(function () {
    var ticker = $.connection.deviceBaseBroadcasterHub; // the generated client-side hub proxy

    function init() {
        ticker.server.getAllDevices().done(function (devices) {
            $.each(devices, function () {
                var device = (this);
                console.log(device);
            });
        });
    };
    // Add a client-side hub method that the server will call
    ticker.client.updateDevices = function (devices) {
        populateChanges(devices);
    };

    // Start the connection
    $.connection.hub.start().done(init);

    map = L.map('map').setView([51.7623, 19.4693], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
			maxZoom: 18,
			attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, ' +
				'Imagery © <a href="http://mapbox.com">Mapbox</a>',
			id: 'examples.map-i875mjb7'
    }).addTo(map);
    map.addLayer(Markers);

});

var Markers = new L.FeatureGroup();
var map;

var populateChanges = function (devices) {
    var markers = Markers.getLayers();
    
    $.each(devices, function () {
        var device = (this);
        var deviceIndex = isMarkersContainsDevice(markers, device);
        if (deviceIndex != -1) {
            var coordinates = JSON.parse(device.Message);
            var newLatLng = new L.LatLng(coordinates.latitude, coordinates.longitude);
            markers[deviceIndex].setLatLng(newLatLng);
            markers[deviceIndex].unbindPopup();
            markers[deviceIndex].bindPopup(createPopupFromDevice(device));
            extendMarkerByDeviceProperties(markers[deviceIndex], device)
        } else {
            var coordinates = JSON.parse(device.Message);
            var marker = L.marker([coordinates.latitude, coordinates.longitude]);
            marker.bindPopup(createPopupFromDevice(device));
            extendMarkerByDeviceProperties(marker, device);
            Markers.addLayer(marker);
        }
    });
}

var extendMarkerByDeviceProperties = function (marker, device) {
    marker.DeviceTypeId = device.DeviceTypeId
    marker.DeviceId = device.DeviceId
    marker.MessageTypeId = device.IMessageTypeId;
    marker.Message = device.Message;
    marker.EventDate = device.EventDate;
    marker.Id = device.Id;
}

var createPopupFromDevice = function (device) {
    var returnPopup = "<p>DeviceTypeId:" + device.DeviceTypeId + "</p>" +
                      "<p>DeviceId:" + device.DeviceId + "</p>" +
                      "<p>MessageTypeId:" + device.MessageTypeId + "</p>" +
                      "<p>Message:" + device.Message + "</p>" +
                      "<p>EventDate:" + device.EventDate + "</p>" +
                      "<p>ID:" + device.Id + "</p>";
    return returnPopup;
};

var isMarkersContainsDevice = function (markers, device) {
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].Id == device.Id) {
            return i;
        }
    }
    return -1;
};

//kind of reflection in JavaScript (przyda się pozniej jak bedzie wiecej rodz. urzadzen)
var getPropertyTable = function (obj) {
    var keys = [];
    for (var key in obj) {
        keys.push(key);
    }
    return keys;
}