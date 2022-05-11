const api = "token="

$.get(`http://ipinfo.io?${api}`, function (response) {

    var loc = response.loc
    var myArray = loc.split(",");
    let lat = myArray[0];
    let log = myArray[1];

    $("#ip").html("IP: " + response.ip);
    $("#address").html("Location: " + response.city + ", " + response.region);
    $("#lat").html("Lat: " + lat);
    $("#log").html("Log: " + log);
    $("#details").html(JSON.stringify(response, null, 4));
}, "jsonp");