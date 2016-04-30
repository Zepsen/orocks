angular
    .module("ORockApp")
    .controller("TrailCtrl", function ($scope, $http) {

    $scope.trailId = $("#trailId").text();

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
    }

    //Main GET method
    $http({
        method: "GET",
        url: "../../api/Trails/" + $scope.trailId
    })
   .success(function (response) {
       $scope.trail = JSON.parse(response);
       var name = $scope.trail.Name;

       $scope.trailComments = []
       for (var i = 0; i < $scope.trail.Comments.length; i++) {
           $scope.trailComments.push(JSON.parse($scope.trail.Comments[i]));
       }

       setTrailProperty($scope.trail);

   });

    //Get to all options by update
    $http({
        method: "GET",
        url: "../../api/Options"
    })
   .success(function (response) {

       var seasons = JSON.parse(response[0]);
       $scope.optionAllSeasons = [];
       for (var i = 0; i < seasons.length; i++) {
           $scope.optionAllSeasons.push(JSON.parse(seasons[i]));
       }

       var types = JSON.parse(response[1]);
       $scope.optionAllTypes = [];
       for (var i = 0; i < types.length; i++) {
           $scope.optionAllTypes.push(JSON.parse(types[i]));
       }

       var durType = JSON.parse(response[2]);
       $scope.optionAllDurationTypes = [];
       for (var i = 0; i < durType.length; i++) {
           $scope.optionAllDurationTypes.push(JSON.parse(durType[i]));
       }



   });

    //Set styles property
    var setTrailProperty = function (trail) {
        //type icon
        $scope.TypeIcon = setIconToTrailType(trail.Type);
        $scope.DurationTypeIcon = setIconToTrailDurationType(trail.DurationType);

        //type name
        $scope.TypeText = setTextTrailType(trail.Type);
        $scope.DurationTypeText = setTextTrailDurationType(trail.DurationType);

        //label color
        $scope.LabelDifficultClass = setLabelClassForDifficult(trail.Difficult);

        //set map position
        COUNTRY = trail.Country;
        initMap();


    }

    $scope.submitUpdateTrail = function () {
        $http({
            method: "PUT",
            url: "../../api/Trails/" + $scope.trailId,
            data: "=" + JSON.stringify($scope.updateTrail),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },

        }).success(function (response) {
            $scope.updateThisTrail();
        });

    }

    $scope.updateThisTrail = function () {

        if ($scope.updateTrail.Distance)
            $scope.trail.Distance = $scope.updateTrail.Distance;

        if ($scope.updateTrail.Difficult) {
            $scope.trail.Difficult = $scope.updateTrail.Difficult;
            $scope.LabelDifficultClass = setLabelClassForDifficult($scope.updateTrail.Difficult);
        }


        if ($scope.updateTrail.Peak)
            $scope.trail.Peak = $scope.updateTrail.Peak;

        if ($scope.updateTrail.Elevation)
            $scope.trail.Elevation = $scope.updateTrail.Elevation;

        if ($scope.updateTrail.SeasonStart)
            $scope.trail.SeasonStart = $scope.updateTrail.SeasonStart.Value;

        if ($scope.updateTrail.SeasonEnd)
            $scope.trail.SeasonEnd = $scope.updateTrail.SeasonEnd.Value;


        $scope.trail.DogAllowed = $scope.updateTrail.DogAllowed;

        $scope.trail.GoodForKids = $scope.updateTrail.GoodForKids;

        if ($scope.updateTrail.Type) {
            $scope.TypeIcon = setIconToTrailType($scope.updateTrail.Type.Value);
            $scope.TypeText = setTextTrailType($scope.updateTrail.Type.Value);
        }


        if ($scope.updateTrail.DurationType) {
            $scope.DurationTypeIcon = setIconToTrailDurationType($scope.updateTrail.DurationType.Value);
            $scope.DurationTypeText = setTextTrailDurationType($scope.updateTrail.DurationType.Value);
        }
    }

    //Comments
    //--------------------------------Stars rate ----------------------//

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

        // INSERT INTO DB_Rate rate VALUES (. $scope.chRate .);
        $scope.postCommentData.Rate = r + 1;

    };

    $scope.postCommentData = {
        Id: $scope.trailId,
        Comment: "",
        User: { _id: "571746ff04973c3dee147d7c" },
        Rate: 0
    }

    $scope.postComment = function () {

        $http({
            method: "POST",
            url: "../../api/Comments/",
            data: "=" + JSON.stringify($scope.postCommentData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },

        }).success(function (response) {
            $scope.updateComments();
        });

    }

    $scope.updateComments = function () {
        $scope.trailComments.push(
            {
                Name: $scope.postCommentData.Name,
                Rate: $scope.postCommentData.Rate,
                Comment: $scope.postCommentData.Comment
            });

    }
});