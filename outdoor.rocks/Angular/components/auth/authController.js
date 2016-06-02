angular
    .module('ORockApp')
    .controller('AuthCtrl', ['$scope', '$http', '$state', '$cookies', function ($scope, $http, $state, $cookies) {
        'use strict';

        $scope.wrongLogin = false;
        $scope.userRegistr = {
            Name: '',
            Password: '',
            ConfirmPassword: '',
            Email: ''
        };

        $scope.userLogin = {
            Name: '',
            Password: ''
        };

        $scope.login = function () {
            $scope.sref($scope.userLogin);
        };

        $scope.registration = function () {

            $http({
                method: "POST",
                url: "/api/Account/Register",
                data: $scope.userRegistr
            })
                .then(function (response) {
                    $scope.sref($scope.userRegistr);
                },
                function (error) {
                    $scope.wrongRegistration = true;
                });

        };

        $scope.sref = function (user) {

            if (sessionStorage.getItem(user.Name) === null) {
                getToken(user);
            }
            $scope.setUser(user.Name);
            $state.go('home');
        };

        function getToken(user) {
            var data = "grant_type=password&username=" + user.Name + "&password=" + user.Password;

            $http.post(
                '/Token',
                data,
                {
                    headers: {'Content-Type': 'application/x-www-form-urlencoded'}
                })
                .then(function (response) {
                    // Cache the access token in session storage.
                    sessionStorage.setItem(response.data.userName, response.data.access_token);
                     
                    //redirect to home
                    $state.go('home');
                },
                function (error) {
                    console.log("storage" + error);
                });
        }

    }]);
