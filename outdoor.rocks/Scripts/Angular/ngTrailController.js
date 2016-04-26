var app = angular.module("ORockApp");

app.controller("TrailCtrl", function ($scope, $http) {
    alert($scope.trailId);
    $http({
        method: "GET",
        url: "../api/Trails/" + trailId
    })
        .success(function (response) {

        });

});
