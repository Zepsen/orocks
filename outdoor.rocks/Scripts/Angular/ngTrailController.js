var appTrail = angular.module("ORockApp", []);

appTrail.controller("TrailCtrl", function ($scope, $http) {

    $scope.trailId = $("#trailId").text();
    
    //Main GET method
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

       setTrailProperty($scope.trail);
       COUNTRY = $scope.trail.Country;
       initMap();
   });

    //Set styles property
    var setTrailProperty = function (trail) {
        $scope.TypeIcon = setIconToTrailType(trail.Type);
        $scope.DurationTypeIcon = setIconToTrailDurationType(trail.DurationType);

        $scope.TypeText = setTextTrailType(trail.Type);
        $scope.DurationTypeText = setTextTrailDurationType(trail.DurationType);

        $scope.LabelDifficultClass = setLabelClassForDifficult(trail.Difficult);
    }

});