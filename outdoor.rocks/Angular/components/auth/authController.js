angular
    .module('ORockApp')
    .controller('AuthCtrl', ['$scope', '$http', '$state', '$cookies', function ($scope, $http, $state, $cookies) {
        'use strict';

        $scope.wrongLogin = false;
        $scope.userRegistr = {
            name: '',
            password: '',
            password1: '',
            email: ''
        };

        $scope.userLogin = {
            name: '',
            password: ''
        };

        $scope.login = function () {

            $http({
                method: "PUT",
                url: "/api/Users/1",
                data: "=" + JSON.stringify($scope.userLogin),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            })
            .then(function (response) {
                var res = JSON.parse(response.data);
                if (res) {                    
                    $cookies.put('user', res._id);                    
                    $state.go('home');
                    
                } else {
                    console.log("No authorized");
                    $scope.wrongLogin = true;
                }
            })
            .then(function (error) {
                console.log(error);                
            });
        };

        $scope.registration = function () {

            $http({
                method: "POST",
                url: "/api/Users",
                data: "=" + JSON.stringify($scope.userRegistr),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            })
            .then(function (response) {
                var res = JSON.parse(response.data);
                if (res) {
                    $cookies.put('user', res._id);
                    $state.go('home');
                } else {
                    console.log("No registration");
                }
            })
            .then(function (error) {
                console.log(error);
            });

        };

    }]);
