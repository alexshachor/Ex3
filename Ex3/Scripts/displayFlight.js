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


        getLocation(onSuccessCallForLocation);

        if (interval) {
            setInterval(function () {
                getLocation(onSuccessCallForRoute);
            }, interval);
        }
    }

    function getLocation(onSuccessCallback) {
        var url =  requestData.interval + "/GetFlightData";
        $.getJSON(url, {}, onSuccessCallback);
    }

    function onSuccessCallForLocation(data) {
        if (!data) {
            return;
        }
        var location = convertLocation(context, data.Lon, data.Lat);



        drawFlightLocationOnCanvas(context, lastLocation, location);
        lastLocation = location;
    }

    function onSuccessCallForRoute(data) {
        if (!data) {
            return;
        }
        var currentLocation = convertLocation(context, data.Lon, data.Lat);
        //TODO: rearrange the function
        //drawFlightRouteOnCanvas(context, lastLocation, currentLocation);
        drawFlightLocationOnCanvas(context, lastLocation, currentLocation);

        lastLocation = currentLocation;
    }

    init();
})

