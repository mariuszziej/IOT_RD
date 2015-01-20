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

    function clearOverlays() {
        $.each(map.overlays_.array_, function () {
            var overlayToDelete = (this);
            map.removeOverlay(overlayToDelete);
        });
    }

    // Add a client-side hub method that the server will call
    ticker.client.updateDevices = function (devices) {

        clearOverlays();

        $.each(devices, function () {
            var device = (this);

            //console.log("new device:");
            //console.log(device);

            var msgCoordinates = JSON.parse(device.Message);
            var coordinates = ol.proj.transform([msgCoordinates.longitude, msgCoordinates.latitude], 'EPSG:4326', 'EPSG:3857');


            var overlay = new ol.Overlay({
                position: coordinates,
                element: $('<img src="/Images/bikeSmall.png">')
                              .css({ marginTop: '-200%', marginLeft: '-50%', cursor: 'pointer' })
                              .tooltip({ title: device.Id, trigger: 'click' })
            });

            map.addOverlay(overlay);
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

    var styleCache = {};
    var geoLayer = new ol.layer.Vector({
        source: new ol.source.GeoJSON({
            projection: 'EPSG:900913',
            url: '/Home/GetMyGeoJson'
        }),
        style: function (feature, resolution) {
            var text = resolution < 5000 ? feature.get('name') : '';
            if (!styleCache[text]) {
                styleCache[text] = [new ol.style.Style({
                    fill: new ol.style.Fill({
                        color: 'rgba(255, 255, 255, 0.1)'
                    }),
                    stroke: new ol.style.Stroke({
                        color: '#319FD3',
                        width: 1
                    }),
                    text: new ol.style.Text({
                        font: '12px Calibri,sans-serif',
                        text: text,
                        fill: new ol.style.Fill({
                            color: '#000'
                        }),
                        stroke: new ol.style.Stroke({
                            color: '#fff',
                            width: 3
                        })
                    }),
                    zIndex: 999
                })];
            }
            return styleCache[text];
        }
    });
    map.addLayer(geoLayer);
});