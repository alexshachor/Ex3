
function convertLocation(context, lon, lat) {
    var result = {};
    result.lon = (parseFloat(lon) + 180) * (context.canvas.width / 360);
    result.lat = (parseFloat(lat) + 90) * (context.canvas.height / 180);
    return result;
}

function drawFlightRouteOnCanvas(context, lastLocation, currentLocation) {
    context.beginPath();
    context.moveTo(lastLocation.lon, lastLocation.lat);
    context.lineTo(currentLocation.lon, currentLocation.lat);
    context.strokeStyle = "#2600ff";
    context.stroke();
}

function drawFlightLocationOnCanvas(context, lastLocation, location) {
    //context.beginPath();
    //context.arc(location.lon, location.lat, 5, 5, 10 * Math.PI);
    //context.fillStyle = "Blue";
    //context.fill();

    context.beginPath();
    context.arc(location.lon, location.lat, 5, 0, 2 * Math.PI, true);
    context.fillStyle = 'red';
    context.lineWidth = 2;
    context.fill();
    context.stroke();
    context.moveTo(location.lon, location.lat);
    if (lastLocation) {
        context.lineTo(lastLocation.lon, lastLocation.lat);
    }
    context.strokeStyle = 'black';
    context.stroke();

}