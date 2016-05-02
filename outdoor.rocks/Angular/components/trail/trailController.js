angular
    .module("ORockApp")
    .controller("TrailCtrl", ['$scope', '$http', function ($scope, $http) {
        'use strict';

        $scope.trail = [];
        $scope.options = [];
        $scope.miniGalleryResponsive = [
         {
             breakpoint: 1024,
             settings: {
                 slidesToShow: 4,
                 slidesToScroll: 1
             }
         },
         {
             breakpoint: 980,
             settings: {
                 slidesToShow: 3,
                 slidesToScroll: 1
             }
         },
         {
             breakpoint: 767,
             settings: {
                 slidesToShow: 2,
                 slidesToScroll: 1
             }
         },
         {
             breakpoint: 500,
             settings: {
                 slidesToShow: 1,
                 slidesToScroll: 1
             }
         }
        ];           
        $scope.updateTrail = {
            Distance: "",
            Peak: "",
            Elevation: "",
            SeasonStart: "",
            SeasonEnd: "",
            DogAllowed: false,
            GoodForKids: false,
            Type: "",
            DurationType: ""
        };

        
        //SPIKE
        function getTrailId() {
            return window.location.href.split('/').pop();
        }

        function initTrailMap(country) {
            COUNTRY = country || "USA";            
            initMap();
        }

        function loadTrail() {

            //GET Trail by Id 
            $http({
                method: "GET",
                url: "/api/Trails/" + getTrailId()                
            })
           .then(
            function (response) {
                $scope.trail = JSON.parse(response.data);
                initTrailMap($scope.trail.Country);
            },
           function (error) {
               console.log("Error");
           });
        }

        //Get to all options by update
        function loadOptions() {
            $http({
                method: "GET",
                url: "/api/Options"
            })
            .then(
            function (response) {                
                $scope.options = JSON.parse(response.data);                
            },
            function (error) {
                console.log("Error");
            });
        }

        

        //Update trails and return  update trail
        $scope.submitUpdateTrail = function () {
            
            $http({
                method: "PUT",
                url: "/api/Trails/" + getTrailId(),
                data: "=" + JSON.stringify($scope.updateTrail),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },

            })
            .then(function (response) {
                $scope.trail = JSON.parse(response.data);                
            })
            .then(function (error) {
                console.log(error);
            });
        }

       

        //Comments
        //Rate
        var starPathSelect = "/Content/Icons/stars/gold-star-icon.png";
        var starPathUnselect = "/Content/Icons/stars/empty_star_icon.png";

        $scope.imgStar = [starPathUnselect, starPathUnselect, starPathUnselect, starPathUnselect, starPathUnselect];
        $scope.IfRateChose = false;

        //rating stars          
        $scope.mEnterStars = function (stars) {
            if ($scope.IfRateChos) {
                return;
            }
            else {
                for (var i = 0; i < 5; i++) {
                    if (i <= stars)
                        $scope.imgStar[i] = starPathSelect;
                    else
                        $scope.imgStar[i] = starPathUnselect;
                }
            }
        };

        $scope.mOverStars = function () {
            if ($scope.IfRateChose) {
                return;
            }
            else {
                for (var i = 0; i < 5; i++) {
                    $scope.imgStar[i] = starPathUnselect;
                }
            }
        };

        //btn Rate        
        $scope.btnRate = function (r) {
            // r start from 0;

            for (var i = 0; i < 5; i++) {
                if (i <= r)
                    $scope.imgStar[i] = starPathSelect;
                else
                    $scope.imgStar[i] = starPathUnselect;
            }

            $scope.IfRateChose = true;                       
            $scope.postCommentData.Rate = r + 1;

        };

        //$scope.postCommentData = {
        //    Id: $scope.trailId,
        //    Comment: "",
        //    User: { _id: "571746ff04973c3dee147d7c" },
        //    Rate: 0
        //}

        $scope.postComment = function () {

            $http({
                method: "POST",
                url: "/api/Comments/",
                data: "=" + JSON.stringify($scope.postCommentData),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },

            })
            .then(function (response) {
                updateComments();
            })
            .then(function (error) {
                console.log(error);
            });

        }

        $scope.updateComments = function () {
            $scope.trail.Comments.push(
                {
                    Name: $scope.postCommentData.Name,
                    Rate: $scope.postCommentData.Rate,
                    Comment: $scope.postCommentData.Comment
                });

        }


        //Load functions
        loadTrail();       
        loadOptions();

    }]);