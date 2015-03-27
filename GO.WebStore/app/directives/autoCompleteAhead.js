'use strict';

app.directive('autoCompleteAhead', function () {

    return {
        restrict: 'EAC',
        scope: {
            items: '=',
            prompt: '@',
            title: '@',
            subtitle: '@',
            detail: '@',
            model: '=',
            exibe: '=',
            onSelect: '&'
        },
        link: function (scope, elem, attrs) {
            scope.handleSelection = function (selectedItem) {
                
                scope.model = selectedItem;
                scope.exibe = selectedItem.nome;
                scope.current = 0;
                scope.selected = true;
                $timeout(function () {
                    scope.onSelect();
                }, 200);
            };
            scope.current = 0;
            scope.selected = true;
            scope.isCurrent = function (index) {
                return scope.current == index;
            };
            scope.setCurrent = function (index) {
                scope.current = index;
            };
        },
        templateUrl: '/app/views/autoCompleteAhead.html'
    }
});

//app.directive('typeahead', function () {
//    return {
//        restrict: 'E',
//        replace: true,
//        scope: {
//            choice: '=',
//            list: '='
//        },
//        template: '<input type="text" ng-model="choice" />',
//        link: function (scope, element, attrs) {
//            scope.typeaheadElement = element;
//            $(element).typeahead({
//                source: scope.list,
//                updater: function (item) {
//                    scope.$apply(function () {
//                        scope.choice = item;
//                    });
//                    return item;
//                }
//            });

//            scope.$watch('list', function (newList, oldList) {
//                $(element).data('typeahead').source = newList;
//            }, true);
//        }
//    };
//});