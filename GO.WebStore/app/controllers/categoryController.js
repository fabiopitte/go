'use strict';

app.controller('categoryController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {

    $scope.category = {};
    $scope.categorias = {};

    $scope.pesquisar = function () {
        gostoFactory.pesquisarCategorias()
            .success(function (data) {
                $scope.categorias = data;
            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

    $scope.salvar = function () {

        $scope.$broadcast('show-errors-event');

        if ($scope.formCategoria.$invalid) {
            return;
        }

        var id = $scope.category.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    $scope.editar = function (item) {
        $scope.category = item;
        window.category = $scope.category;
    };

    $scope.excluir = function (item) {

        $scope.category = item;

        $scope.CorpoMensagem = "Deseja mesmo excluir a categoria " + item.title + "?";
        $scope.BtnOk = "Excluir";
        $scope.BtnCancelar = "Cancelar";
        $('#mensagem').modal('show');
    };

    $scope.clickExcluir = function () {

        var categoryId = $scope.category.id;

        angular.forEach($scope.categorias, function (item, v) {
            if (item.id === categoryId) {

            }
        });

        gostoFactory.excluirCategoria(categoryId)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

                $('#mensagem').modal('hide');

            }).error(function (error) {
                mensagem('Aconteceu algum erro', error, 'erro');
            });
    };

    if ($routeParams.code != null && window.category != null) {
        $scope.category = window.category;
    }
    else {
        $scope.urlModal = "/app/views/modalExclusao.html";
        window.category = null;
    }

    function inserir() {

        var cat = $scope.category;
        gostoFactory.inserirCategoria(cat)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', data.responseText, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };

    function atualizar() {

        var cat = $scope.category;
        gostoFactory.atualizarCategoria(cat)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', data.responseText, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };
}]);