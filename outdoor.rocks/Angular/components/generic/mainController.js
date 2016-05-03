angular
    .module('ORockApp')
    .controller('MainCtrl', ['$scope', '$http', '$state', '$cookies', function ($scope, $http, $state, $cookies) {
        'use strict';
      
        //Global prop
        $scope.search2RegionsAndTrailsModel = [];
        $scope.search2TrailsModel = [];
        var searchTrails = [];
        var searchCountries = [];
        $scope.auth = {
            id: "",
            status: ""
        }
        

        //Typeaehad        
        function loadRegionsAndTrailsName() {            
            $http({
                method: "GET",
                url: "/api/Filters/"
            })
            .success(function (response) {
                $scope.inputFiltes = JSON.parse(response);                               

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

            searchCountries.forEach(function(i) {
                if (i.id === value.id) {
                    res = true;
                    return;
                }
            });

            if (res) return;
            
            $state.go('trail', { id: value.id });
        }

        //Auth
        $scope.getAuth = function() {            
            return $scope.auth;
        };
        
        function checkAuth() {
            debugger
            if ($cookies.get('user')) {
                
                var id = $cookies.get('user');
                
                $http({
                        method: "GET",
                        url: "/api/Users/" + id,                    
                }).then(function (response) {
                    var res = JSON.parse(response.data);
                    $scope.auth.id = res._id;
                    $scope.auth.status = res.Role;               
                }).then(function(error) {
                    console.log(error);                    
                });
            }
        }

        //Load functions
        loadRegionsAndTrailsName();
        checkAuth();
    }]);


