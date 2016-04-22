
angular.module("ORockApp", [])

.controller("HomeCtrl", function ($scope, $http) {

     //Get Features Trails
     $http({
        method: "GET",
        url: "../api/Trails/"
     })
         .success(function (response) {
             var arr = [];
             for (var i = 0; i < response.length; i++) {
                 arr.push(JSON.parse(response[i]));
                 arr[i].LabelDifficultClass = setLabelClassForDifficult(arr[i].Difficult);
             }
                 

             $scope.trails = arr;
            
         });


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
