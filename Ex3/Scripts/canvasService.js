
function convertLocation(context, lon, lat) {
    var result = {};
    result.lon = (parseFloat(lon) + 180) * (context.canvas.width / 360);
    result.lat = (parseFloat(lat) + 90) * (context.canvas.height / 180);
    return result;
}


function drawFlightLocationOnCanvas(context, lastLocation, location) {

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