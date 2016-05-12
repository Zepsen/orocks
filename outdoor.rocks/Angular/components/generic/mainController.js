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
            })
            .success(function (response) {
                $scope.inputFiltes = response;

                $scope.inputFiltes.Trails.forEach(function (i) {
                    searchTrails.push(
                    {
                        "id": i.Id,
                        "name": i.Value,
                        "icon": "glyphicon-tree-conifer"
                    });
                });

                $scope.inputFiltes.Countries.forEach(function (i) {
                    searchCountries.push(
                    {
                        "id": i.Id,
                        "name": i.Value,
                        "icon": "glyphicon-globe"
                    });
                });

                $scope.search2RegionsAndTrailsModel = searchTrails.concat(searchCountries);
                $scope.search2TrailsModel = searchTrails;

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
                $scope.auth.status = res.Role;

            }).then(function (error) {
                console.log(error);
            });
        }


        $scope.logout = function () {
            $cookies.remove("user");
            $scope.btnLoginShow = true;
            $scope.auth.id = "";
            $scope.auth.status = "";
        };

        //Load functions
        loadRegionsAndTrailsName();
        //$scope.checkAuth();

    }]);


