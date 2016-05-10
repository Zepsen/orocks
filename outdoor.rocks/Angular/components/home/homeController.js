angular
    .module('ORockApp')
    .controller('HomeCtrl', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
        'use strict';

        $scope.regions = null;
        $scope.countries = null;
        $scope.limitTrails = 6;
        var limitTrailsStep = 6;
        $scope.inputValue = "";
        $scope.showBtnMore = false;
        $scope.filterTrails = null;
        $scope.trails = [];
        
        var loginData = {
            grant_type: 'password',
            username: 'admin@admin.ua',
            password: 'Asd_asd'
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            
            // Cache the access token in session storage.
            sessionStorage.setItem('tokenKey', data.access_token);
        });


        function loadTrails() {
            //Get Features Trails
            $http({
                method: "GET",
                url: "/api/Trails/"
            })
            .success(function (response) {                
                $scope.trails = response;
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
                $scope.regions = response.data;                    
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
            $scope.filterTrails = $scope.Countries[index];            
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
        $scope.checkAuth();
        
    }]);
