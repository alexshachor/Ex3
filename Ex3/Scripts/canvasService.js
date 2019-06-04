var canvasService;
$(function () {

    //convert the location from lon and lat to their relative position on screen
    function convertLocation(context, lon, lat) {
        var result = {};
        result.lon = (parseFloat(lon) + 180) * (context.canvas.width / 360);
        result.lat = (parseFloat(lat) + 90) * (context.canvas.height / 180);
        return result;
    }

    //draw the current location/route of the plance
    function drawFlightLocationOnCanvas(context, lastLocation, location) {

        context.beginPath();

        //draw the starting point only once.
        if (!lastLocation) {
            context.arc(location.lon, location.lat, 5, 0, 2 * Math.PI, true);
        }

        //points style
        context.lineWidth = 2;
        context.fillStyle = 'red';
        context.fill();

        //draw the flight route
        context.moveTo(location.lon, location.lat);
        if (lastLocation) {
            context.lineTo(lastLocation.lon, lastLocation.lat);
        }

        //route style
        context.strokeStyle = 'black';
        context.stroke();
    }

    //exposing the service functionality
    canvasService = { convertLocation: convertLocation, drawFlightLocationOnCanvas: drawFlightLocationOnCanvas };
})
