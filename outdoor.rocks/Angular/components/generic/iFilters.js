
var path2ImgStorage = "/Content/Images/",
    path2IconStorage = "/Content/Icons",


    relDiff2Style = {
        easy: "label-success",
        medium: "label-warning",
        hard: "label-danger"
    },



    relIcon2TrailDurationType = {
        'loop': 'loop.png',
        "point-to-point": 'point-to-point.png',
        "in-and-out": 'in-and-out.png'
    },

    relTextTrailDurationType = {
        'loop': 'Loop',
        'point-to-point': 'Point to point',
        'in-and-out': 'In and Out'
    };

    relIcon2TrailType = {
        oneday: 'one-day.png',
        manydays: 'many-days.png',
        weekend: 'weekend.png'
    },

     relTextTrailType = {
        'oneday': 'One day',
        'manydays': 'Many days',
        'weekend': 'Weekend'
    },





angular
    .module('ORockApp')
    .filter('coverPhoto', function () {
        return function (photoName) {
            return "url('" + path2ImgStorage + ( photoName || "Default.jpg") + "')";
        };
    })
    .filter('labelDifficult', function () {
        return function (diff) {
            return relDiff2Style[diff] || "label-default";
        };
    })
    .filter('typeIcon', function () {
        return function (type) {
            return relIcon2TrailType[type];
        };
    })
    .filter('durationTypeIcon', function () {
        return function (type) {            
            return relIcon2TrailDurationType[type];
        };
    })
    .filter('typeText', function () {
        return function (type) {
            return relTextTrailType[type] || "";
        };
    })
    .filter('durationTypeText', function () {
        return function (type) {
            return relTextTrailDurationType[type] || "";
        };
    })
    .filter('getFullIconPath', function () {
        return function (path, subfolder) {

            if (path === undefined) return '';
            //var res = []
            //res.push(path2IconStorage);
            //if (subfolder) res.push(subfolder);
            //res.push(path);

            //return res.join('/');

            return [path2IconStorage, subfolder, path].join('/');
        };
    })
    .filter('getFullImgPath', function () {
        return function (path) {

            if (path === undefined) return '';

            return [path2ImgStorage, path].join('/');
        };
    });


