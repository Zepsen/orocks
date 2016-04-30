angular.module('ORocksApp')
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise('/');

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: 'Angular/components/home/templates/home.html',
                    controller: 'HomeCtrl'
                })
                .state('trail', {
                    url: '/trail',
                    templateUrl: 'Angular/components/blog/templates/trail.html',
                    controller: 'TrailCtrl'
                })                
        }]);
