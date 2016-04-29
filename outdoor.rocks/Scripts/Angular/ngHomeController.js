var app = angular.module("ORockApp", []);

app.controller("HomeCtrl", function ($scope, $http, $window) {
    $scope.regions = null;
    $scope.countries = null;
    $scope.limitTrails = 6;
    var limitTrailsStep = 6;
    $scope.inputValue = "";
    $scope.showBtnMore = false;
    $scope.filterTrails = null;


    //Countries from file
    $http.get('Content/Countries/countries.json').success(function (data) {
        $scope.regions = data;
        setDefaultRegionAndCountry();
    });

    var setDefaultRegionAndCountry = function () {
        $scope.regions[1].selected = true;
        $scope.countries = $scope.regions[1].countries;
    }



    //Selected region
    $scope.selectRegion = function (index) {
        $scope.regions.forEach(function (i) {
            i.selected = false;
        })
        $scope.regions[index].selected = true;
        $scope.countries = $scope.regions[index].countries;
        $scope.filterTrails = $scope.regions[index].region;

    }

    //Filters by country
    $scope.selectCountry = function () {
        $scope.filterTrails = 13.4;
    }

    //Select trail
    $scope.selectTrail = function (id) {
        $window.location.href = "/Home/Trail/" + id;
    }


    //Get Features Trails
    $http({
        method: "GET",
        url: "../api/Trails/"
    })
        .success(function (response) {
            var arr = [];
            for (var i = 0; i < response.length; i++) {
                arr.push(JSON.parse(response[i]));
                arr[i].CoverPhoto = setPhotoPathStyle(arr[i].CoverPhoto);
                arr[i].Type = setIconToTrailType(arr[i].Type);
                arr[i].DurationType = setIconToTrailDurationType(arr[i].DurationType);
                arr[i].LabelDifficultClass = setLabelClassForDifficult(arr[i].Difficult);
                TRAILS.push(arr[i].Name);
            }

            $scope.trails = arr;

            if ($scope.trails.length > 5)
                $scope.showBtnMore = true;
        });

    var setPhotoPathStyle = function (img) {
        var res = 'background-image:url(Content/Images/';

        return res += ((img != null) ? img : "Default.jpg") + ')';
    }



    //Add more trails to home pages
    $scope.moreTrails = function () {
        if ($scope.trails.length > $scope.limitTrails) {
            $scope.limitTrails += limitTrailsStep;
            if ($scope.trails.length < $scope.limitTrails) {
                $scope.showBtnMore = false;
            }
        }
    }

    $scope.inputPressEnter = function (value) {
        alert(1);
    }



});

