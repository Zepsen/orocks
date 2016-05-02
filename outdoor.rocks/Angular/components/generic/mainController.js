angular
    .module('ORockApp')
    .controller('MainCtrl', ['$scope', '$http', '$state', function ($scope, $http, $state) {
        'use strict';
      
        //Global prop
        $scope.search2RegionsAndTrailsModel = [];
        $scope.search2TrailsModel = [];
        var searchTrails = [];
        var searchCountries = [];


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

        //var _selected;

        //$scope.ngModelOptionsSelected = function (value) {
        //    if (arguments.length) {
        //        _selected = value;
        //    } else {
        //        return _selected;
        //    }
        //};

        //$scope.modelOptions = {
        //    debounce: {
        //        default: 500,
        //        blur: 250
        //    },
        //    getterSetter: true
        //};
        
        //Selected trail in input
        $scope.inputPressEnter = function (value) {
            var res = false;

            searchCountries.forEach(function(i) {
                if (i.id == value.id) {
                    res = true;
                    return;
                }
            });

            if (res) return;
            
            $state.go('trail', { id: value.id });
        }

        //Load functions
        loadRegionsAndTrailsName();

    }]);


