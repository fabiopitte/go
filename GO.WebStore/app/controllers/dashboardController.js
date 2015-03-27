'use strict';

app.controller("dashboardController", function ($scope) {
    $scope.labels = ['2010', '2011', '2012', '2013', '2014', '2015', '2016'];
    $scope.series = ['Series A', 'Series B'];

    $scope.data = [
      [5, 15, 25, 35, 45, 55, 65],
      [10, 20, 30, 40, 50, 60, 70]
    ];

    $scope.labels2 = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    $scope.data2 = [300, 500, 100];
});