'use strict';

app.controller('navigationController', function ($scope, $location, authService) {

    $scope.isActive = function (path) {
        return path === $location.path();
    }

    $scope.authentication = authService.authentication;
});