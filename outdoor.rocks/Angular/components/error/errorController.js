angular
    .module("ORockApp")
    .controller('ErrorCtrl', ['$scope', '$state', '$stateParams', function ($scope, $state, $stateParams) {
        'use strict';
        
        $scope.error = {
            status: "",
            header: "",
            message: ""
        };
        
        switch ($stateParams.status) {
            case '400':
                $scope.error.status = $stateParams.status;
                $scope.error.header = "Bad Request";
                $scope.error.message = "Bad format for id";
                break;
            case '401':
                $scope.error.status = $stateParams.status;
                $scope.error.header = "No autorized";
                $scope.error.message = "Authorize please";
                break;
            case '404':
                $scope.error.status = $stateParams.status;
                $scope.error.header = "Not found";
                $scope.error.message = "Not found by this id";
                break;
            case '409':
                $scope.error.status = $stateParams.status;
                $scope.error.header = "Conflict";
                $scope.error.message = "This person exist";
                break;
            default:
                $scope.error.status = 0;
                $scope.error.header = "Undefined exception";
                $scope.error.message = "Undefined";
                break;
        }

    }]);