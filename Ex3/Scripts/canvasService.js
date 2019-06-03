
function convertLocation(canvasElement, lon, lat) {
    var result = {};
    result.lon = (parseFloat(lon) + 180) * (canvasElement.width / 360);
    result.lat = (parseFloat(lat) + 90) * (canvasElement.height / 180);
    return result;
}

function drawFlightRouteOnCanvas(context, lastLocation, currentLocation) {
    context.beginPath();
    context.moveTo(lastLocation.lon, lastLocation.lat);
    context.lineTo(currentLocation.lon, currentLocation.lat);
    context.strokeStyle = "#2600ff";
    context.stroke();
}

function drawFlightLocationOnCanvas(context, location) {
    context.beginPath();
    context.arc(location.lon, location.lat, 5, 10 * Math.PI);
    context.fillStyle = "Blue";
    context.fill();
}