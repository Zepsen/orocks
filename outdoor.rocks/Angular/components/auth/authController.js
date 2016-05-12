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
            //$http({
            //    method: "PUT",
            //    url: "/api/Users/1",
            //    data: "=" + JSON.stringify($scope.userLogin),
            //    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            //})
            //.then(function (response) {
            //    var res = response.data;
            //    if (res) {
            //        $scope.sref(res);
            //    } else {
            //        console.log("No authorized");
            //        $scope.wrongLogin = true;
            //    }
            //})
            //.then(function (error) {
            //    console.log(error);
            //});
        };

        $scope.registration = function () {

            $http({
                method: "POST",
                url: "/api/Account/Register",
                data: $scope.userRegistr
            })
                .success(function (response) {
                    $scope.sref($scope.userRegistr);
                })
                .error(function (error) {
                    $scope.wrongRegistration = true;
                    console.log(error);
                });

        };

        $scope.sref = function (user) {

            if (sessionStorage.getItem(user.Name) === null) {
                getToken();
            }
            
            $scope.setUser(user.Name);

            $state.go('home');
        };

        function getToken() {
            var data = "grant_type=password&username=" + user.Name + "&password=" + user.Password;

            $http.post(
                '/Token',
                data,
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                }
                )
                .success(function (data) {
                    // Cache the access token in session storage.
                    sessionStorage.setItem(data.userName, data.access_token);

                    //redirect to home
                    $state.go('home');

                })
                .error(function (error) {
                    console.log("storage" + error)
                });
        }

    }]);
