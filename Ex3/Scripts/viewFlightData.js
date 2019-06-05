$(function () {

    var context;
    var lastLocation;
    var interval;


    function init() {
        //expecting requestData (which declared in the view) to be already defined
        if (!requestData) {
            return;
        }
        interval = requestData.interval || 0;
        interval *= 1000;

        context = canvasService.getCanvasContext("mapCanvas");
        getFlightDataListFromFile();
    }

    //get location from the server
    function getFlightDataListFromFile() {
        var url = "/flightData/" + requestData.fileName;
        $.getJSON(url, {}, onSuccessCallForFlightDataFromFile);
    }

    //callback function for success call for flight location
    function onSuccessCallForFlightDataFromFile(data) {
        if (!data) {
            return;
        }
        displayFlightData(data);
    }

    function displayFlightData(flightDataList) {
        var index = 0;
        var flightDataListElement = document.getElementById("flightDataList");

        var displayFlightData = function () {
            if (index >= flightDataList.length) {
                clearInterval(startInterval);
                alert("Scenario has done successfully!");
                return;
            }

            var flightData = flightDataList[index];

            //draw the new location
            var currentLocation = canvasService.convertLocation(context, flightData.FlightLocation.Lon, flightData.FlightLocation.Lat);
            canvasService.drawFlightLocationOnCanvas(context, lastLocation, currentLocation);
            lastLocation = currentLocation;

            //display flight data in details
            addFlightDataListItem(flightDataListElement, flightData);


            index++;
        };

        displayFlightData();
        //if interval was defined, w'll update the location, every interval-value time 
        if (interval) {
            var startInterval = setInterval(displayFlightData, interval);
        }
    }



    function addFlightDataListItem(flightDataListElement, flightData) {

        var flightDataItemText = "Lon: " + flightData.FlightLocation.Lon +
            " Lat: " + flightData.FlightLocation.Lat +
            " Throttle: " + flightData.Throttle +
            " Rudder: " + flightData.Rudder;

        //create a list item (li) node as a child of ul element
        var flightDataItem = document.createElement("li");
        flightDataItem.appendChild(document.createTextNode(flightDataItemText));
        flightDataListElement.appendChild(flightDataItem);
    }

    init();
});