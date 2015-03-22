'use strict';

var app = angular.module('gosto', ['ngRoute', 'LocalStorageModule']);

app.config(['$routeProvider', function ($routeProvider) {
    
    $routeProvider.when("/", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when('/home', {
        controller: 'homeController',
        templateUrl: '/app/views/home.html'
    });

    $routeProvider.when('/category', {
        controller: 'categoryCtrl',
        templateUrl: '/app/views/category.html'
    });

    $routeProvider.otherwise({ redirectTo: '/' });
}]);

var serviceBase = 'http://localhost:60629/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.factory('gostoFactory', ['$http', function ($http) {

    var urlBase = 'http://localhost:60629/api/v1/public';
    var dataFactory = {};

    dataFactory.pesquisarCategorias = function () {
        return $http.get(urlBase + '/categories');
    };

    return dataFactory;
}]);