'use strict';

app.controller('categoryController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {
    $scope.index = null;
    $scope.category = {};
    $scope.categorias = {};
    $scope.totalRegistros = 0;

    $scope.pesquisar = function () {
        gostoFactory.pesquisarCategorias()
            .success(function (data) {
                $scope.categorias = data;
                $scope.totalRegistros = data.length;

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

    $scope.excluir = function (item, index) {

        $scope.category = item;
        $scope.index = index;

        $scope.CorpoMensagem = "Deseja mesmo excluir a categoria " + item.title + "?";
        $scope.BtnOk = "Excluir";
        $scope.BtnCancelar = "Cancelar";
        $('#mensagem').modal('show');
    };

    $scope.clickExcluir = function () {

        var categoryId = $scope.category.id;

        gostoFactory.excluirCategoria(categoryId)
            .success(function (data) {

                $scope.categorias.splice($scope.index, 1);
                $scope.totalRegistros = $scope.categorias.length;

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

    $scope.resetar = function () {
        $scope.category = null;
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