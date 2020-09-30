// Функция ymaps.ready() будет вызвана, когда
// загрузятся все компоненты API, а также когда будет готово DOM-дерево.
ymaps.ready(init);
function init() {
    // Создание карты.
    var myMap = new ymaps.Map("map", {
        // Координаты центра карты.
        // Порядок по умолчанию: «широта, долгота».
        // Чтобы не определять координаты центра карты вручную,
        // воспользуйтесь инструментом Определение координат.
        center: [51.662196, 39.200720],
        // Уровень масштабирования. Допустимые значения:
        // от 0 (весь мир) до 19.
        zoom: 12
    });


    var myGeoObject = new ymaps.Placemark([51.657615, 39.205762], {}, {
        preset: 'islands#redIcon'
    });
    var myGeoObject2 = new ymaps.Placemark([51.671312, 39.187856], {}, {
        preset: 'islands#redIcon'
    });

    // Размещение геообъекта на карте.
    myMap.geoObjects.add(myGeoObject);
    myMap.geoObjects.add(myGeoObject2);
    //Зона доставки
    var myCircle = new ymaps.GeoObject({
        geometry: {
            type: "Circle",
            coordinates: [51.668304, 39.221201],
            radius: 4000
        }
    });
    myMap.geoObjects.add(myCircle);

}