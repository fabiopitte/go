/*global app*/
'use strict';

app.directive('showErrors', ['$timeout', function ($timeout) {

    return {
        restrict: 'A',
        require: '^form',
        link: function (scope, el, attrs, formCtrl) {

            var inputE1 = el[0].querySelector('[name]');

            var inputNgE1 = angular.element(inputE1);

            var inputName = inputNgE1.attr('name');

            inputNgE1.bind('blur', function () {
                el.toggleClass('has-error', formCtrl[inputName].$invalid);
            });

            scope.$on('show-errors-event', function () {
                el.toggleClass('has-error', formCtrl[inputName].$invalid);
            });

            scope.$on('hide-errors-event', function () {
                $timeout(function () {
                    el.removeClass('has-error');
                }, 0, false);
            });
        }
    };
}]);