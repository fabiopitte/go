'use strict';

app.directive('navbar', function() {
    return {
        restrict: 'A',
        templateUrl:'app/views/navbar.html',
        controller: 'navigationController'
    }
});