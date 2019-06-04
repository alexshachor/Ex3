$(function () {

    var canvasElement;
    var context;
    var lastLocation;


    function init() {
        //expecting requestData (which declared in the view) to be already defined
        if (!requestData) {
            return;
        }

        var interval = requestData.interval || 0;
        interval *= 1000;

        //adjust canvas to our screen
        canvasElement = document.getElementById("mapCanvas");
        context = canvasElement.getContext("2d");
        context.canvas.width = window.innerWidth;
        context.canvas.height = window.innerHeight;

        getLocation();

        //if interval was defined, w'll update the location, every interval-value time 
        if (interval) {
            setInterval(function () {
                getLocation();
            }, interval);
        }
    }

    //get location from the server
    function getLocation() {
        var url = "/getLocation/" + requestData.ip + "/" + requestData.port;
        $.getJSON(url, {}, onSuccessCallForFlightLocation);
    }

    //callback function for success call for flight location
    function onSuccessCallForFlightLocation(data) {
        if (!data) {
            return;
        }
        //draw the new location
        var currentLocation = canvasService.convertLocation(context, data.Lon, data.Lat);
        canvasService.drawFlightLocationOnCanvas(context, lastLocation, currentLocation);
        lastLocation = currentLocation;
    }

    init();
})

