'use strict';

app.directive('sidebarSide', function() {
    return {
        restrict: 'A',
        templateUrl:'app/views/sidebarSide.html',
        controller: 'navigationController'
    }
});