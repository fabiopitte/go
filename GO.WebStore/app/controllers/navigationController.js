'use strict';

app.controller('navigationController', function ($scope, $location) {

    $scope.isActive = function (path) {
        return path === $location.path();
    }
})