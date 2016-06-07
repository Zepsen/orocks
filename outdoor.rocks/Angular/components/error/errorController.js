angular
    .module("ORockApp")
    .controller('ErrorCtrl', ['$scope', function ($scope) {
        'use strict';
        
        $scope.error = {
            status: "",
            header: "",
            message: ""
        };

        var mainError = $scope.getError();
        debugger 
        switch (mainError.status) {
            case 304:
                $scope.error.status = mainError.status;
                $scope.error.header = "Not Modified";
                $scope.error.message = mainError.statusText;
                break;
            case 400:
                $scope.error.status = mainError.status;
                $scope.error.header = "Bad Request";
                $scope.error.message = mainError.message;
                break;
            case 401:
                $scope.error.status = mainError.status;
                $scope.error.header = "No autorized";
                $scope.error.message = mainError.message;
                break;
            case 404:
                $scope.error.status = mainError.status;
                $scope.error.header = "Not found";
                $scope.error.message = mainError.message;
                break;
            case 409:
                $scope.error.status = mainError.status;
                $scope.error.header = "Conflict";
                $scope.error.message = mainError.message;
                break;
            case 429:
                $scope.error.status = mainError.status;
                $scope.error.header = "Not found by id";
                $scope.error.message = mainError.message;
                break;
            case 430:
                $scope.error.status = mainError.status;
                $scope.error.header = "Connection error";
                $scope.error.message = mainError.message;
                break;
            case 500:
                $scope.error.status = mainError.status;
                $scope.error.header = "Server error";
                $scope.error.message = "Internal server error";
                break;
            default:
                $scope.error.status = 0;
                $scope.error.header = "Undefined exception";
                $scope.error.message = "Undefined";
                break;
        }

    }]);