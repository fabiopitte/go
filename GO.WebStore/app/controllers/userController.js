'use strict';

app.controller('userController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {

    $scope.user = {};

    if (window.user != null) {
        $scope.user = window.user;
    }

    $scope.salvar = function () {

        $scope.$broadcast('show-errors-event');

        if ($scope.formUsuario.$invalid) {
            return;
        }

        var id = $scope.user.id;
        if (id == undefined) { mensagem('Erro ao alterar o usuário', error, 'erro'); } else { atualizar(); }
    };

    function atualizar() {

        var user = $scope.user;
        gostoFactory.atualizarUsuario(user)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', error, 'erro');
            });
    };

    $scope.resetar = function () {
        $scope.user = null;
        window.user = null;
    }

    $scope.obterUsuario = function () {
        var id = 1;

        gostoFactory.obterUsuario(id)
            .success(function (data) {
                $scope.user = data;
            }).error(function (error) {
            });
    }

    $scope.editar = function (item) {
        window.user = $scope.user;
    };
}]);