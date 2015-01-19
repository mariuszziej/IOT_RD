$(function () {

    var ticker = $.connection.deviceBaseBroadcasterHub; // the generated client-side hub proxy

    function init() {
        ticker.server.getAllDevices().done(function (devices) {
            $.each(devices, function () {
                var device = (this);
                console.log(device);
            });
        });
    }

    // Add a client-side hub method that the server will call
    ticker.client.updateDevices = function (devices) {
        $.each(devices, function () {
            var device = (this);
            console.log("new device:");
            console.log(device);
        });
    }

    // Start the connection
    $.connection.hub.start().done(init);

    var map = new ol.Map({
        target: 'map',
        layers: [
          new ol.layer.Tile({
              source: new ol.source.OSM()
          })
        ],
        view: new ol.View({
            center: ol.proj.transform([19.4694176, 51.7624200], 'EPSG:4326', 'EPSG:3857'),
            zoom: 14
        }),
        controls: ol.control.defaults({
            attributionOptions: {
                collapsible: false
            }
        })
    });

});