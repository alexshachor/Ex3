$(function () {
    var url = "10/5/GetFlightData";
    var params = { ip: "1.2.3.4", port: 5 };
    getLocation(url, params);

    function getLocation(url, params) {

        $.getJSON(url, {}, function (data) {

            alert(data.Lon);
            alert(data.Lat);
        });

    }
})