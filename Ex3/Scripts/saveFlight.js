$(function () {

    var context;
    var lastLocation;
    var flightDataList;


    function init() {
        //expecting requestData (which declared in the view) to be already defined
        if (!requestData) {
            return;
        }
        flightDataList = [];
        var interval = requestData.interval || 0;
        interval *= 1000;
        var duration = requestData.duration || 0;
        duration *= 1000;

        context = canvasService.getCanvasContext("mapCanvas");

        //if interval was defined, w'll update the location, every interval-value time 
        if (interval) {
            var startInterval = setInterval(function () {
                getFlightDataList();
            }, interval);

            var stopInterval = setTimeout(function () {
                clearInterval(startInterval);
                saveFlightDataList(flightDataList);
            }, duration);

        }

    }

    //get location from the server
    function getFlightDataList() {
        var url = "/flightData/" + requestData.ip + "/" + requestData.port;
        $.getJSON(url, {}, onSuccessCallForFlightData);
    }

    //save flight data in the server
    function saveFlightDataList(flightDataList) {
        var url = "/save/" + requestData.fileName + "/";
        $.post(url, { flightDataList: flightDataList }, onSuccessCallForSavingFlightData);
    }

    //callback function for success call for flight location
    function onSuccessCallForFlightData(data) {
        if (!data) {
            return;
        }
        //draw the new location
        var currentLocation = canvasService.convertLocation(context, data.FlightLocation.Lon, data.FlightLocation.Lat);
        canvasService.drawFlightLocationOnCanvas(context, lastLocation, currentLocation);
        lastLocation = currentLocation;
        flightDataList.push(data);
    }

    //callback function for success call for saving flight data
    function onSuccessCallForSavingFlightData() {
        console.log("Saving flight data was success");
    }

    init();
});