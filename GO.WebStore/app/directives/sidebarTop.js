'use strict';

app.directive('sidebarTop', function() {
    return {
        restrict: 'A',
        templateUrl:'app/views/sidebarTop.html',
        controller: 'navigationController'
    }
});