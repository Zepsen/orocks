var COUNTRY = "Germany";

function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 8,
        center: { lat: -34.397, lng: 150.644 }
    });
    var geocoder = new google.maps.Geocoder();

    document.getElementById('openInNewWindow').addEventListener('click', function () {
        window.open();
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


//var myMap;
 
//// Дождёмся загрузки API и готовности DOM.
//ymaps.ready(init);
 
//function init() {
//    myMap = new ymaps.Map('yandex_map', {       
//        zoom: 12
//    });
//    var myGeocoder = ymaps.geocode("Москва");
//    myGeocoder.then(
//        function (res) {
//            myMap.geoObjects.add(res.geoObjects);
//        },
//        function (err) {
//            // обработка ошибки
//        }
//    );
//    // Создание экземпляра карты и его привязка к контейнеру с
//    // заданным id ("map").
      
//}