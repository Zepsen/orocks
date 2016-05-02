angular.module('ORockApp')
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise('/');

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '../Angular/components/home/templates/home.html',
                    controller: 'HomeCtrl'
                })
                .state('trail', {
                    url: '/trail/:id',
                    templateUrl: '../Angular/components/trail/templates/trail.html',
                    controller: 'TrailCtrl'
                })
                .state('login', {
                    url: '/login',
                    templateUrl: '../Angular/components/auth/templates/login.html',
                    controller: 'AuthCtrl'
                })
                .state('registration', {
                    url: '/registration',
                    templateUrl: '../Angular/components/auth/templates/registration.html',
                    controller: 'AuthCtrl'
                });
        }]);
