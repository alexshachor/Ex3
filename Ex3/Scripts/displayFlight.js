$(function () {

    function init() {
        if (!requestData) {
            return;
        }

        var interval = requestData.interval || 0;
        interval *= 1000;

        var canvasElement = $("#mapCanvas");
        var context = canvasElement.getContext("2d");

        var lastLocation;
        getLocation(onSuccessCallForLocation);

        if (interval) {
            setInterval(function () {
                getLocation(onSuccessCallForRoute);
            }, interval);
        }
    }

    function getLocation(onSuccessCallback) {
        var url = requestData.port + "/" + requestData.interval + "/GetFlightData";
        $.getJSON(url, {}, onSuccessCallback);
    }

    function onSuccessCallForLocation(data) {
        if (!data) {
            return;
        }
        var location = convertLocation(canvasElement, data.Lon, data.Lat);
        drawFlightLocationOnCanvas(context, location);
        lastLocation = location;
    }

    function onSuccessCallForRoute(data) {
        if (!data) {
            return;
        }
        var currentLocation = convertLocation(canvasElement, data.Lon, data.Lat);
        drawFlightRouteOnCanvas(canvasElement, lastLocation, currentLocation);
        lastLocation = currentLocation;
    }
    
    init();
})