$(function () {

    var canvasElement;
    var context;
    var lastLocation;


    function init() {
        if (!requestData) {
            return;
        }

        var interval = requestData.interval || 0;
        interval *= 1000;

        canvasElement = document.getElementById("mapCanvas");
        context = canvasElement.getContext("2d");
        context.canvas.width = window.innerWidth;
        context.canvas.height = window.innerHeight;

        getLocation();

        if (interval) {
            setInterval(function () {
                getLocation();
            }, interval);
        }
    }

    function getLocation() {
        var url = "/" + requestData.interval + "GetLocation";// + requestData.ip + "/" + requestData.port + "/" + requestData.interval;
        $.getJSON(url, {}, onSuccessCallForFlightLocation);
    }

    function onSuccessCallForFlightLocation(data) {
        if (!data) {
            return;
        }
        var currentLocation = convertLocation(context, data.Lon, data.Lat);
        drawFlightLocationOnCanvas(context, lastLocation, currentLocation);
        lastLocation = currentLocation;
    }

    init();
})

