'use strict';

app.controller('supplierController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {

    $scope.supplier = {};
    $scope.suppliers = {};
    $scope.endereco = {};
    $scope.supplier.tipoPessoa = 0;
    $scope.loading = false;

    $scope.totalRegistros = 0;
    $scope.urlEndereco = '/app/views/endereco.html';

    if ($routeParams.code != null && window.supplier != null) {
        $scope.supplier = window.supplier;
        $scope.endereco = $scope.supplier.endereco;
        window.supplier = null;
        window.endereco = null;
    }
    else {
        $scope.urlModal = "/app/views/modalExclusao.html";
        window.supplier = null;
        window.endereco = null;
    }

    $scope.pesquisar = function () {
        $scope.loading = true;
        gostoFactory.pesquisarFornecedores()
            .success(function (data) {
                $scope.suppliers = data;
                $scope.totalRegistros = data.length;
                $scope.loading = false;
            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

    $scope.salvar = function () {
        var id = $scope.supplier.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    $scope.editar = function (item) {
        $scope.supplier = item;
        window.supplier = $scope.supplier;
        $scope.endereco = item.endereco;
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

        gostoFactory.excluirFornecedor(supplierId)
            .success(function (data) {

                $scope.suppliers.splice($scope.index, 1);
                $scope.totalRegistros = $scope.suppliers.length;

                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

                $('#mensagem').modal('hide');

            }).error(function (error) {
                mensagem('Aconteceu algum erro', error, 'erro');
            });
    };

    $scope.resetar = function () {
        $scope.supplier = null;
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