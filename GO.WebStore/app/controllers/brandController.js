'use strict';

app.controller('brandController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {

    $scope.brand = {};
    $scope.brandies = {};
    $scope.totalRegistros = 0;
    $scope.loading = false;

    $scope.pesquisar = function () {
        $scope.loading = true;
        gostoFactory.pesquisarMarcas()
            .success(function (data) {
                $scope.brandies = data;
                $scope.totalRegistros = data.length;
                $scope.loading = false;
            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

    $scope.salvar = function () {

        $scope.$broadcast('show-errors-event');

        if ($scope.formMarca.$invalid) {
            return;
        }

        var id = $scope.brand.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    $scope.editar = function (item) {
        $scope.brand = item;
        window.brand = $scope.brand;
    };

    $scope.excluir = function (item) {

        $scope.brand = item;

        $scope.CorpoMensagem = "Deseja mesmo excluir a marca " + item.title + "?";
        $scope.BtnOk = "Excluir";
        $scope.BtnCancelar = "Cancelar";
        $('#mensagem').modal('show');
    };

    $scope.clickExcluir = function () {

        var brandId = $scope.brand.id;

        gostoFactory.excluirMarca(brandId)
            .success(function (data) {

                $scope.brandies.splice($scope.index, 1);
                $scope.totalRegistros = $scope.brandies.length;

                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

                $('#mensagem').modal('hide');

            }).error(function (error) {
                mensagem('Aconteceu algum erro', error, 'erro');
            });
    };

    if ($routeParams.code != null && window.brand != null) {
        $scope.brand = window.brand;
    }
    else {
        $scope.urlModal = "/app/views/modalExclusao.html";
        window.brand = null;
    }

    $scope.resetar = function () {
        $scope.brand = null;
        window.brand = null;
    }

    function inserir() {

        var brand = $scope.brand;
        gostoFactory.inserirMarca(brand)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', data.responseText, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };

    function atualizar() {

        var brand = $scope.brand;
        gostoFactory.atualizarMarca(brand)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', data.responseText, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };
}]);