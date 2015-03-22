'use strict';

app.controller('supplierController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {

    $scope.supplier = {};
    $scope.suppliers = {};

    $scope.pesquisar = function () {
        gostoFactory.pesquisarFornecedores()
            .success(function (data) {
                $scope.suppliers = data;
            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

    $scope.salvar = function () {

        $scope.$broadcast('show-errors-event');

        if ($scope.formSupplier.$invalid) {
            return;
        }

        var id = $scope.supplier.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    $scope.editar = function (item) {
        $scope.supplier = item;
        window.supplier = $scope.supplier;
    };

    $scope.excluir = function (item) {

        $scope.supplier = item;

        $scope.CorpoMensagem = "Deseja mesmo excluir o fornecedor " + item.nome + "?";
        $scope.BtnOk = "Excluir";
        $scope.BtnCancelar = "Cancelar";
        $('#mensagem').modal('show');
    };

    $scope.clickExcluir = function () {

        var supplierId = $scope.supplier.id;

        angular.forEach($scope.suppliers, function (item, v) {
            if (item.id === supplierId) {

            }
        });

        gostoFactory.excluirFornecedor(supplierId)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

                $('#mensagem').modal('hide');

            }).error(function (error) {
                mensagem('Aconteceu algum erro', error, 'erro');
            });
    };

    if ($routeParams.code != null && window.supplier != null) {
        $scope.supplier = window.supplier;
    }
    else {
        $scope.urlModal = "/app/views/modalExclusao.html";
        window.supplier = null;
    }

    function inserir() {

        var fornecedor = $scope.supplier;

        gostoFactory.inserirFornecedor(fornecedor)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', error, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };

    function atualizar() {

        var fornecedor = $scope.supplier;

        gostoFactory.atualizarFornecedor(fornecedor)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', data.responseText, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };
}]);