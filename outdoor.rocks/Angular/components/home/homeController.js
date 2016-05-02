angular
    .module('ORockApp')
    .controller('HomeCtrl', ['$scope', '$http', '$state', function ($scope, $http) {
        'use strict';

        $scope.regions = null;
        $scope.countries = null;
        $scope.limitTrails = 6;
        var limitTrailsStep = 6;
        $scope.inputValue = "";
        $scope.showBtnMore = false;
        $scope.filterTrails = null;
        $scope.trails = [];
       

        function loadTrails() {
            //Get Features Trails
            $http({
                method: "GET",
                url: "/api/Trails/"
            })
            .success(function (response) {                
                $scope.trails = JSON.parse(response);
                if ($scope.trails.length > 5)
                    $scope.showBtnMore = true;
            });
        }

        function getRegionsAndCountries() {            
            $http({
                method: "GET",
                url: "/api/Locations/"
            })
            .then(function (response) {                
                $scope.regions = JSON.parse(response.data);                    
            })
            .then(function (error) {
                console.log(error);
            });
        }
        

        
        
        //Filters

        //Selected region
        $scope.selectRegion = function (index) {
            $scope.regions.forEach(function (i) {
                i.Selected = false;
            });
            $scope.regions[index].Selected = true;
            $scope.Countries = $scope.regions[index].Countries;
            $scope.filterTrails = $scope.regions[index].Region;

        }

        //Filters by country
        $scope.selectCountry = function (index) {
            $scope.filterTrails = $scope.Countries[index].Country;
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

                
        //Load functions
        loadTrails();
        getRegionsAndCountries();

    }]);
