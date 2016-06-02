angular
    .module("ORockApp")
    .controller("TrailCtrl", ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
        'use strict';


        $scope.trail = [];
        $scope.options = [];

        $scope.slickConfig = {
            infinite: true,
            slidesToShow: 4,
            slidesToScroll: 4,
            responsive: [
              {
                  breakpoint: 1024,
                  settings: {
                      slidesToShow: 3,
                      slidesToScroll: 3,
                      infinite: true,
                      dots: true
                  }
              },
              {
                  breakpoint: 600,
                  settings: {
                      slidesToShow: 2,
                      slidesToScroll: 2
                  }
              },
              {
                  breakpoint: 480,
                  settings: {
                      slidesToShow: 1,
                      slidesToScroll: 1
                  }
              }
            ]
        };

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



        function initTrailMap(country) {
            COUNTRY = country || "USA";
            initMap();
        }

        function loadTrail() {

            //GET Trail by Id 
            $http({
                method: "GET",
                url: "/api/Trails/" + $stateParams.id
            })
                .then(
                function (response) {
                    $scope.trail = response.data;
                    initTrailMap($scope.trail.Country);
                },
                function (error) {
                    $state.go('error', { status: error.status });
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
                $scope.options = response.data;
            },
            function (error) {
                $state.go('error', { status: error.status });
            });
        }


        //Update trails and return  update trail
        $scope.submitUpdateTrail = function () {

            $http({
                method: "PUT",
                url: "/api/Trails/" + $stateParams.id,
                data: "=" + JSON.stringify($scope.updateTrail),
                headers:
                    {
                        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                        'Authorization': 'Bearer ' + sessionStorage.getItem($scope.getUser())
                    },

            })
            .then(
            function (response) {
                $scope.trail = response.data;
            },
            function (error) {
                $state.go('error', { status: error.status });

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

        $scope.postCommentData = {
            Id: $stateParams.id,
            Comment: "",
            User: $scope.auth.id,
            Name: $scope.auth.name,
            Rate: 0
        }

        $scope.postComment = function () {
            $http({
                method: "POST",
                url: "/api/Comments/",
                data: "=" + JSON.stringify($scope.postCommentData),
                headers:
                    {
                        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                        'Authorization': 'Bearer ' + sessionStorage.getItem($scope.getUser())
                    },

            })
            .success(function (response) {
                $scope.updateComments();
            })
            .error(function (error) {
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

        //Auth
        function checkAuthTrail() {
            var auth = $scope.getAuth();

            $scope.isAdmin = auth.status === 'Admin';
            $scope.isUser = auth.status === 'Admin' || $scope.auth.status === 'User';
        }


        //Show full size img by click 
        $scope.clickImg = function (index) {
            $("#imgModal").attr('src', $('#gal' + index).attr('data-img-url'));
        };


        //Load functions
        loadTrail();
        loadOptions();
        checkAuthTrail();


    }]);