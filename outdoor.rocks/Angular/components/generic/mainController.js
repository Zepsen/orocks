angular
    .module('ORockApp')
    .controller('MainCtrl', ['$scope', '$http', '$state', '$cookies', function ($scope, $http, $state, $cookies) {
        'use strict';

        //Global prop
        $scope.search2RegionsAndTrailsModel = [];
        $scope.search2TrailsModel = [];
        $scope.btnLoginShow = true;

        $scope.user = "";
        $scope.setUser = function (user) {
            $scope.user = user;
        }

        $scope.getUser = function () {                        
            return $scope.user; 
        }

        var searchTrails = [];
        var searchCountries = [];

        $scope.auth = {
            id: "",
            name: "",
            status: ""
        }

        //Auth
        $scope.getAuth = function () {
            return $scope.auth;
        };

        //Typeaehad        
        function loadRegionsAndTrailsName() {
            $http({
                method: "GET",
                url: "/api/Filters/"
            }).then(
            function (response) {
                $scope.inputFiltes = response;
                
                $scope.inputFiltes.data.Trails.forEach(function (i) {
                    searchTrails.push(
                    {
                        "id": i.Id,
                        "name": i.Value,
                        "icon": "glyphicon-tree-conifer"
                    });
                });

                $scope.inputFiltes.data.Countries.forEach(function (i) {
                    searchCountries.push(
                    {
                        "id": i.Id,
                        "name": i.Value,
                        "icon": "glyphicon-globe"
                    });
                });
                
                $scope.search2RegionsAndTrailsModel = searchTrails.concat(searchCountries);
                $scope.search2TrailsModel = searchTrails;

            },
            function (error) {
                $state.go('error', { status: error.status });
            });
        }


        //Selected trail in input
        $scope.inputPressEnter = function (value) {
            var res = false;

            searchCountries.forEach(function (i) {
                if (i.id === value.id) {
                    res = true;
                    return;
                }
            });

            if (res) return;

            $state.go('trail', { id: value.id });
        }


        //Check if user already auths
        $scope.checkAuth = function () {
            if ($scope.user) {
                $scope.btnLoginShow = false;
                $scope.getAuthById($scope.user);
            }
            else {
                $scope.btnLoginShow = true;
            }
        }

        $scope.getAuthById = function (userName) {
            $http({
                method: "GET",
                url: "/api/Users/" + userName,
            }).then(function (response) {
                var res = response.data;
                $scope.auth.id = res.Id;
                $scope.auth.name = res.Name;
                $scope.auth.status = res.Role;

            },
            function (error) {
                $state.go('error', { status: error.status });
            });
        }


        $scope.logout = function () {            
            sessionStorage.removeItem($scope.user);
            $scope.btnLoginShow = true;
            $scope.user = "";
            $scope.auth.id = "";
            $scope.auth.status = "";
        };

        //Load functions
        loadRegionsAndTrailsName();
        

    }]);


