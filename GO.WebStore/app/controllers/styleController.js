'use strict';

app.controller('styleController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {
    $scope.index = null;
    $scope.style = {};
    $scope.styles = {};
    $scope.totalRegistros = 0;
    $scope.loading = false;

    $scope.pesquisar = function () {
        $scope.loading = true;
        gostoFactory.pesquisarEstilos()
            .success(function (data) {
                $scope.styles = data;
                $scope.totalRegistros = data.length;
                $scope.loading = false;

            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

    $scope.salvar = function () {

        $scope.$broadcast('show-errors-event');

        if ($scope.formEstilo.$invalid) {
            return;
        }

        var id = $scope.style.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    $scope.editar = function (item) {
        $scope.style = item;
        window.style = $scope.style;
    };

    $scope.excluir = function (item, index) {

        $scope.style = item;
        $scope.index = index;

        $scope.CorpoMensagem = "Deseja mesmo excluir o estilo " + item.title + "?";
        $scope.BtnOk = "Excluir";
        $scope.BtnCancelar = "Cancelar";
        $('#mensagem').modal('show');
    };

    $scope.clickExcluir = function () {

        var categoryId = $scope.style.id;

        gostoFactory.excluirEstilo(categoryId)
            .success(function (data) {

                $scope.styles.splice($scope.index, 1);
                $scope.totalRegistros = $scope.styles.length;

                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

                $('#mensagem').modal('hide');

            }).error(function (error) {
                mensagem('Aconteceu algum erro', error, 'erro');
            });
    };

    if ($routeParams.code != null && window.style != null) {
        $scope.style = window.style;
    }
    else {
        $scope.urlModal = "/app/views/modalExclusao.html";
        window.style = null;
    }

    $scope.resetar = function () {
        $scope.style = null;
        window.style = null;
    }

    function inserir() {

        var cat = $scope.style;
        gostoFactory.inserirEstilo(cat)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', data.responseText, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };

    function atualizar() {

        var cat = $scope.style;
        gostoFactory.atualizarEstilo(cat)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', data.responseText, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };
}]);