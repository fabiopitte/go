'use strict';

app.controller('categoryCtrl', ['$scope', 'gostoFactory', function ($scope, gostoFactory) {

    $scope.status;
    $scope.categorias;

    $scope.pesquisarCategorias = function () {

        gostoFactory.pesquisarCategorias()
            .success(function (data) {
                var template = $("#template-listagem").clone().html();

                angular.forEach(data, function (value, key) {

                    var html = template.replace("{{Title}}", value.title)
                                       .replace("{{Edicao}}", value.id).replace("{{Exclusao}}", value.id)
                                       .replace("{{Edicao-m}}", value.id).replace("{{Exclusao-m}}", value.id)

                    $("#corpo").append(html);
                });

                $scope.categorias = data;

            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao carregar os dados';
            });
    };

    $scope.atualizaCategoria = function (id) {
        var cat;
        for (var i = 0; i < $scope.categorias.length; i++) {
            var currCat = $scope.categorias[i];
            if (currCat.ID === id) {
                cat = currCat;
                break;
            }
        }

        dataFactory.updateCustomer(cat)
          .success(function () {
              $scope.status = 'Updated Category! Refreshing customer list.';
          })
          .error(function (error) {
              $scope.status = 'Unable to update category: ' + error.message;
          });
    };

    $scope.inserirCategoria = function () {
        //Fake customer data
        var cust = {
            ID: 10,
            FirstName: 'JoJo',
            LastName: 'Pikidily'
        };
        dataFactory.insertCustomer(cust)
            .success(function () {
                $scope.status = 'Inserted Customer! Refreshing customer list.';
                $scope.categorias.push(cust);
            }).
            error(function (error) {
                $scope.status = 'Unable to insert customer: ' + error.message;
            });
    };
}]);