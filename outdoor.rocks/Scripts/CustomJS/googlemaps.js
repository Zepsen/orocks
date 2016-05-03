var COUNTRY = "Germany";

function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 8,
        center: { lat: -34.397, lng: 150.644 },
        zoomControl: true,
        scaleControl: true
    });
    var geocoder = new google.maps.Geocoder();
    map.ur
    document.getElementById('openInNewWindow').addEventListener('click', function () {
        window.open('http://maps.googleapis.com/maps/api/geocode/output');
    });

    geocodeAddress(geocoder, map);
}

function geocodeAddress(geocoder, resultsMap) {
    //var address = document.getElementById('address').value;

    geocoder.geocode({ 'address': COUNTRY }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location
            });

        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });

   
}

