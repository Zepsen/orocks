
angular.module("ORockApp", [])

//.directive('myEnter', function () {
//    return function (scope, element, attrs) {
//        element.bind("keydown keypress", function (event) {
//            if(event.which === 13) {
//                scope.$apply(function (){
//                    scope.$eval(attrs.myEnter);
//                });

//                event.preventDefault();
//            }
//        });
//    };
//})

.controller("HomeCtrl", function ($scope, $http) {
    $scope.regions = null;
    $scope.countries = null;
    $scope.limitTrails = 6;
    var limitTrailsStep = 6;
    $scope.inputValue = "";

    //Countries from file
    $http.get('Content/Countries/countries.json').success(function (data) {
        $scope.regions = data;
        setDefaultRegionAndCountry();
    });
   
    var setDefaultRegionAndCountry = function () {
        $scope.regions[1].selected = true;
        $scope.countries = $scope.regions[1].countries;
    }

    //Selected region
    $scope.selectRegion = function (index) {
        $scope.regions.forEach(function (i) {
            i.selected = false;
        })
        $scope.regions[index].selected = true;
        $scope.countries = $scope.regions[index].countries;
    }



    //Get Features Trails
    $http({
        method: "GET",
        url: "../api/Trails/"
    })
        .success(function (response) {
            var arr = [];
            for (var i = 0; i < response.length; i++) {
                arr.push(JSON.parse(response[i]));
                arr[i].CoverPhoto = setPhotoPathStyle(arr[i].CoverPhoto);
                arr[i].Type = setIconToTrailType(arr[i].Type);
                arr[i].DurationType = setIconToTrailDurationType(arr[i].DurationType);
                arr[i].LabelDifficultClass = setLabelClassForDifficult(arr[i].Difficult);
            }

            $scope.trails = arr;

        });

    var setPhotoPathStyle = function(img){
        var res = 'background-image:url(Content/Images/';        
       
        return res += ((img != null) ? img : "Default.jpg") + ')';
    }

    var setIconToTrailType = function (type) {
        var res = '';
        switch (type) {
            case 'oneday': res = 'one-day.png'
                break;
            case 'manydays': res = 'many-days.png'
                break;
            case 'weekend': res = 'weekend.png'
                break;
        }
        return res
    }

    var setIconToTrailDurationType = function (durationtype) {
        var res = '';
        switch (durationtype) {
            case 'loop': res = 'loop.png'
                break;
            case 'point-to-point': res = 'point-to-point.png'
                break;
            case 'in': res = 'in-and-out.png'
                break;
        }
        return res
    }

    var setLabelClassForDifficult = function (diff) {
        var res = "label label-default";

        switch (diff) {
            case "easy": res = "label label-success";
                break;
            case "medium": res = "label label-warning";
                break;
            case "hard": res = "label label-danger";
                break;
        }

        return res;
    }

    //Add more trails to home pages
    $scope.moreTrails = function () {
        if ($scope.trails.length > $scope.limitTrails) {
            $scope.limitTrails += limitTrailsStep;
        }
    }

    $scope.inputPressEnter = function (value) {
        
    }


    
});



$("#getByIdBtn").click(function () {
    var id = $('#id').val();

    $.ajax({
        url: "../api/Trails/" + id,
        type: "GET",
        success: function (result) {
            $("#result").html(result);
        }
    });
});

$("#postBtn").click(function () {
    var data = JSON.stringify({
        Name: $('#name').val(),
        descr: $('#descr').val(),
        WhyGo: $('#why').val(),
        diff: $('#diff').val()
    });

    $.ajax({
        url: "../api/Trails/",
        type: "POST",
        data: "=" + data,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            $("#result").html(result);
        }
    });
});

//Put
$("#putBtn").click(function () {
    var id = $('#idPut').val()
    var data = JSON.stringify({
        Name: $('#namePut').val()
    });

    $.ajax({
        url: "../api/Trails/" + id,
        type: "PUT",
        data: "=" + data,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            $("#result").html(result);
        }
    });
});

//delete
$("#delBtn").click(function () {
    var id = $('#idDel').val()
    $.ajax({
        type: "DELETE",
        url: "../api/Trails/" + id,
        success: function (result) {
            $("#result").html(result);
        }
    });
});
