var appTrail = angular.module("ORockApp", []);

appTrail.controller("TrailCtrl", function ($scope, $http) {

    $scope.trailId = $("#trailId").text();
    
    $http({
        method: "GET",
        url: "../../api/Trails/" + $scope.trailId
    })
   .success(function (response) {

   });



});