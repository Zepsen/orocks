angular
    .module('ORockApp')
    .controller('MainCtrl', ['$scope', '$http', '$state', '$cookies', function ($scope, $http, $state, $cookies) {
        'use strict';

        //Global prop
        $scope.search2RegionsAndTrailsModel = [];
        $scope.search2TrailsModel = [];
        $scope.btnLoginShow = true;
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
                        "id": i._id,
                        "name": i.Value,
                        "icon": "glyphicon-tree-conifer"
                    });
                });

                $scope.inputFiltes.Countries.forEach(function (i) {
                    searchCountries.push(
                    {
                        "id": i._id,
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

        $scope.checkAuth = function () {

            var id = $cookies.get('user');

            //if (id) 
                $scope.getAuthById(id);
            
        }

        $scope.getAuthById = function (id) {
            
            $http({
                method: "GET",
                url: "/api/Users/" + id,
            }).then(function (response) {
                var res = response.data;
                $scope.auth.id = res.Id;
                $scope.auth.status = res.Role;
                $scope.btnLoginShow = true;

            }).then(function (error) {
                console.log(error);
                $scope.btnLoginShow = false;
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
        $scope.checkAuth();

    }]);


