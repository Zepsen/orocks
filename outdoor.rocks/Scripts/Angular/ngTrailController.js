var appTrail = angular.module("ORockApp", []);

appTrail.controller("TrailCtrl", function ($scope, $http) {

    $scope.trailId = $("#trailId").text();
    
    $http({
        method: "GET",
        url: "../../api/Trails/" + $scope.trailId
    })
   .success(function (response) {
       $scope.trail = JSON.parse(response);
       var name = $scope.trail.Name;
       $scope.trailComments = []
       for(var i=0; i < $scope.trail.Comments.length; i++) {
           $scope.trailComments.push(JSON.parse($scope.trail.Comments[i]));
       }
       
   });


});