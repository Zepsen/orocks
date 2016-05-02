angular
    .module('ORockApp')
    .controller('AuthCtrl', ['$scope', '$http', '$state', '$cookies', function ($scope, $http, $state, $cookies) {
        'use strict';

        $scope.user = {
            name: '',
            password: ''
        };

        $scope.login = function () {

            $http({
                method: "PUT",
                url: "/api/Users/1",
                data: "=" + JSON.stringify($scope.user),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            })
            .then(function (response) {
                var res = JSON.parse(response.data);
                if (res) {
                    $cookies.put('user', res);
                    $state.go('home');
                } else {
                    console.log("Error");
                }
            })
            .then(function (error) {
                console.log(error);
            });

        };

    }]);
