'use strict';

app.directive('customerTypeAhead', function () {
    return {
        restrict: 'E',
        template: '<input id="customerTypeahead" ng-required="true" class="form-control" autofocus />'
    }
});