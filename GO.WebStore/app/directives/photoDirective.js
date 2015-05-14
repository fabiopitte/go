'use strict';

app.directive('photo', function (gostoFactory) {
    return {
        restrict: 'E',
        template: '<a href="{{photo.url}}" title="Photo Title" data-rel="colorbox" class="cboxElement">' +
                    '<img width="150" height="150" alt="150x150" src="{{photo.url}}">' +
                '</a>',
        controller: function ($scope) {
            return gostoFactory.getPhoto(url).success(function (data) {
                return data;
            });
        }
    }
});